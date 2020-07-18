using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class BulletManager : MonoBehaviour {
        public float bulletSpeed;

        private void Start() {
            
        }

        void Update() {
            transform.position += transform.up * Time.deltaTime * bulletSpeed;
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("OuterBox")) {
                Destroy(gameObject);
                Debug.Log("Cleaned bullet");
            }
        }
    }
}
