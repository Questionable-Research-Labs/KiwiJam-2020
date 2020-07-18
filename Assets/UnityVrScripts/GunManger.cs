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
            grabInteractable.onActivate.AddListener(TriggerChange);
            grabInteractable.onDeactivate.AddListener(TriggerChange);
        }

        private void OnDestroy() {
            grabInteractable.onActivate.RemoveListener(TriggerChange);
            grabInteractable.onDeactivate.RemoveListener(TriggerChange);
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
        }

        void TriggerChange(XRBaseInteractor interactor) {
            _triggerHeld = !_triggerHeld;
        }
    }
}

