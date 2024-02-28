using SingletonTemplate;
using UnityEngine;
using System;
using TT.BaseClasses;
using TT.Enums;

namespace TT.Managers
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private Sound[] soundsList;

        public override void Awake()
        {
            base.Awake();
            foreach (var sound in soundsList)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.AudioClip;
                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.LoopClip;
                sound.Source.playOnAwake = sound.PlayOnAwake;
            }
            EnableMusic();
        }

        public void PlaySound(string name)
        {
            var foundSound = Array.Find(soundsList, sound => sound.ClipName == name);
            foundSound.Source.Play();
        }

        public void StopSound(string name)
        {
            var foundSound = Array.Find(soundsList, sound => sound.ClipName == name);
            foundSound.Source.Stop();
        }

        public void EnableMusic()
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.Music)
                {
                    if (!sound.Source.isPlaying)
                    {
                        sound.Source.Play();
                    }
                    sound.Source.mute = false;
                }
            }
        }
        public void DisableMusic()
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.Music)
                {
                    sound.Source.mute = true;
                }
            }
        }

        public void EnableSoundEffects()
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.SoundEffect && !sound.Source.isPlaying)
                {
                    sound.Source.enabled = true;
                }
            }
        }

        public void DisableSoundEffects()
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.SoundEffect)
                {
                    sound.Source.enabled = false;
                }
            }
        }

        public void AdjustMusicVolume(float volume)
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.Music)
                {
                    sound.Source.volume = volume;
                }
            }
        }

        public void AdjustSFXVolume(float volume)
        {
            foreach (var sound in soundsList)
            {
                if (sound.SoundType == SoundType.SoundEffect)
                {
                    sound.Source.volume = volume;
                }
            }
        }
    }
}
