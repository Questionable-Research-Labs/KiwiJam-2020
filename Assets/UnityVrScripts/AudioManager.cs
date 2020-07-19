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
                if (sound.parent != null)
                {
                    ad.GetComponent<Transform>().parent = sound.parent.transform;
                }
                else
                {
                    ad.GetComponent<Transform>().parent = mainCamera.transform;
                }
                _audioSources.Add(ad);
            }
        }
        
        void Start() {
            new Thread(() => {
                PlaySound(sounds[0].name);
                Thread.Sleep(5000);
                PlaySound(sounds[1].name);
                Thread.Sleep(5000);
                PlaySound(sounds[4].name);
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
