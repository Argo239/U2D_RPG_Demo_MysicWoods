//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.Windows;

//public class Players : MonoBehaviour {

//    public float collisionOffset = 0.02f;
//    public ContactFilter2D movementFilter;
//    public SwordAttack swordAttack;

//    private Animator animator;
//    private Rigidbody2D rb;
//    private SpriteRenderer spriteRenderer;


//    Vector2 movementInput;
//    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


//    public static Player Instance { get; private set; }

//    [SerializeField] private float moveSpeed = 1f;

//    private bool isMoving;   

//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }
   
//    private void Updata()
//    {
//        bool canMove = 

//        if (canMove)
//        {
//            if (movementInput != Vector2.zero)
//            {
//                bool success = TryMove(movementInput);
//                if (!success)
//                {
//                    success = TryMove(new Vector2(movementInput.x, 0));
//                }
//                if (!success)
//                {
//                    success = TryMove(new Vector2(0, movementInput.y));
//                }
//                animator.SetBool("isMoving", success);
//                animator.SetFloat("MoveX", movementInput.x);
//                animator.SetFloat("MoveY", movementInput.y);
//                moveSpeed = 1f;
//            }
//            else
//            {
//                animator.SetBool("isMoving", false);
//            }
//        }
//    }

//    /// <summary>
//    /// ÒÆ¶¯
//    /// </summary>
//    private bool TryMove(Vector2 direction)
//    {
//        if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
//            moveSpeed = 2f;
//        int count = rb.Cast(
//            direction,
//            movementFilter,
//            castCollisions,
//            moveSpeed * Time.fixedDeltaTime + collisionOffset);
//        if (count == 0)
//        {
//            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }


//    void OnMove(InputValue movementValue)
//    {
//        movementInput = movementValue.Get<Vector2>(); 
//    }

//    void OnFire()
//    {
//        animator.SetTrigger("swordAttack");
//        animator.SetBool("isAttack", true);
//    }

//    public void SwordAttack()
//    {
//        LockMovement();
//        float InputX = animator.GetFloat("MoveX");
//        float InputY = animator.GetFloat("MoveY");
//        if (InputY > 0)
//        {
//            Debug.Log("UP");
//            swordAttack.AttackUp();
//            animator.SetBool("isAttack", false);
//            EndSwordAttack();
//        } else if (InputY < 0)
//        {
//            Debug.Log("Down");
//            swordAttack.AttackDown();
//            animator.SetBool("isAttack", false);
//            EndSwordAttack();

//        } else if (InputX < 0)
//        {
//            Debug.Log("Left");
//            swordAttack.AttackLeft();
//            animator.SetBool("isAttack", false);
//            EndSwordAttack();
//        } else if (InputX > 0)
//        {
//            Debug.Log("Right");
//            swordAttack.AttackRight();
//            animator.SetBool("isAttack", false);
//            EndSwordAttack();
//        }
//    }

//    public void EndSwordAttack()
//    {
//        UnLockMovement();
//        swordAttack.StopAttack(); 
//    }

//    public void LockMovement()
//    {
//        canMove = false;
//    }

//    public void UnLockMovement()
//    {
//        canMove = true;
//    }
//}
