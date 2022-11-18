using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    [SerializeField] protected Transition[] transitions;

    public virtual void ExecuteActions(GameObject entity)
    {

    }

    public virtual void ExecuteEntryActions(GameObject entity)
    {

    }

    public virtual void ExecuteExitActions(GameObject entity)
    {

    }

    public Transition[] GetTransitions()
    {
        return transitions;
    }
}
