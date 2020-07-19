using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.Audio;

namespace UnityVRScripts {
    public class AudioManager : MonoBehaviour {
        public Sound[] sounds;
        public GameObject mainCamera;

        private static List<AudioSource> _audioSources = new List<AudioSource>();
        private static Sound[] _sounds;
        
        void Start() {
            _sounds = sounds;
            foreach (var sound in sounds) {
                var audioSource = new AudioSource();
                audioSource.GetComponent<GameObject>().name = $"{sound.name} Audio Source";
                audioSource.clip = sound.audioClip;
                audioSource.volume = sound.volume;
                audioSource.pitch = sound.pitch;
                audioSource.transform.parent = mainCamera.transform;
                _audioSources.Add(audioSource);
            }

            StartCoroutine("MakeTheNoises");
        }

        IEnumerable MakeTheNoises() {
            PlaySound(sounds[0].name);
            yield return new WaitForSeconds(5);
            PlaySound(sounds[1].name);
            yield return new WaitForSeconds(5);
            PlaySound(sounds[2].name);
            yield return new WaitForSeconds(5);
            PlaySound(sounds[3].name);
            yield return new WaitForSeconds(5);
            PlaySound(sounds[4].name);
            yield return new WaitForSeconds(5);
        }
        
        public static void PlaySound(string sound) {
            for (int i = 0; i < _sounds.Length; i++) {
                if (_sounds[i].name == sound) {
                    _audioSources[i].Play();
                }
            }
        }
    }
}
