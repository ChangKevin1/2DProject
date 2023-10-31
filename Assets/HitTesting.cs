using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTesting : MonoBehaviour
    
{
    Transform transform;
    [SerializeField] GameObject damageNumber;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject healthSystem;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;
    int hitPause;
    float duration;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        healthSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(duration < 0)
        {
            healthSystem.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<SmoothCamera>().zoomOut();
            duration = 0;
        }
        else if(duration > 0) 
        {
            duration -= Time.deltaTime;
            //damageNumber.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 uiPos = new Vector3(screenPos.x + 200, screenPos.y, screenPos.z);
            healthSystem.transform.position = uiPos;
        }
        if(hitPause == 0)
        {
            Time.timeScale = 1.0f;
        }
        if(hitPause >= 0 )
            hitPause--;
    }

    public void beingAttacked()
    {
        Debug.Log("beingAttacked");
        int damage = Random.Range(1, 30);
        GetComponent<ShakeComponent>().Shake();
        GameObject.Find("Main Camera").GetComponent<ShakeComponent>().Shake();
        hitEffect.transform.position = transform.position;
        hitEffect.GetComponent<ParticleSystem>().Play();
        healthSystem.SetActive(true);
        damageNumber.GetComponent<CFXR_ParticleText>().UpdateText(damage.ToString());
        damageNumber.GetComponent<ParticleSystem>().Play();
        healthSystem.GetComponent<HealthSystem>().TakeDamage(damage);
        duration = 2;
        GameObject.Find("Main Camera").GetComponent<SmoothCamera>().zoomIn();
        Time.timeScale = 0.0f;
        hitPause = 6;
        audio.PlayOneShot(audio.clip);

    }
    
}


