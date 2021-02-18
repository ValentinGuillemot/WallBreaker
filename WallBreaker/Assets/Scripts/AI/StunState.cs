using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State/Stun", order = 5)]
public class StunState : State
{
    [SerializeField]
    GameObject feedback = null;

    GameObject _instantiatedFeedback = null;

    float _timeStunned;

    public float TimeStunned
    {
        get { return _timeStunned;}
        set { _timeStunned = value; }
    }

    float _currentTime;

    State _interruptedState;

    public State InterruptedState
    {
        get { return _interruptedState; }
        set { _interruptedState = value; }
    }

    public override void InitState(Builder ai)
    {
        _ai = ai;
        _ai.GetComponent<NavMeshAgent>().destination = _ai.transform.position;
        _currentTime = _timeStunned;

        _instantiatedFeedback = Instantiate(feedback, _ai.transform);
    }

    public override State UpdateState()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0.0f)
        {
            Destroy(_instantiatedFeedback);
            return _interruptedState;
        }

        return null;
    }

    public void AddStun(float stun)
    {
        _currentTime += stun;
    }
}
