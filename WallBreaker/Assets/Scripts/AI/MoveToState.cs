using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State/Move", order = 4)]
public class MoveToState : State
{
    [SerializeField]
    State nextState = null;

    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float stoppingDistance = 0.0f;

    NavMeshAgent _navigator;

    Vector3 _blockPos;

    public Vector3 BlockPos
    {
        get { return _blockPos; }
        set { _blockPos = value; }
    }

    public override void InitState(Builder ai)
    {
        _ai = ai;
        _navigator = _ai.GetComponent<NavMeshAgent>();
        _navigator.destination = _blockPos;
        _navigator.speed = speed;
        _navigator.stoppingDistance = stoppingDistance;
    }

    public override State UpdateState()
    {
        if (_navigator.remainingDistance <= stoppingDistance)
        {
            return nextState;
        }

        return null;
    }
}
