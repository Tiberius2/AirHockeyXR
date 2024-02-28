using UnityEngine;
using TT.Enums;


namespace TT.BaseClasses
{

    [System.Serializable]
    public class Sound
    {
        [HideInInspector]
        public AudioSource Source;
        public string ClipName;
        public AudioClip AudioClip;
        public bool LoopClip;
        public SoundType SoundType;
        public bool PlayOnAwake = false;

        [Range(0f,1f)]
        public float Volume;
        [Range(.1f,3f)]
        public float Pitch;
        
    }
}
