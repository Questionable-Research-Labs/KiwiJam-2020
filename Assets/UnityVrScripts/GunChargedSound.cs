using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVRScripts;

namespace UnityVRScripts {
    public class GunChargedSound : MonoBehaviour
    {
        AudioSource gunChargedAudioSource;
        // Start is called before the first frame update
        void Start()
        {
            gunChargedAudioSource = GetComponent<AudioSource>();
            Debug.Log();
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentCharge >= gunMaxCharge)
            {
                gunChargingAudioSource.Stop();
                gunChargedAudioSource.Play();
            }
        }
    }
}