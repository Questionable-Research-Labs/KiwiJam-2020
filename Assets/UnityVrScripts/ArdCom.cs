using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;

namespace UnityVRScripts {
    
    public static class ArdCom {
        // Start is called before the first frame update
        public static String arduinoPort;
        public static bool useArdunio;

        public static bool leftRelayStatus = false;
        public static bool rightRelayStatus = false;
        
        private static SerialPort serialPortStream;

        public static void Init(String arduinoPort) {
            ArdCom.arduinoPort = arduinoPort;
            
            serialPortStream = new SerialPort(arduinoPort, 9600);
            serialPortStream.Open();

            useArdunio = true;

            TurnAllRelaysOff();
        }

        public static void SendToArduino (String message) {
            
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

        public static void TurnAllRelaysOff() {
            LeftRelayOff();
            RightRelayOff();
        }
        public static void TurnAllRelaysOn() {
            LeftRelayOn();
            RightRelayOn();
        }
        public static void LeftRelayOn() {
            if (leftRelayStatus) return;
            leftRelayStatus = true;
            SendToArduino("01");
        }

        public static void LeftRelayOff() {
            if (!leftRelayStatus) return;
            leftRelayStatus = false;
            SendToArduino("00");
        }
        public static void RightRelayOn() {
            if (rightRelayStatus) return;
            rightRelayStatus = true;
            SendToArduino("11");
        }

        public static void RightRelayOff() {
            if (!rightRelayStatus) return;
            rightRelayStatus = false;
            SendToArduino("10");
        }
        
        public static void TurnOnRightRelayForDuration(int activeMilliSeconds) {
            if (rightRelayStatus) return;
            new Thread(() => {
                RightRelayOn();
                Thread.Sleep(activeMilliSeconds);
                RightRelayOff();
            }).Start();
        }
        
        public static void TurnOnLeftRelayForDuration(int activeMilliSeconds) {
            if (leftRelayStatus) return;
            new Thread(() => {
                LeftRelayOn();
                Thread.Sleep(activeMilliSeconds);
                LeftRelayOff();
            }).Start();
        }

        public static void TurnOnControllerForDuration(int activeMilliSeconds, GameObject controllerTag  ) {
            if (controllerTag.CompareTag("LeftController")) {
                TurnOnLeftRelayForDuration(activeMilliSeconds);
            } else if (controllerTag.CompareTag("RightController")) {
                TurnOnRightRelayForDuration(activeMilliSeconds);
            }
        }

        public static void Terminate() {
            if (useArdunio)
            {
                SendToArduino("00");
                SendToArduino("10");
                serialPortStream.Close();
                useArdunio = false;
            }
        }
    }
}
