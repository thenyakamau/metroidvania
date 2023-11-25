using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace namespace1
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rdBody;
        private CapsuleCollider2D cCollider;
        private SpriteRenderer spRenderer;
        private Animator animator;

        [SerializeField] private LayerMask jumpableGround;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float jumpForce = 14f;

        private float directionX = 0f;
        private string currentState = Constants.PLAYERIDLE;
        public bool gameOver;

        // In case you decide to restore the original script before Arno's changes were added to it,
        // remove whatever comes after the expresion "Arno's line..., " or the conditional statement -
        // that percedes "Arno's block...."

        // Arno's line of code
        private GameManager _gameManagerScript;

        // Start is called before the first frame update
        private void Start()
        {
            rdBody = GetComponent<Rigidbody2D>();
            cCollider = GetComponent<CapsuleCollider2D>();
            spRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            // Arno's line of code
            _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            directionX = Input.GetAxisRaw("Horizontal");

            float velocityX = rdBody.velocity.x;
            float velocityY = rdBody.velocity.y;

            float newPosition = directionX * moveSpeed;
            rdBody.velocity = new Vector2(newPosition, velocityY);

            if (Input.GetButtonDown("Jump") && IsGrounded())
                rdBody.velocity = new Vector2(velocityX, jumpForce);

            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
                UpdateAttackState();

            UpdateMovementState();

            // Arno's block of code
            if (transform.position.y < -10)
            {
                gameOver = true;
            }
        }

        private void UpdateAttackState()
        {
            if (Input.GetButtonDown("Fire1"))
                ChangeAnimationState(Constants.PLAYERFIRSTATTACK);
            else
                ChangeAnimationState(Constants.PLAYERSHIELDNORMAL);

        }

        private void UpdateMovementState()
        {
            // Else statement not required since initial state is state
            float verticalSpeed = rdBody.velocity.y;
            string fallingState = verticalSpeed > .1f ? Constants.PLAYERJUMPING : Constants.PLAYERFALLING;

            if (directionX != 0f && IsGrounded())
                ChangeAnimationState(Constants.PLAYERRUNNING);
            else if (!IsGrounded())
                ChangeAnimationState(fallingState);
            else
            {
                // Arno's block of code
                if (!_gameManagerScript.isOnPause)
                    ChangeAnimationState(Constants.PLAYERIDLE);
            }

            // Arno's block of code
            if (!_gameManagerScript.isOnPause)
                spRenderer.flipX = directionX < 0f;
        }

        private void ChangeAnimationState(string newState)
        {
            if (currentState == newState) return;

            animator.Play(newState);
            currentState = newState;
        }

        private bool IsGrounded()
        {
            Bounds collBounds = cCollider.bounds;
            return Physics2D.BoxCast(collBounds.center, collBounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }
    }
}
