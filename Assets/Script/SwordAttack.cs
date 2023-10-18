using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3f;
    Vector2 Attackoffset;

    // Start is called before the first frame update
    private void Start()
    {
        Attackoffset = transform.position;
    }

    public void AttackUp()
    {
        //X 0 Y -0.1
        Debug.Log("Up");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(0, -0.1f);
    }

    public void AttackDown()
    {
        //X 0 Y -0.12                    
        Debug.Log("Down");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(0, -0.12f);
    }

    public void AttackLeft()
    {
        //X -0.08 Y -0.09
        Debug.Log("Left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(-0.08f, -0.09f);
    }

    public void AttackRight()
    {
        //X 0.08 Y -0.09
        Debug.Log("Right");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(0.08f, -0.09f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy != null )
            {
                enemy.Health -= damage;
            }
        }
    }

}
