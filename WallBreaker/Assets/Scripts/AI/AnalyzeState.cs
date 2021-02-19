using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Analyze", order = 1)]
public class AnalyzeState : State
{
    [SerializeField]
    GoToRepairState stateIfFind = null;

    [SerializeField]
    float timeBetweenChecks = 3.0f;

    float _currentTime;

    public override void InitState(Builder ai)
    {
        _ai = ai;
        _currentTime = timeBetweenChecks;
    }

    public override State UpdateState()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0.0f)
        {
            _currentTime = timeBetweenChecks;
            return Analyze();
        }

        return null;
    }

    State Analyze()
    {
        List<Vector3> positions = _ai.PositionsToCheck;
        Vector3 middle = _ai.transform.position;
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 dir = (positions[i] - middle);
            float dist = dir.magnitude;
            RaycastHit hit;
            if (!Physics.Raycast(middle, dir, out hit, dist))
            {
                stateIfFind.BlockPos = positions[i];
                return stateIfFind;
            }
        }

        return null;
    }
}
