using TT.BaseClasses;
using TT.HelperClasses;
using TT.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TT.VRUIContrrolers
{
    public class VRSettingsScreen : BaseScreen
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Toggle sfxToggle;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Sprite soundEffectsOnSprite;
        [SerializeField] private Sprite soundEffectsOffSprite;
        [SerializeField] private Sprite musicOnSprite;
        [SerializeField] private Sprite musicOffSprite;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
            sfxToggle.onValueChanged.AddListener(OnSFXToggleValueChanged);
            musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
            if (requiredToCloseItself)
            {
                backButton.onClick.AddListener(CloseScreen);
            }
            else
            {
                backButton.onClick.AddListener(OnBackButtonPressed);
            }
            musicToggle.isOn = SettingsManager.Instance.IsMusicEnabled;
            sfxToggle.isOn = SettingsManager.Instance.AreSoundEffectsEnabled;
            musicSlider.value = SettingsManager.Instance.MusicVolume;
            sfxSlider.value = SettingsManager.Instance.SoundEffectsVolume;
        }

        public override void DisableScreen()
        {
            base .DisableScreen();
            backButton.onClick.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            sfxToggle.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
        }
        private void OnMusicToggleValueChanged(bool isMusicEnabled)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetMusicToggle(isMusicEnabled);
            ToggleHelper.UpdateMusicToggleSprite(musicToggle, musicOnSprite, musicOffSprite, SettingsManager.Instance.IsMusicEnabled);

        }

        private void OnSFXToggleValueChanged(bool areSoundEffectsEnabled)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetSoundEffectsToggle(areSoundEffectsEnabled);
            ToggleHelper.UpdateSoundEffectsToggleSprite(sfxToggle, soundEffectsOnSprite, soundEffectsOffSprite, SettingsManager.Instance.AreSoundEffectsEnabled);
        }

        private void OnMusicSliderValueChanged(float musicVolume)
        {
            SettingsManager.Instance.SetMusicVolume(musicVolume);
        }

        private void OnSFXSliderValueChanged(float soundEffectsVolume)
        {
            SettingsManager.Instance.SetSoundEffectsVolume(soundEffectsVolume);
        }

        private void OnBackButtonPressed()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(VRMainMenuScreen), true);
        }

        private void CloseScreen()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.CloseScreen();
        }
    }
}
