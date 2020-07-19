using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        void Start()
        {
            AudioManager.PlaySound("s");
            Invoke(nameof(Sound1), 5);
            Invoke(nameof(Sound2), 7);
        }

        void Sound1()
        {
            AudioManager.PlaySound("g");
        }

        void Sound2()
        {
            AudioManager.PlaySound("k");
        }

        // Update is called once per frame
        void Update() {
           /* if (!introduction)
            {
                introduction = true;

            } */
        }

        private void OnGUI() {
            if (detectedHealth < 0) {
                detectedHealth = 0;
            }
            dHBox.text = "Detected Health: " + detectedHealth + "%";
            dCBox.text = "Danger Compensation: " + dangerCompensation;
            goBox.text = "";
            if (detectedHealth <= 0){
                ArdCom.Terminate();
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
