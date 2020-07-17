using UnityEngine;

namespace UnityVRScripts {
    public class ArduinoInitlizer : MonoBehaviour {
        public string arduinoPort;
        public bool useArduino;
        void Awake() {
            if (useArduino) 
                ArdCom.Init(arduinoPort);
        }
    }
}