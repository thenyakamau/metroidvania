using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private bool isWalking = true;
    private string currentState = "enemy_idle";

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = Waypoint1.transform;
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == Waypoint1.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Waypoint2.transform)
        {
            flip();
            currentPoint = Waypoint1.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == Waypoint1.transform)
        {
            flip();
            currentPoint = Waypoint2.transform;
        }

        if (isWalking) 
        {
            ChangeAnimationState("enemy_walk");
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Waypoint1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(Waypoint2.transform.position, 0.5f);
        Gizmos.DrawLine(Waypoint1.transform.position, Waypoint2.transform.position); 
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);
        currentState = newState;
    }
}
