using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class BulletManager : MonoBehaviour {
        public float bulletSpeed;
		AudioSource bulletAudioSource;

		void Start() {
			bulletAudioSource = GetComponent<AudioSource>();
			bulletAudioSource.Play();
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

        private void OnCollisionEnter(Collision other) {
            Destroy(gameObject);
            if (other.gameObject.CompareTag("Spider")) {
                other.gameObject.GetComponent<SpiderController>().SpiderDeath();
            }
        }
    }
}
