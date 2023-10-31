using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListener : MonoBehaviour
{
    public Boolean attackTrigger = false;
    public HitTesting hitOne = null;
    public Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("enemy") )
        {
            hitOne = other.GetComponent<HitTesting>();
        }       
    }
    private void Update()
    {
        if(hitOne != null && attackTrigger == true)
        {
            hitOne.beingAttacked();
            attackTrigger = false;
            hitOne = null;
            collider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitOne = null;
    }

    public void startAttack()
    {
        collider.enabled = true;
    }

}
