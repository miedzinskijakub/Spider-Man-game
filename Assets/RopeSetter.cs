using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    public SpiderRope[] spiderRope;
    private int index = 0;
    [SerializeField]private KeyCode key;
    
    void Update()
    {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(key))
        {
            spiderRope[index].SetStart(worldPos);
            index++;
            if(index > spiderRope.Length - 1)
            {
                index = 0;
            }
        }
        if (Input.GetKey(key))
        {
            spiderRope[index].SetStart(worldPos);
            index++;
            if (index > spiderRope.Length - 1)
            {
                index = 0;
            }
        }
    }
}
