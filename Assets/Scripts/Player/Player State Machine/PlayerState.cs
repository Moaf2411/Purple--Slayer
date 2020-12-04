using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    private string animBoolName;
    protected float startTime;
    protected bool isAnimationFinished;
    

    public PlayerState(Player player,PlayerStateMachine stateMachine,PlayerData stateData,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

    }


    public virtual void Enter()
    {
        Check();
        player.anim.SetBool(animBoolName,true);
        startTime = Time.unscaledTime;

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName,false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        Check();
        
        
    }

    public virtual void Check()
    {
        
    }

    public virtual void AnimationFinished()
    {      
    }

    public virtual void TriggerAnimation()
    {     
    }
    
}
