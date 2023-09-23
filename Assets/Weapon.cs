using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float mag;
    [SerializeField] private float velocityDamage = 25;
    [SerializeField] private float damage;
    private float critical = 0.95f;
    private float criticalSpeed = 100f;

    public LayerMask itemLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            critical -= 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            criticalSpeed -= 1;
        }
        //Debug.Log(mag);    
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mag = collision.relativeVelocity.magnitude;

        if(collision.gameObject.layer == 7)
        {
       
        if(mag > velocityDamage)
        {
            var health = collision.gameObject.GetComponent<Health>();
            if (health)
            {
                health.GetDamage(damage * mag);
            }
            

            if(Random.value > critical && mag > criticalSpeed)
            {
                health.Dismember();
            }
            
        }
    }
    }
}
