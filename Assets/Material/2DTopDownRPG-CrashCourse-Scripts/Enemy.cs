//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    _animator _animator;

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
//        _animator = GetComponent<_animator>();
//    }

//    public void Defeated()
//    {
//        _animator.SetTrigger("Defeated");
//    }

//    public void RemoveEnemy()
//    {
//        Destroy(gameObject);
//    }
//}
