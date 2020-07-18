using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class followPlayer : MonoBehaviour

{
    private GameObject wayPoint;
    private Vector3 wayPointPos;
    private Vector3 objectSelfPos;
    private float dist;
    //  I am speeed vvvvvv
    private float speed = 1.5f;
    private Rigidbody rb;
    private bool jumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        objectSelfPos = transform.position;
        wayPointPos = wayPoint.transform.position;
        dist = Vector3.Distance(objectSelfPos, wayPointPos);
        if (dist <= 1 && !jumped)
        {
            rb.AddForce(0, 400, 0);
            jumped = !jumped;
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }

    public void SpiderDeath() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            jumped = false;
        }
    }
}
