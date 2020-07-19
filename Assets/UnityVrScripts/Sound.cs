using UnityEngine;
using UnityEngine.Audio;

namespace UnityVRScripts {
    [System.Serializable]
    public class Sound {
        public AudioClip audioClip;
        public string name;
        public Transform parent;
        
        [Range(0,1)]public float pitch;
        [Range(0,1)]public float volume;
    }
}