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
            leftRelayStatus = true;
            SendToArduino("01");
        }

        public static void LeftRelayOff() {
            leftRelayStatus = false;
            SendToArduino("00");
        }
        public static void RightRelayOn() {
            rightRelayStatus = true;
            SendToArduino("11");
        }

        public static void RightRelayOff() {
            rightRelayStatus = false;
            SendToArduino("10");
        }
        
        public static void TurnOnRightRelayForDuration(int activeMilliSeconds) {
            new Thread(new ThreadStart(() => {
                RightRelayOn();
                System.Threading.Thread.Sleep(activeMilliSeconds);
                RightRelayOff();
            })).Start();
        }
        
        public static void TurnOnLeftRelayForDuration(int activeMilliSeconds) {
            new Thread(new ThreadStart(() => {
                LeftRelayOn();
                System.Threading.Thread.Sleep(activeMilliSeconds);
                LeftRelayOff();
            })).Start();
        }

        public static void Terminate() {
            serialPortStream.Close();
        }
    }
}
