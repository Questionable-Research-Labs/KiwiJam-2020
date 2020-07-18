using UnityEngine;

namespace UnityVRScripts {
    public class MeleWeapon : MonoBehaviour{
        private void OnCollisionEnter(Collision other) {
            Destroy(gameObject);
            if (other.gameObject.CompareTag("Spider")) {
                var spooder = other.gameObject.GetComponent<Rigidbody>();
                var direction = 3 * (spooder.GetComponent<Transform>().transform.forward * -1);
                spooder.AddForce(direction);
            }
        }
    }
}