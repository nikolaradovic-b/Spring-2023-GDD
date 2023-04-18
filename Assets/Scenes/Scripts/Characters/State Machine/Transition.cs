using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionCondition
{
    SeePlayer,
    PlayerInAttackRange,
    TargetInProtectionRange,
    BossAttack,
    BossProtect
}

[System.Serializable]
public struct ConditionContainer
{
    public TransitionCondition cond;
    public bool negated;
}

[System.Serializable]
public struct OR
{
    public ConditionContainer[] ands;
}

[CreateAssetMenu(menuName = "StateMachine/Transition")]
public class Transition : ScriptableObject
{
    [SerializeField] private OR[] conditions;
    [SerializeField] private State targetState;

    private GameObject objChecked;

    public bool IsTriggered(GameObject obj)
    {
        objChecked = obj;
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

    private bool EvaluateAnd(ConditionContainer andCond)
    {
        switch (andCond.cond)
        {
            // Reference world states to handle each condition type!
            case TransitionCondition.SeePlayer:
                bool canSee = PollingMachine.Instance.CanSeePlayer(objChecked,
                        objChecked.GetComponentInChildren<EnemyBase>().GetSeePlayerRange());
                if (andCond.negated)
                {
                    return !canSee;
                }
                else
                {
                    return canSee;
                }
            case TransitionCondition.PlayerInAttackRange:
                bool canAttack = PollingMachine.Instance.TargetInAttackRange(objChecked,
                    PollingMachine.Instance.GetPlayer(), 
                    objChecked.GetComponentInChildren<EnemyBase>().GetAttackRange());
                if (andCond.negated)
                {
                    return !canAttack;
                }
                else
                {
                    return canAttack;
                }
            case TransitionCondition.TargetInProtectionRange:
                bool canProtect = PollingMachine.Instance.TargetInAttackRange(objChecked,
                    objChecked.GetComponentInChildren<EnemyShaman>().GetClosestEnemy(),
                    objChecked.GetComponentInChildren<EnemyBase>().GetAttackRange());
                if (andCond.negated)
                {
                    return !canProtect;
                }
                else
                {
                    return canProtect;
                }
            case TransitionCondition.BossAttack:
                bool bossAttack = PollingMachine.Instance.CanBossAttack(objChecked.GetComponentInChildren<Boss>());
                if (andCond.negated)
                {
                    return !bossAttack;
                }
                else
                {
                    return bossAttack;
                }
            case TransitionCondition.BossProtect:
                bool bossProtect = PollingMachine.Instance.GetBossProtect(objChecked.GetComponentInChildren<Boss>());
                if (andCond.negated)
                {
                    return !bossProtect;
                }
                else
                {
                    return bossProtect;
                }
            default:
                return false;
        }
    }
}
