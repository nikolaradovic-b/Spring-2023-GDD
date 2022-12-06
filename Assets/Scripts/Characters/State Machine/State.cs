using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Patrol,
    Chase,
    Fire,
    BossProtect
}

[CreateAssetMenu(menuName = "StateMachine/State")]
public class State : ScriptableObject
{
    [SerializeField] private EnemyState state;
    [SerializeField] private Transition[] transitions;

    public virtual void ExecuteActions(GameObject entity)
    {
        switch (state)
        {
            case EnemyState.Patrol:
                entity.GetComponent<EnemyMovement>().ExecutePatrolState();
                break;
            case EnemyState.Fire:
                entity.GetComponentInChildren<EnemyBase>().ExecuteFireState();
                break;
            case EnemyState.Chase:
                entity.GetComponentInChildren<EnemyBase>().ExecuteChaseState();
                break;
            case EnemyState.BossProtect:
                entity.GetComponent<EnemyMovement>().StopMovement();
                break;
            default:
                break;
        }
    }

    public virtual void ExecuteEntryActions(GameObject entity)
    {
        switch (state) 
        {
            case EnemyState.Fire:
                entity.GetComponentInChildren<EnemyBase>().SetAttacking(true);
                break;
            default:
                break;
        }
    }

    public virtual void ExecuteExitActions(GameObject entity)
    {
        switch (state)
        {
            case EnemyState.Fire:
                entity.GetComponentInChildren<EnemyBase>().SetAttacking(false);
                break;
            default:
                break;
        }
    }

    public Transition[] GetTransitions()
    {
        return transitions;
    }
}
