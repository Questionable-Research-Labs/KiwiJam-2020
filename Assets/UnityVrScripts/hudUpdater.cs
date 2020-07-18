using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityVRScripts {
    public class hudUpdater : MonoBehaviour {
        public static float detectedHealth = 100;
        public static float dangerCompensation = 0;

        public Text dHBox;
        public Text dCBox;
        

        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            
        }

        private void OnGUI() {
            dHBox.text = "Detected Health: " + detectedHealth + "%";
            dCBox.text = "Danger Compensation: §" + dangerCompensation;
        }

        public static void increassScore(float amountToChange) {
            dangerCompensation+=amountToChange;
        }

        public static void decreaseHealth(float amountToChange) {
            detectedHealth+=amountToChange;
        }
    }
}
