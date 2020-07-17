using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text.RegularExpressions;
namespace UnityVRScripts {

    
    public class ArdCom : MonoBehaviour {
        // Start is called before the first frame update
        public String arduinoPort;
        public bool useArdunio;

        public bool leftRelayStatus = false;
        public bool rightRelayStatus = false;
        
        private SerialPort serialPortStream;

        void Start() {
            if (useArdunio)
            {
                serialPortStream = new SerialPort(arduinoPort, 9600);
                serialPortStream.Open(); 
            }
            else {
                Debug.Log("Ard is not enabled");
            }

            TurnAllRelaysOff();
        }

        public void SendToArduino (String message) {
            
            // // register the event
            // port.DataReceived += Port_DataReceived;
            //open the port
            if (useArdunio) {
                try {
                    // start the communication
                    Debug.Log("Sending message to Ard: " + message);
                    serialPortStream.Write(message + ">");
                }
                catch (Exception ex) {
                    Debug.Log("Writing failed! \nError: " + ex.Message);
                }
            }
            else {
                Debug.Log(message + " sent to arduino sim");
            }
        }

        public void TurnAllRelaysOff() {
            LeftRelayOff();
            RightRelayOff();
        }
        public void TurnAllRelaysOn() {
            LeftRelayOn();
            RightRelayOn();
        }
        public void LeftRelayOn() {
            leftRelayStatus = true;
            SendToArduino("01");
        }

        public void LeftRelayOff() {
            leftRelayStatus = false;
            SendToArduino("00");
        }
        public void RightRelayOn() {
            rightRelayStatus = true;
            SendToArduino("11");
        }

        public void RightRelayOff() {
            rightRelayStatus = false;
            SendToArduino("10");
        }
    }
}
