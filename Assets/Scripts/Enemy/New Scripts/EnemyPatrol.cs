using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializedField] private Transform leftEdge;
    [SerializedField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializedField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializedField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializedField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializedField] private animator anim;

    private void Awake() 
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("isRunning", false);
    }

    private void Update() 
    {
        if (movingLeft) 
        {
            if(movingLeft.x >= leftEdge.position.x)
            MoveInDirection(-1);
            else 
                DirectionChange();
        }
        else 
        {
            if (movingLeft.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }

        private void DirectionChange() 
        {
            anim.SetBool("isRunning", false);
             idleTimer += Time.deltaTime;

            if(idleTimer > idleDuration)
                movingLeft = !movingLeft;
        }
        
    }

    private void MoveInDirection(int _direction) 
    {
        idleTimer = 0;
        anim.SetBool("isRunning", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
