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

namespace UnityVRScripts {
    [RequireComponent(typeof(Rigidbody))]
    public class SpiderController : MonoBehaviour {
        private GameObject wayPoint;
        private Vector3 wayPointPos;
        private Vector3 objectSelfPos;

        public AudioSource bite, chatter, boom;

        private float dist;

        //  I am speeed vvvvvv
        private float speed = 1.5f;
        private Rigidbody rb;
        private bool jumped = false;

        public bool latched = false;
        public GameObject latchedObject = null;
        public float latchedTime = 0;
        public float timeBetweenLatches;
        public float lastLatch = 0.0f;
        private bool shouldMove = true;

        public float latchingTime;

        public float timeBetweenJumps;
        public float timeLastedJump;

        SpiderSpawner spiderSpawner;

        public GameObject animatorChild;

        void Start() {
            float size = Random.Range(0.1f, 0.2f);
            gameObject.transform.localScale += new Vector3(size, size, size);
            rb = GetComponent<Rigidbody>();
            if (Random.Range(0, 2) == 0) {
                wayPoint = GameObject.Find("LeftBaseController");
            }
            else {
                wayPoint = GameObject.Find("RightBaseController");
            }
        }

        public void SetSpawner(SpiderSpawner spiderSpawner) {
            this.spiderSpawner = spiderSpawner;
        }
        
        void Update() {
            if (!latched && shouldMove) {
                objectSelfPos = transform.position;
                wayPointPos = wayPoint.transform.position;
                dist = Vector3.Distance(objectSelfPos, wayPointPos);
                if (dist <= 1 && !jumped && Time.time - timeLastedJump >= timeBetweenJumps) {
                    rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    jumped = !jumped;
                }

                transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
                transform.LookAt(wayPoint.transform);
            }
            else {
                transform.position = latchedObject.transform.position;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                rb.velocity = Vector3.zero;
                if (Time.time - latchedTime >= latchingTime) {
                    UnlatchSpider(latchedObject.gameObject);
                }
            }

        }

        public void SpiderDeath()
        {
            shouldMove = false;
            boom.Play();
            Invoke(nameof(NoSpider), 1);
        }

        void NoSpider()
        {
            hudUpdater.increaseScore(100);
            
            SpiderSpawner.DecreaseSpiderCount();
            Debug.Log("SpiderDead");
            animatorChild.GetComponent<Animator>().SetBool("Death",true);
            Destroy(gameObject);
        }

        void OnCollisionEnter(Collision collision) {
            jumped = false;
            timeLastedJump = Time.time;
        }

        private void OnTriggerEnter(Collider other) {
            if ((other.gameObject.CompareTag("LeftController") || other.gameObject.CompareTag("RightController")) &&
                Time.time - lastLatch >= timeBetweenLatches) {
                jumped = true;
                latched = true;
                hudUpdater.decreaseHealth(-3);
                latchedObject = other.gameObject;
                latchedTime = Time.time;
                rb.useGravity = false;
                ArdCom.TurnOnControllerForDuration(100, other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("LeftController") || other.gameObject.CompareTag("RightController")) {
                if (latched) {
                    UnlatchSpider(other.gameObject);
                }
            } else if (other.gameObject.CompareTag("OuterBox")) {
                spiderSpawner.SpawnSpider();
                // Debug.Log("Respawned spider");
                Destroy(gameObject);
            }
        }

        private void UnlatchSpider(GameObject controlller) {
            latched = false;
            rb.useGravity = true;
            lastLatch = Time.time;
        }
        
    }
}
