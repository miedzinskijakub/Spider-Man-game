using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRope : MonoBehaviour
{
    public Rigidbody2D origin;
    public Material mat;
    private LineRenderer line;
    public float line_width = .1f;
    public float speed = 75;
    public float pull_force = 50;
    private Vector3 velocity;
    public float target_pull = 30;

    private bool isAllowedToTrigger = true;
    private bool isReachTheObject = false;
    private bool makeLine = false;
    private Vector2 offset;
    private float mass;

    [SerializeField] private KeyCode key;
    [SerializeField] private KeyCode pull_key;
    private Rigidbody2D targetRigidBody;
    private Rigidbody2D storedTargetRigidBody;
    void Start()
    {
        line = GetComponent<LineRenderer>();

        if (!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.startWidth = line_width;
        line.endWidth = line_width;
        line.material = mat;
        
    }

    public void SetStart(Vector2 mousePos)
    {
        Vector2 dir = mousePos - origin.position;
        dir = dir.normalized;
        velocity = dir * speed;
        transform.position = origin.position + dir;
        isAllowedToTrigger = true;
        isReachTheObject = false;
        makeLine = true;

    }

    void Update()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.A))
        {
            target_pull += 1;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            pull_force += 1;
        }

        if (Input.GetKeyDown(key))
        {
            SetStart(worldPos);
            
            if (Input.GetKey(key))
            {
                //Vector2 dir = worldPos - origin.position;
                //dir = dir.normalized;
                //velocity = dir * speed;
                //transform.position = origin.position + dir;


                //makeLine = true;
                //line.SetPosition(0, transform.position);
                //line.SetPosition(1, origin.position);
            }
        }
        else if(Input.GetKeyUp(key))
        {
            line.SetPosition(0, Vector2.zero);
            line.SetPosition(1, Vector2.zero);
            makeLine = false;

        }
       
        if (!makeLine)
        {
            return;
        }


        if (isReachTheObject && Input.GetKey(key))
        {
            isAllowedToTrigger = false;
                //storedTargetRigidBody = targetRigidBody;

            Vector2 dir = (Vector2)transform.position - origin.position;
            //Usun to na dole jesli chcesz zeby szybciej lecial
            dir = dir.normalized * 0.25f + dir * 0.75f;
            origin.AddForce(dir * pull_force);

            if (targetRigidBody)
            {
                targetRigidBody.AddForce(-dir * target_pull ); // pomnó¿ * mass
                transform.position = targetRigidBody.position + offset;
            }
            
        }else if(isReachTheObject && Input.GetKeyUp(key))
        {
            line.SetPosition(0, Vector2.zero);
            line.SetPosition(1, Vector2.zero);
            makeLine = false;
            isAllowedToTrigger = false;

            return;
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            line.SetPosition(0, Vector2.zero);
            line.SetPosition(1, Vector2.zero);

           // makeLine = false;
            if (distance > 50)
            {
                makeLine = false;
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
                return;
            }
            
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAllowedToTrigger)
        {
            
            velocity = Vector2.zero;
            isReachTheObject = true;

            targetRigidBody = collision.attachedRigidbody;

            if (targetRigidBody)
            {
                offset = targetRigidBody.position - (Vector2)transform.position;
                offset *= .5f;
                mass = targetRigidBody.mass;
            }
        }
        
    }
}
