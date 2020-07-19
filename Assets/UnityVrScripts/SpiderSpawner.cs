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

        public int test;

        public void SpawnSpider() {
            Debug.Log("");
            Instantiate(spiderPrefab, spawnLocations[UnityEngine.Random.Range(0,spawnLocations.Length)],Quaternion.identity );
        }

        private void Update() {
            if (currentlySpawning && Time.time - lastSpawnTime > spiderSpawnDelay) {
                SpawnSpider();
                lastSpawnTime = Time.time;
                spiderCount++;
                spawningProgress++;
                if (spawningProgress >= waveSpiderAmounts[currentWave]) {
                    currentlySpawning = false;
                }
            } else if (spiderCount <= 1 && Time.time - lastWaveTime > waveSpiderTimes[currentWave]) {
                currentlySpawning = true;
                lastWaveTime = Time.time;
                spawningProgress = 0;
                currentWave++;
                Debug.Log("Starting wave");
            }

            test = spiderCount;
        }

        public static void DecreaseSpiderCount() {
            spiderCount--;
        }
    }
}
