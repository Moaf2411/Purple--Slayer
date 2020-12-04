using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public EnemyStateMachine stateMachine;

  public float facingDirection;


  private Vector2 workSpace;
  private Vector3 workSpace3;

  public Rigidbody2D rb { get; private set; }
  
  public Animator animator { get; private set; }
  
  public GameObject enemyGO { get; private set; }

  [SerializeField] public D_Entity entityData;


  public virtual void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    
    stateMachine = new EnemyStateMachine();

    enemyGO = gameObject;

    facingDirection = enemyGO.transform.localScale.x;
  }



  public virtual void Update()
  {
    stateMachine.currentState.LogicUpdate();
  }

  public virtual void FixedUpdate()
  {
    stateMachine.currentState.PhysicsUpdate();
  }

  public void SetVelocityX(float vel)
  {
    workSpace.Set(facingDirection * vel,rb.velocity.y);
    rb.velocity = workSpace;
  }

  public void SetVelocityY(float vel)
  {
    workSpace.Set(rb.velocity.x,facingDirection * vel);
    rb.velocity = workSpace;
  }

  public void SetVelocity(Vector2 vel)
  {
    workSpace.Set(vel.x , vel.y);
    rb.velocity = workSpace;
  }

  public bool CheckForPlayerInRange(float radius)
  {
    return Physics2D.OverlapCircle(transform.position, radius, entityData.whatIsPlayer);
  }

  

  public Collider2D GetPlayerCollider(float radius)
  {
    return Physics2D.OverlapCircle(transform.position, radius, entityData.whatIsPlayer);
  }

  public void Flip()
  {
   
    facingDirection *= -1;
    workSpace3.Set(facingDirection,1,1);
    enemyGO.transform.localScale = workSpace3;
    
  }
  
  public bool WallCheck(Transform wallCheck,float distance)
  {
    return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, distance,
      entityData.whatIsGround);
  }

  public bool GroundCheck(Transform groundCheck, float distance)
  {
    return Physics2D.Raycast(groundCheck.position, Vector2.down, distance, entityData.whatIsGround);
  }
  
  
  
  
  
  
}
