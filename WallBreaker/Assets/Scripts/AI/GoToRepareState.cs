using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State/GoToRepare", order = 2)]
public class GoToRepareState : State
{
    [SerializeField]
    RepareState repareState = null;

    [SerializeField]
    float speed = 3.5f;

    [SerializeField]
    float stoppingDistance = 1.0f;

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
            repareState.BlockPos = _blockPos;
            return repareState;
        }
        
        return null;
    }
}
