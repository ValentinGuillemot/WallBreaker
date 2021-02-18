using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    protected Builder _ai;

    public virtual void InitState(Builder ai)
    {
        _ai = ai;
    }

    public virtual State UpdateState()
    {

        return null;
    }
}
