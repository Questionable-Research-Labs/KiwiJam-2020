using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class SpiderSpawner : MonoBehaviour {
        public GameObject spiderPrefab = null;

        private void Start() {
            for (int i = 0; i < 2; i++) {
                Instantiate(spiderPrefab, new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), 1.0f, UnityEngine.Random.Range(-10.0f, 10.0f)),Quaternion.identity );
            }
        }
    }
}
