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
        public static bool spawningEnabled = false;

        public LightsOff lightsToTurnOnAtEnd;

        public void SpawnSpider() {
            Debug.Log("Spawning spider");
            var a = Instantiate(spiderPrefab, spawnLocations[UnityEngine.Random.Range(0,spawnLocations.Length)],Quaternion.identity );
            a.GetComponent<SpiderController>().SetSpawner(this);
        }

        private void Update() {
            if (spawningEnabled) {
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
                    if (currentWave >= waveSpiderAmounts.Length) {
                        spawningEnabled = false;
                        lightsToTurnOnAtEnd.LightsOn = false;
                    }
                }
            }
            

            test = spiderCount;
        }

        public static void DecreaseSpiderCount() {
            spiderCount--;
        }
    }
}
