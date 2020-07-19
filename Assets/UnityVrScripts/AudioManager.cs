using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

namespace UnityVRScripts {
    public class AudioManager : MonoBehaviour {
        public Sound[] sounds;
        public GameObject mainCamera;

        private static List<GameObject> _audioSources = new List<GameObject>();
        public static Sound[] _sounds;

        public GameObject audioThing;

        void Awake() {
            _sounds = sounds;
            foreach (var sound in sounds) {
                var ad = Instantiate(audioThing);
                var audioSource = (AudioSource) ad.GetComponent<AudioSource>();
                ad.name = $"{sound.name} Audio Source";
                audioSource.playOnAwake = false;
                audioSource.clip = sound.audioClip;
                audioSource.volume = sound.volume;
                audioSource.pitch = sound.pitch;
                ad.GetComponent<Transform>().parent = mainCamera.transform; 
                _audioSources.Add(ad);
            }
        }
        
        void Start() {
            new Thread(() => {
                PlaySound("s");
                Thread.Sleep(5000);
                PlaySound("g");
                Thread.Sleep(5000);
                PlaySound("k");
            }).Start();
        }

        public static void PlaySound(string sound) {
            for (int i = 0; i < _sounds.Length; i++) {
                if (_sounds[i].name == sound) {
                    _audioSources[i].GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
