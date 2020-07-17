using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class BulletMovement : MonoBehaviour {
        public float bulletSpeed;

        // Update is called once per frame
        void Update() {
            transform.position += transform.up * Time.deltaTime * bulletSpeed;
        }
    }
}
