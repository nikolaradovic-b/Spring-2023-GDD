using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TransitionCondition
{
    SeePlayer,

}

[System.Serializable]
public struct AND
{
    public TransitionCondition[] condition;
}

[System.Serializable]
public struct OR
{
    public AND[] ands;
}

public class Transition : ScriptableObject
{
    [SerializeField] private OR[] conditions;
    [SerializeField] private State targetState;

    public bool IsTriggered()
    {
        foreach (var or in conditions)
        {
            // Evaluate the OR condition
            if (EvaluateOr(or))
            {
                return true;
            }
        }
        return false;
    }

    public State GetTargetState()
    {
        return targetState;
    }

    private bool EvaluateOr(OR orCond)
    {
        // There are bunch of AND conditions by construction, so they have to be all true
        foreach (var and in orCond.ands)
        {
            if (!EvaluateAnd(and))
            {
                return false;
            }
        }
        return true;
    }

    private bool EvaluateAnd(AND andCond)
    {
        switch (andCond.condition)
        {
            // Reference world states to handle each condition type!

        }
        return true;
    }
}
