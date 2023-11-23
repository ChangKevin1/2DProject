using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AttackListener : MonoBehaviour
{
    public bool attackTrigger = false;
    public HitTesting hitOne = null;
    public Collider2D collider;
    public bool sustained = false;
    public float cooldownDelta = 0;
    public float sustainedDelta = 0;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("enemy") )
        {
            attackTrigger = true;
            hitOne = other.GetComponent<HitTesting>();
        }       
    }
    private void Update()
    {
        if( sustainedDelta > 0  )
        {
            sustainedDelta -= Time.deltaTime;
            if (sustainedDelta <= 0)
            {
                attackTrigger = false;
                sustainedDelta = 0;
            }
        }
        if (cooldownDelta > 0)
        {
            cooldownDelta = cooldownDelta - Time.deltaTime;
            if (cooldownDelta < 0)
                cooldownDelta = 0;
        }
        if (hitOne != null && attackTrigger == true && !sustained)
        {
            hitOne.beingAttacked();
            attackTrigger = false;
            hitOne = null;
            collider.enabled = false;
        }
        if (hitOne != null && attackTrigger &&this.isActiveAndEnabled == true && sustained && cooldownDelta <= 0 && sustainedDelta >= 0)
        {
            if (sustainedDelta == 0)
                sustainedDelta = 2.5f;
            hitOne.beingAttacked();
            cooldownDelta = 0.25f;
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
