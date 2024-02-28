using SingletonTemplate;

namespace TT.Managers
{
    public class SettingsManager : MonoSingleton<SettingsManager>
    {
        public bool IsMusicEnabled => isMusicEnabled;
        public bool AreSoundEffectsEnabled => areSoundEffectsEnabled;
        public float MusicVolume => musicVolume;
        public float SoundEffectsVolume => soundEffectsVolume;

        private bool isMusicEnabled = true;
        private bool areSoundEffectsEnabled = true;
        private float musicVolume = 0.5f;
        private float soundEffectsVolume = 0.5f;


        public void SetMusicToggle(bool isEnabled)
        {
            isMusicEnabled = isEnabled;
            if (isMusicEnabled)
            {
                AudioManager.Instance.EnableMusic();
            }
            else
                AudioManager.Instance.DisableMusic();
        }

        public void SetSoundEffectsToggle(bool isEnabled)
        {
            areSoundEffectsEnabled = isEnabled;
            if (areSoundEffectsEnabled)
            {
                AudioManager.Instance.EnableSoundEffects();
            }
            else
            {
                AudioManager.Instance.DisableSoundEffects();
            }
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            AudioManager.Instance.AdjustMusicVolume(musicVolume);
        }

        public void SetSoundEffectsVolume(float volume)
        {
            soundEffectsVolume = volume;
            AudioManager.Instance.AdjustSFXVolume(soundEffectsVolume);
        }
    }
}
