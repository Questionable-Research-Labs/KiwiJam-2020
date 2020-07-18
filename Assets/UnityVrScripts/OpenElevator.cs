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
        private float _slideAmount;
        private float _slideDownAmount;
        private static DoorSlide _doorSlide = DoorSlide.STATIONARY;
        
        public static void OpenDoors() {
            _doorSlide = DoorSlide.OPEN;
        }

        public static void CloseDoors() {
            _doorSlide = DoorSlide.CLOSE;
        }

        public static void ElevatorMoveDown() {
            _doorSlide = DoorSlide.DOWN;
        }

        private void Start() {
            _slideAmount = GetComponent<Transform>().localScale.z / 2;
            new Thread(new ThreadStart(() =>
            {
                ElevatorMoveDown();
                Thread.Sleep(2000);
                OpenDoors();
                Thread.Sleep(2000);
                CloseDoors();
            })).Start();
        }

        private void Update() {
            if (_doorSlide == DoorSlide.DOWN)
            {
                GetComponent<Transform>().transform.Translate(new Vector3(0f, -7.5f, 0f) * Time.deltaTime);
                return;
            }

            if (_doorSlide == DoorSlide.STATIONARY) return;
            
            var move = movementSpeed * (Time.deltaTime / 1f);
            _move += move;
            
            if (_doorSlide == DoorSlide.CLOSE) {
                move = -move;
            }

            if (Mathf.Abs(_move) > _slideAmount) {
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
        DOWN = -2,
        OPEN = -1,
        STATIONARY = 0,
        CLOSE = 1,
    }
}
