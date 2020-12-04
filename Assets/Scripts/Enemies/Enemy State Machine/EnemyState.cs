using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Entity entity;

    protected string animBoolName;

    protected float startTime;

    public EnemyState(Entity entity, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }



    public virtual void Enter()
    {
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }
}
