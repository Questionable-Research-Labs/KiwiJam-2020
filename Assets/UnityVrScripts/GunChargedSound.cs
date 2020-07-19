  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVRScripts;

namespace UnityVRScripts {
    public class GunChargedSound : MonoBehaviour
    {
        AudioSource gunChargedAudioSource;

        public bool gunChargedIsPlaying;
        // Start is called before the first frame update
        void Start()
        {
            gunChargedAudioSource = GetComponent<AudioSource>();
            gunChargedIsPlaying = false;
            // Debug.Log();
        }

        // Update is called once per frame
        void Update()
        {
            if (GunManger._currentCharge >= GunManger.gunMaxCharge && GunManger.triggerHeld && !gunChargedIsPlaying) {
                GunManger.gunChargingAudioSource.Stop();
                gunChargedAudioSource.Play();
                gunChargedIsPlaying = true;

            }
            
            if (!GunManger.triggerHeld) {
                gunChargedAudioSource.Stop();
                gunChargedIsPlaying = false;
                Debug.Log("the gun is not charging");
            }
        }
    }
}