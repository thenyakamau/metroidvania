using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyAttacking : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float prox = 1.0f;
    private string currentState = "enemy_idle";
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);
        currentState = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x - transform.position.x <= prox) 
        {
            ChangeAnimationState("EnemyAttack");
        } else 
        {
            ChangeAnimationState("enemy_walk");
        }
    }
}
