using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Repair", order = 3)]
public class RepairState : State
{
    [SerializeField]
    MoveToState doneWithRepare = null;

    [SerializeField]
    float timeToRepare = 5.0f;

    float _currentTime = 0.0f;

    [SerializeField]
    GameObject wallPrefab = null;

    Vector3 _blockPos;

    public Vector3 BlockPos
    {
        get { return _blockPos; }
        set { _blockPos = value; }
    }

    public override void InitState(Builder ai)
    {
        _ai = ai;
        _currentTime = timeToRepare;
    }

    public override State UpdateState()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0.0f)
        {
            GameObject newWall = Instantiate(wallPrefab);
            newWall.name = "new Wall";
            newWall.transform.position = BlockPos;

            doneWithRepare.BlockPos = _ai.WaitPos;
            return doneWithRepare;
        }
        return null;
    }
}
