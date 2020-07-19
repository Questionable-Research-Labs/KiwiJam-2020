using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    [RequireComponent(typeof(AudioSource))]
    public class GunManger : MonoBehaviour {
        public GameObject bulletPrefab;
        public static AudioSource gunChargingAudioSource;
        public int startingPitch = 1;
        public int startingTimeSamples = 1;
        public float gunChargeRate;
        
        public static float gunMaxCharge;
        public static float _currentCharge;
        
        

        //Play the music
        bool m_Play;

        //Detect when you use the toggle, ensures music isn’t played multiple times
        bool m_ToggleChange;

        public static bool triggerHeld = false;

        void Start() {
            //Fetch the AudioSource from the GameObject
            gunChargingAudioSource = GetComponent<AudioSource>();
            gunChargingAudioSource.pitch = startingPitch;
            gunChargingAudioSource.volume = 0.2f;
            
            //Ensure the toggle is set to true for the music to play at start-up
            m_Play = true;
        }
        
        private void Update() {
            if (Input.GetAxis("XRI_Right_Trigger") < 0.5 && !triggerHeld) {

                TriggerPress();
            } else if (Input.GetAxis("XRI_Right_Trigger") > 0.5 && triggerHeld) {
                TriggerRelease();
                
            }
            
            if (triggerHeld) {
                _currentCharge += gunChargeRate * Time.deltaTime;
            }
            else if (_currentCharge>0) {
                _currentCharge -= gunChargeRate * Time.deltaTime;
            }
        }

        void FireBullet() {
            Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 180, 0));
            Debug.Log("Fired Bullet");
            ArdCom.TurnOnRightRelayForDuration(200);

            // rightController.SendHapticImpulse(1f, 500);
        }

        void TriggerPress() {
            triggerHeld = true;
            gunChargingAudioSource.pitch = startingPitch;
            gunChargingAudioSource.timeSamples = startingTimeSamples;
            gunChargingAudioSource.Play();
            //XRController.SendHapticImpulse(0, 0.5f);

        }
        void TriggerRelease() {
            triggerHeld = false;
            _currentCharge = 0.0f;
            Debug.Log("Trigger Unpress");
            Debug.Log(_currentCharge);
            if (_currentCharge >= gunMaxCharge) {
                FireBullet();
            }
            else {
                gunChargingAudioSource.timeSamples = gunChargingAudioSource.clip.samples - 1;
                gunChargingAudioSource.pitch = -1;
                gunChargingAudioSource.Play();
            }
            
        }
    }
}

