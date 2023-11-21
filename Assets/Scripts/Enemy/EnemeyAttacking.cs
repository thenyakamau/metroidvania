using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyAttacking : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float prox = 2.0f;
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
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= prox ) 
        {

            Debug.Log("we are in the attackign mode");
            anim.SetBool("isRunning", false);
            ChangeAnimationState("EnemyAttack");
        } else 
        {
            anim.SetBool("isRunning", true);
            ChangeAnimationState("enemy_walk");
        }
    }
}
