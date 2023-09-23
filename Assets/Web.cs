using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public Rigidbody2D rb;
    public LineRenderer line;
    private Vector3 target;
    public LayerMask layer;
    public int spiderforce = -20;
    public int mouse = 0;
    public KeyCode shoot = KeyCode.F;

    public Rigidbody2D origin;

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        if (Input.GetMouseButtonDown(mouse))
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, worldPosition - transform.position, 100, layer);
            if (hit.collider != null)
            {
                target = hit.point;
                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, target);
                rb.AddForce((transform.position - target).normalized * spiderforce);
            }
        }
        else if (Input.GetMouseButton(mouse) && target != Vector3.zero)
        {

            line.SetPosition(0, transform.position);
            line.SetPosition(1, target);

            rb.AddForce((transform.position - target).normalized * spiderforce);

        }
        else
        {
            line.enabled = false;
            target = Vector3.zero;
        }
    }
}
