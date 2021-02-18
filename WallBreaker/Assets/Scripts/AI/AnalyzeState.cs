using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Analyze", order = 1)]
public class AnalyzeState : State
{
    [SerializeField]
    GoToRepareState stateIfFind = null;

    public override State UpdateState()
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
