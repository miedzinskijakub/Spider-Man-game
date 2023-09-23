using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private GameObject particle;
    [SerializeField] private float health;
    [SerializeField] private float flashTime = 0.1f;
    [SerializeField] private bool dead = false;
    Color originalColor;
    [SerializeField] private bool dismember = false;
    //Transform parentObject;

    private void Start()
    {
        //parentObject = GetComponentInParent<Transform>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (dismember)
        {
            Dismember();
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        FlashRed();
        if(health < 0)
        {
            IsDead();
        }
    }

    private void FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    /// <summary>
    /// enemy flash
    /// decapitate
    /// </summary>

    private void IsDead()
    {
        dead = true;
        if (dead)
        {
            Dismember();
        }
    }

    public void Dismember()
    {
        //var positionParent = gameObject.transform.parent;
        //var objectparticle = Instantiate(particle, positionParent);
        //objectparticle.transform.parent = gameObject.transform.parent;

        gameObject.GetComponent<HingeJoint2D>().enabled = false;
       gameObject.GetComponent<Health>().enabled = false;
        gameObject.GetComponent<AddForce>().enabled = false;
        var childrensScript = gameObject.GetComponentsInChildren<AddForce>();
        var childrensHealthScript = gameObject.GetComponentsInChildren<Health>();

        for (int i = 0; i < childrensScript.Length; i++)
        {
            childrensScript[i].enabled = false;
            childrensHealthScript[i].enabled = false;
        }
    }
}
