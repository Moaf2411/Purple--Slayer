using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] public List<Vector3> positions;
    private Queue<Vector3> positionsQueue;

    [Header("Variables")]
    
    [SerializeField] private float speed;
    [SerializeField] private float hesitate;

    public Rigidbody2D rb{get;  private set; }

    private Vector2 workSpace,dir;
    private Vector3 targetPosition;

    private bool set;
    private float setTime;

   

    

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        positionsQueue=new Queue<Vector3>(positions.Count);
        for (int i = 0; i < positions.Count; i++)
        {
            positionsQueue.Enqueue(positions[i]);
        }

       
    }

    
    
    
    
    
    private void Start()
    {
        targetPosition = positionsQueue.Dequeue();
        positionsQueue.Enqueue(targetPosition);
        
        
        
        dir.Set(targetPosition.x - transform.position.x , targetPosition.y - transform.position.y);
        dir.Normalize();
        
        SetVelocity(dir,speed);

    }

    
    
    
    
    
    
    private void SetVelocity(Vector2 dir, float speed)
    {
        workSpace.Set(dir.x * speed , dir.y * speed);
        rb.velocity = workSpace;
    }

    
    
    
    
    
    private void Update()
    {
       
        if (  Mathf.Sign(dir.x) != Mathf.Sign( targetPosition.x - transform.position.x) ||  Mathf.Sign(dir.y) != Mathf.Sign( targetPosition.y - transform.position.y) || dir == Vector2.zero )
        {
            
            
            // when reached the target position, go to next target
            // or if the starting target was your current position (direction was 0) also go to the next position
            
            targetPosition = positionsQueue.Dequeue();
            positionsQueue.Enqueue(targetPosition);
            dir.Set(targetPosition.x - transform.position.x , targetPosition.y - transform.position.y);
            dir.Normalize();

            set = true;
            setTime = Time.time;
            
            rb.velocity=Vector2.zero;


        }

        if (Time.time > setTime + hesitate && set)
        {
            SetVelocity(dir,speed);
            set = false;
        }
        
       
        
    }

    
    
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(positions[i] , 0.2f);
        }
    }
}
