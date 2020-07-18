using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace UnityVRScripts {
    public class SpiderSpawner : MonoBehaviour {
        public GameObject spiderPrefab = null;
        public Vector3[] spawnLocations;

        public int[] waveSpiderAmounts;
        public float[] waveSpiderTimes;
        public int spiderSpawnDelay;
        public int currentWave = 0;

        public static int spiderCount = 0;

        private float lastWaveTime;
        private float lastSpawnTime;
        
        private bool currentlySpawning;
        private int spawningProgress;
        private void Start() {
            for (int i = 0; i < 10; i++) {
                Instantiate(spiderPrefab, new Vector3(UnityEngine.Random.Range(3.0f, 8.0f), -3.0f, UnityEngine.Random.Range(-10.0f, 10.0f)),Quaternion.identity );
            }
        }

        private void SpawnSpider() {
            Instantiate(spiderPrefab, spawnLocations[UnityEngine.Random.Range(0,spawnLocations.Length)],Quaternion.identity );
        }

        private void Update() {
            if (currentlySpawning && Time.time - lastSpawnTime > spiderSpawnDelay) {
                SpawnSpider();
                lastSpawnTime = Time.time;
            } else if (spiderCount <= 1 && Time.time - lastWaveTime > waveSpiderTimes[currentWave]) {
                currentlySpawning = true;
                lastWaveTime = Time.time;
                spawningProgress = 0;
                currentWave++;
            }
        }
    }
}
