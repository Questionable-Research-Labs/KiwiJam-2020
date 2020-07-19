using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityVRScripts {
    public class hudUpdater : MonoBehaviour {
        public static float detectedHealth = 30;
        public static float dangerCompensation = 0;

        public Text dHBox; //detectedHealth
        public Text dCBox; //dangerCompensation
        public Text goBox; //gameoverbox;

        private bool healthCritcalWarning = false;

        void Start() {
            AudioManager.PlaySound("g");

        }

        // Update is called once per frame
        void Update() {
            
        }

        private void OnGUI() {
            dHBox.text = "Detected Health: " + detectedHealth + "%";
            dCBox.text = "Danger Compensation: " + dangerCompensation;
            goBox.text = "";
            if (detectedHealth <= 0){
                goBox.text = "Game Over!";
            }
            if (detectedHealth <= 5 && !healthCritcalWarning)
            {
                AudioManager.PlaySound("h");
            }
        }

        public static void increaseScore(float amountToChange) {
            dangerCompensation+=amountToChange;
        }

        public static void decreaseHealth(float amountToChange) {
            detectedHealth+=amountToChange;
        }
    }
}
