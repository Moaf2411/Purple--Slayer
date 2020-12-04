using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    
    public PlayerJumpState jumpState { get; private set; }
    
    public PlayerInAirState inAirState { get; private set; }
    
    public PlayerAttackState attackState { get; private set; }
    
    public PlayerCrouch crouchState { get; private set; } 
    
    public PlayerCrouchAttack crouchAttackState { get; private set; }



    [SerializeField] public PlayerData playerData;

    #endregion
    
    #region Player components
    
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    

    public PlayerInputHandler inputHandler;
    
    
    
    
    
    #endregion

    #region Other Variables

    public int facingDirection { get; private set; }

    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public Transform attackPosition;
    
    

    private Vector2 tempVector2;
    private Vector3 tempVector3;

   

    #endregion

    #region Unity Functions

    private void Awake()
    {
        StateMachine=new PlayerStateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
        
        
        
        idleState=new PlayerIdleState(this,StateMachine,playerData,"idle");
        moveState=new PlayerMoveState(this,StateMachine,playerData,"move");
        jumpState=new PlayerJumpState(this,StateMachine,playerData,"inAir");
        inAirState=new PlayerInAirState(this,StateMachine,playerData,"inAir");
        attackState=new PlayerAttackState(this,StateMachine,playerData,"attack");
        crouchState=new PlayerCrouch(this,StateMachine,playerData,"crouch");
        crouchAttackState=new PlayerCrouchAttack(this,StateMachine,playerData,"attack");

        facingDirection = 1;
    }

    private void Start()
    {
        StateMachine.Initialize(idleState);
        jumpState.ResetJump();
        
    }

    private void Update()
    {
        StateMachine.currentState.LogicUpdate();

      
     
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }

    #endregion

    #region Checks

    public bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius,
            playerData.whatIsGround);
    }

    public bool WallCheck()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right*facingDirection,playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public void CheckFlip(float x)
    {
        if (Mathf.Sign(x) != facingDirection && Mathf.Abs(x)>Mathf.Epsilon)
        {
            Flip();
        }
    }

    #endregion


    #region Setters

    public void SetVelocityX(float x)
    {
        tempVector2.Set(x , rb.velocity.y);
        rb.velocity = tempVector2;
    }

    public void SetVelocityY(float y)
    {
        tempVector2.Set(rb.velocity.x,y);
        rb.velocity = tempVector2;
    }
    

    #endregion

    #region Animation Functions
    
    public void AnimationFinished()
    {
        StateMachine.currentState.AnimationFinished();
    }

    public void TriggerAnimation()
    {
        StateMachine.currentState.TriggerAnimation();
    }
    

    #endregion

    #region Other Functions

    public void Flip()
    {
        facingDirection *= -1;
        tempVector3.Set(facingDirection,1,1);
        gameObject.transform.localScale = tempVector3;

    }
    
    

  

   
    
    

    #endregion

    #region Combat Related Functions

    public void Attack()
    {
        playerData.attackDetails.damageAmount = playerData.damageAmount;
        playerData.attackDetails.damagePosition = rb.position;
        
        Collider2D[] damageables = Physics2D.OverlapCircleAll(attackPosition.position, playerData.attackRadius,
            playerData.whatIsDamageable);

        foreach (Collider2D damaged in damageables)
        {
            
            damaged.transform.SendMessage("Damage",playerData.attackDetails);
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,playerData.groundCheckRadius);
        Gizmos.DrawWireSphere(attackPosition.position,playerData.attackRadius);
        Gizmos.DrawLine(wallCheck.position,new Vector2(wallCheck.position.x+(facingDirection*playerData.wallCheckDistance),wallCheck.position.y));
    }
}
