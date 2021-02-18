using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField]
    State initState = null;

    [SerializeField]
    StunState stunState = null;

    State _currentState = null;

    [SerializeField]
    List<Wall> wallToRepare = null;

    List<Vector3> _positionsToCheck = new List<Vector3>();

    public List<Vector3> PositionsToCheck
    {
        get { return _positionsToCheck; }
    }

    Vector3 _waitPos;

    public Vector3 WaitPos
    {
        get { return _waitPos; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _waitPos = transform.position;

        for (int i = 0; i < wallToRepare.Count; i++)
            _positionsToCheck.Add(wallToRepare[i].transform.position);

        _currentState = initState;
        _currentState.InitState(this);
    }

    // Update is called once per frame
    void Update()
    {
        State nextState = _currentState.UpdateState();
        if (nextState)
        {
            _currentState = nextState;
            _currentState.InitState(this);
        }
    }

    public void Stun(float strength)
    {
        if (_currentState != stunState)
        {
            stunState.TimeStunned = strength;
            stunState.InterruptedState = _currentState;
            _currentState = stunState;
            stunState.InitState(this);
        }
    }
}
