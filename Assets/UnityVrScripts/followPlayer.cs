using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour

{
    private GameObject wayPoint;
    private Vector3 wayPointPos;
    //  I am speeed vvvvvv
    private float speed = 6.0f;
    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            wayPoint = GameObject.Find("LeftBaseController");
        }
        else
        {
            wayPoint = GameObject.Find("RightBaseController");
        }
        
    }

    void Update()
    {
        wayPointPos = new Vector3(wayPoint.transform.position.x, transform.position.y, wayPoint.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }
}
