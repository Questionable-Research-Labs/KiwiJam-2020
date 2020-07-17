using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityVRScripts {
    public class wallHitDetector : MonoBehaviour {
        // Start is called before the first frame update
        public ArdCom ardComManager;

        void OnTriggerEnter (Collider other) {
            var tag = other.gameObject.tag;
            if (tag == "RightController") {
                ardComManager.RightRelayOn();
            }
            else if (tag == "LeftController") {
                ardComManager.LeftRelayOn();
            }
        }
        void OnTriggerExit (Collider other) {
            var tag = other.gameObject.tag;
            if (tag == "RightController") {
                ardComManager.RightRelayOff();
            }
            else if (tag == "LeftController") {
                ardComManager.LeftRelayOff();
            }
        }
    }
}
