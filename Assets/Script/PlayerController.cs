using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    bool canMove = true;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", success);
                animator.SetFloat("MoveX", movementInput.x);
                animator.SetFloat("MoveY", movementInput.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        ////¾«ÁéÍ¼·­×ª
        //if (movementInput.x < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else if (movementInput.x > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}
    }

    /// <summary>
    /// ÒÆ¶¯
    /// </summary>
    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }


    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>(); 
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();
        float InputX = animator.GetFloat("MoveX");
        float InputY = animator.GetFloat("MoveY");
        if (InputY > 0)
        {
            Debug.Log("UP");
            swordAttack.AttackUp();
        }
        else if (InputY < 0)
        {
            Debug.Log("Down");
            swordAttack.AttackDown();
        }
        else if (InputX < 0)
        {
            Debug.Log("Left");
            swordAttack.AttackLeft();
        }
        else if (InputX > 0)
        {
            Debug.Log("Right");
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnLockMovement();
        swordAttack.StopAttack(); 
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnLockMovement()
    {
        canMove = true;
    }

}
