//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//// Takes and handles input and movement for a player character
//public class PlayerController : MonoBehaviour
//{
//    public float moveSpeed = 1f;
//    public float collisionOffset = 0.05f;
//    public ContactFilter2D movementFilter;
//    public SwordAttack swordAttack;

//    Vector2 _inputVector;
//    SpriteRenderer spriteRenderer;
//    Rigidbody2D rb;
//    _animator _animator;
//    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

//    bool canMove = true;

//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        _animator = GetComponent<_animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }

//    private void FixedUpdate()
//    {
//        if (canMove)
//        {
//            // If movement input is not 0, try to move
//            if (_inputVector != Vector2.zero)
//            {

//                bool success = TryMove(_inputVector);

//                if (!success)
//                {
//                    success = TryMove(new Vector2(_inputVector.x, 0));
//                }

//                if (!success)
//                {
//                    success = TryMove(new Vector2(0, _inputVector.y));
//                }

//                _animator.SetBool("isMoving", success);
//            }
//            else
//            {
//                _animator.SetBool("isMoving", false);
//            }

//            // Set direction of sprite to movement direction
//            if (_inputVector.x < 0)
//            {
//                spriteRenderer.flipX = true;
//            }
//            else if (_inputVector.x > 0)
//            {
//                spriteRenderer.flipX = false;
//            }
//        }
//    }

//    private bool TryMove(Vector2 direction)
//    {
//        if (direction != Vector2.zero)
//        {
//            // Check for potential collisions
//            int count = rb.Cast(
//                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
//                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
//                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
//                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an Offset

//            if (count == 0)
//            {
//                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        else
//        {
//            // Can't move if there's no direction to move in
//            return false;
//        }

//    }

//    void OnMove(InputValue movementValue)
//    {
//        _inputVector = movementValue.Get<Vector2>();
//    }

//    void OnFire()
//    {
//        _animator.SetTrigger("swordAttack");
//    }

//    public void SwordAttack()
//    {
//        LockMovement();

//        if (spriteRenderer.flipX == true)
//        {
//            swordAttack.AttackLeft();
//        }
//        else
//        {
//            swordAttack.AttackRight();
//        }
//    }

//    public void EndSwordAttack()
//    {
//        UnlockMovement();
//        swordAttack.StopAttack();
//    }

//    public void LockMovement()
//    {
//        canMove = false;
//    }

//    public void UnlockMovement()
//    {
//        canMove = true;
//    }
//}
