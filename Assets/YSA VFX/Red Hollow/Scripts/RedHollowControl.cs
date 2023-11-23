using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHollowControl : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float hue = 0;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<HueControl>().hue = hue;
    }

    public void Play_Charging() {
        GameObject.Find("sfx paper").GetComponent<AudioSource>().Play();
        animator.Play("Red Hollow - Charging");
    }

    public void Finish_Charging() {
        animator.Play("Red Hollow - Charged");
    }

    public void Burst_Beam() {
        animator.Play("Red Hollow - Burst");
        GameObject.Find("30_Earth_02").GetComponent<AudioSource>().Play();
        GameObject.Find("Main Camera").GetComponent<ShakeComponent>().Shake(3f,0.1f,0.2f);
    }

    public void Dead()
    {
        animator.Play("Red Hollow - Dead");
    }
}
