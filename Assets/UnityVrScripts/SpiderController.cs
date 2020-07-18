using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityVRScripts;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class SpiderController : MonoBehaviour

{
    private GameObject wayPoint;
    private Vector3 wayPointPos;
    private Vector3 objectSelfPos;
    private float dist;
    //  I am speeed vvvvvv
    private float speed = 1.5f;
    private Rigidbody rb;
    private bool jumped = false;

    private bool latched = false;
    private GameObject latchedObject = null;
    private float latchedTime = 0;

    public float latchingTime;

    void Start() {
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

    void Update() {
        if (!latched) {
            objectSelfPos = transform.position;
            wayPointPos = wayPoint.transform.position;
            dist = Vector3.Distance(objectSelfPos, wayPointPos);
            if (dist <= 1 && !jumped)
            {
                rb.AddForce(0, 5, 0, ForceMode.Impulse);
                jumped = !jumped;
            }
            transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
        }
        else {
            transform.position = latchedObject.transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f));
            rb.velocity = Vector3.zero;
            if (Time.time - latchedTime >= latchingTime) {
                latched = false;
                ArdCom.TurnOnControllerForDuration(100, latchedObject.gameObject);
                rb.useGravity = true;
            }
        }

    }

    public void SpiderDeath() {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        jumped = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("LeftController")) {
            jumped = true;
            latched = true;
            latchedObject = other.gameObject;
            latchedTime = Time.time;
            rb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("LeftController")) {
            if (latched) {
                latched = false;
                ArdCom.TurnOnControllerForDuration(100, other.gameObject);
                rb.useGravity = true;
            }
        }
    }
}
