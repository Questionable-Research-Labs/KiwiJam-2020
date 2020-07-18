using System;
using UnityEngine;

namespace UnityVRScripts {
    public class ArduinoInitlizer : MonoBehaviour {
        public string arduinoPort;
        public bool useArduino;
        void Awake() {
            if (useArduino && arduinoPort != null) 
                ArdCom.Init(arduinoPort);
        }

        private void OnDestroy() {
            ArdCom.Terminate();
        }

        private void Update() {
            if (Input.GetKeyDown("space")) {
                ArdCom.TurnAllRelaysOn();
            } else {
                ArdCom.TurnAllRelaysOff();
            }
        }
    }
}