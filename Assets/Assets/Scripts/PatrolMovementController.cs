using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 3f;
    [SerializeField] private LayerMask playerLayer;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool playerInSight = false;
    private float OriginalVelocity;
    private float raycastDistance = 3f;

    private void Start() 
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
        OriginalVelocity = velocityModifier;

    }
    private void Update() 
    {
        CheckNewPoint();
        checkPlayer();
        if (playerInSight)
        {
            velocityModifier = OriginalVelocity * 2;
            followPlayer();
        }
        else
        {
            velocityModifier = OriginalVelocity;
        }

    }
    private void CheckNewPoint()
    {
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = (patrolPos + 1) % checkpointsPatrol.Length;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }      
    }
    void checkPlayer()
    {
        Vector2 direct = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, 10f, playerLayer);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            Debug.DrawRay(transform.position, direct * raycastDistance, Color.white);
            playerInSight = true;
        }
        else
        {
            Debug.DrawRay(transform.position, direct * raycastDistance, Color.black);
            playerInSight = false;
        }
    }
    void followPlayer()
    {
        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        CheckFlip(myRBD2.velocity.x);

    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}