using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    public class GunManger : MonoBehaviour {
        public GameObject bulletPrefab;
        private XRGrabInteractable grabInteractable = null;
        public static AudioSource gunChargingAudioSource;
        public int startingPitch = 1;
        public int startingTimeSamples = 1;
        public float gunChargeRate;
        public XRController rightController;
        
        public static float gunMaxCharge;
        public static float _currentCharge;
        
        

        //Play the music
        bool m_Play;

        //Detect when you use the toggle, ensures music isn’t played multiple times
        bool m_ToggleChange;

        public static bool triggerHeld = false;

        void Start() {
            grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.onActivate.AddListener(TriggerPress);
            grabInteractable.onDeactivate.AddListener(TriggerUnpress);
            grabInteractable.onSelectExit.AddListener(TriggerUnpress);
            //Fetch the AudioSource from the GameObject
            gunChargingAudioSource = GetComponent<AudioSource>();
            gunChargingAudioSource.pitch = startingPitch;
            gunChargingAudioSource.volume = 0.2f;
            
            //Ensure the toggle is set to true for the music to play at start-up
            m_Play = true;
        }

        private void OnDestroy() {
            grabInteractable.onActivate.RemoveListener(TriggerPress);
            grabInteractable.onDeactivate.RemoveListener(TriggerUnpress);
        }

        private void Update() {
            if (triggerHeld) {
                _currentCharge += gunChargeRate * Time.deltaTime;

            }
            else if (_currentCharge>0) {
                _currentCharge -= gunChargeRate * Time.deltaTime;
            }

        }

        void FireBullet() {
            Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            Debug.Log("Fired Bullet");
            ArdCom.TurnOnRightRelayForDuration(200);

            // rightController.SendHapticImpulse(1f, 500);
        }

        void TriggerPress(XRBaseInteractor interactor) {
            triggerHeld = true;
            gunChargingAudioSource.pitch = startingPitch;
            gunChargingAudioSource.timeSamples = startingTimeSamples;
            gunChargingAudioSource.Play();

        }
        void TriggerUnpress(XRBaseInteractor interactor) {
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

