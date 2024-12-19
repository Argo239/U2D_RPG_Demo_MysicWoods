//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    Animator animator;

//    public float HP
//    {
//        set
//        {
//            maxHealth = value;

//            if (maxHealth <= 0)
//            {
//                Defeated();
//            }
//        }
//        get
//        {
//            return maxHealth;
//        }
//    }

//    public float maxHealth = 1;

//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    public void Defeated()
//    {
//        animator.SetTrigger("Defeated");
//    }

//    public void RemoveEnemy()
//    {
//        Destroy(gameObject);
//    }
//}
