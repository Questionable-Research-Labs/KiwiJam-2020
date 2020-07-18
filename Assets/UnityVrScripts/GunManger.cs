using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityVRScripts {
    public class GunManger : MonoBehaviour {
        public GameObject bulletPrefab;
        private XRGrabInteractable grabInteractable = null;

        public float gunChargeRate;
        public float gunMaxCharge;

        public float _currentCharge;

        private bool _triggerHeld = false;
        void Start() {
            grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.onActivate.AddListener(TriggerPress);
            grabInteractable.onDeactivate.AddListener(TriggerUnpress);
            grabInteractable.onSelectExit.AddListener(TriggerUnpress);
        }

        private void OnDestroy() {
            grabInteractable.onActivate.RemoveListener(TriggerPress);
            grabInteractable.onDeactivate.RemoveListener(TriggerUnpress);
        }

        private void Update() {
            if (_triggerHeld) {
                _currentCharge += gunChargeRate * Time.deltaTime;
                if (_currentCharge >= gunMaxCharge) {
                    FireBullet();
                    _currentCharge = 0.0f;
                }
            }
            else if (_currentCharge>0) {
                _currentCharge -= gunChargeRate * Time.deltaTime;
            }

        }

        void FireBullet() {
            Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            Debug.Log("Fired Bullet");
            ArdCom.TurnOnRightRelayForDuration(200);
        }

        void TriggerPress(XRBaseInteractor interactor) {
            _triggerHeld = true;
        }
        void TriggerUnpress(XRBaseInteractor interactor) {
            _triggerHeld = false;
            if (_currentCharge >= gunMaxCharge) {
                FireBullet();
                _currentCharge = 0.0f;
            }
        }
    }
}

