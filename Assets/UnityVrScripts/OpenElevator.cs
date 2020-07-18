using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityVRScripts {
    public class OpenElevator : MonoBehaviour {
        public int movementSpeed;
        
        public Transform door1;
        public Transform door2;

        private float _move = 0;
        private float _slideAmmount;
        private static DoorSlide _doorSlide = DoorSlide.STATIONARY;
        
        public static void OpenDoors() {
            _doorSlide = DoorSlide.OPEN;
        }

        public static void CloseDoors() {
            _doorSlide = DoorSlide.CLOSE;
        }

        private void Start() {
            _slideAmmount = GetComponent<Transform>().localScale.z / 2;
            new Thread(new ThreadStart(() => {
                Thread.Sleep(2000);
                OpenDoors();
                Thread.Sleep(2000);
                CloseDoors();
            })).Start();
        }

        private void Update() {
            if (_doorSlide == DoorSlide.STATIONARY) return;
            
            var move = movementSpeed * (Time.deltaTime / 1f);
            _move += move;
            
            if (_doorSlide == DoorSlide.CLOSE) {
                move = -move;
            }

            if (Mathf.Abs(_move) > _slideAmmount) {
                _doorSlide = DoorSlide.STATIONARY;
                _move = 0;
                return;
            }

            var vec1 = door1.position;
            var vec2 = door2.position;

            vec1.z -= move;
            vec2.z += move;

            door1.position = vec1;
            door2.position = vec2;
        } 
    }

    public enum DoorSlide {
        OPEN = -1,
        STATIONARY = 0,
        CLOSE = 1,
    }
}
