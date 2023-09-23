using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRope : MonoBehaviour
{
    public Rigidbody2D origin;
    public Material mat;
    private LineRenderer line;
    public float line_width = .1f;
    public float speed = 75;
    public float pull_force = 50;
    private Vector3 velocity;
    public float target_pull = 30;

    public float stayTime = 1f;

    private IEnumerator timer;
    private bool pull = false;
    private bool update = false;
    private Vector2 offset;
    private float mass;

    private Rigidbody2D target;
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

    public void SetStart(Vector2 targetPos)
    {
        Vector2 dir = targetPos - origin.position;
        dir = dir.normalized;
        velocity = dir * speed;
        transform.position = origin.position + dir;
        pull = false;
        update = true;

        if(timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
    }
    
    void Update()
    {
        if (!update)
        {
            return;
        }
        if (pull)
        {
            Vector2 dir = (Vector2)transform.position - origin.position;
            //Usun to na dole jesli chcesz zeby szybciej lecial
            dir = dir.normalized * 0.25f + dir * 0.75f;
            origin.AddForce(dir * pull_force);

            if (target)
            {
                target.AddForce(-dir * target_pull * mass);
                transform.position = target.position + offset;
            }
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            if(distance > 50)
            {
                update = false;
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
                return;
            }
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        update = false;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        velocity = Vector2.zero;
        pull = true;
        timer = Reset(stayTime);
        StartCoroutine(timer);

        target = collision.attachedRigidbody;
        if (target)
        {
            offset = target.position - (Vector2)transform.position;
            offset *= .5f;
            mass = target.mass;
        }
    }
}
