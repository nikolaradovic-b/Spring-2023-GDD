using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The base state machine for all enemies
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State initialState;

    private State currentState;

    private void Start()
    {
        currentState = initialState;
    }

    // Checks and applies transitions per frame
    private void Update()
    {
        // Assume no transition is triggered
        Transition triggered = null;

        // Checks through each transition and stores the first one that triggers
        foreach (Transition t in currentState.GetTransitions())
        {
            if (t.IsTriggered(gameObject))
            {
                triggered = t;
                break;
            }
        }

        // Checks if we have a transition to fire
        if (triggered != null)
        {
            // Find the target state
            State targetState = triggered.GetTargetState();

            // Execute the exit actions of the old state, the transition action and the 
            // entry actions for the new state
            currentState.ExecuteExitActions(gameObject);
            targetState.ExecuteEntryActions(gameObject);

            // Complete the transition and return the action list
            currentState = targetState;
        }
        else
        {
            // Otherwise just execute the current state's actions
            currentState.ExecuteActions(gameObject);
        }
    }
}
