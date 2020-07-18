using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class BulletManager : MonoBehaviour {
        public float bulletSpeed;

        void Update() {
            transform.position += transform.up * Time.deltaTime * bulletSpeed;
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("OuterBox")) {
                Destroy(gameObject);
                Debug.Log("Cleaned bullet");
            }
        }

        private void OnCollisionEnter(Collision other) {
            Destroy(gameObject);
        }
    }
}
