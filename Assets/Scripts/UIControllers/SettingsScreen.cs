using UnityEngine;
using UnityEngine.UI;
using TT.BaseClasses;
using TT.Managers;
using TT.HelperClasses;

namespace TT.UIControllers
{
    public class SettingsScreen : BaseScreen
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundEffectsToggle;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Camera arCamera;
        [SerializeField] private Sprite musicOnSprite;
        [SerializeField] private Sprite musicOffSprite;
        [SerializeField] private Sprite soundEffectsOnSprite;
        [SerializeField] private Sprite soundEffectsOffSprite;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Button backToMenuButton;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);

            if (arCamera != null && arCamera.gameObject.activeInHierarchy)
            {
                Debug.Log("arCamera is active");
            }
            else
            {
                uiCamera.gameObject.SetActive(true);
            }
            musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
            soundEffectsToggle.onValueChanged.AddListener(OnSoundEffectsToggleValueChanged);
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
            effectsSlider.onValueChanged.AddListener(OnEffectsSliderChanged);
            if (requiredToCloseItself)
            {
                backToMenuButton.onClick.AddListener(CloseScreen);
            }
            else
            {
                backToMenuButton.onClick.AddListener(OnBackButtonClicked);
            }
            backgroundImage.enabled = !requiredToCloseItself;
            musicToggle.isOn = SettingsManager.Instance.IsMusicEnabled;
            soundEffectsToggle.isOn = SettingsManager.Instance.AreSoundEffectsEnabled;
            musicSlider.value = SettingsManager.Instance.MusicVolume;
            effectsSlider.value = SettingsManager.Instance.SoundEffectsVolume;
        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            if (arCamera != null && arCamera.gameObject.activeInHierarchy)
            {
                Debug.Log("arCamera is active");
            }
            else
            {
                uiCamera.gameObject.SetActive(false);
            }
            musicToggle.onValueChanged.RemoveAllListeners();
            soundEffectsToggle.onValueChanged.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            effectsSlider.onValueChanged.RemoveAllListeners();
        }

        private void OnSoundEffectsToggleValueChanged(bool areSoundEffectsEnabled)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetSoundEffectsToggle(areSoundEffectsEnabled);
            ToggleHelper.UpdateSoundEffectsToggleSprite(soundEffectsToggle, soundEffectsOnSprite, soundEffectsOffSprite, SettingsManager.Instance.AreSoundEffectsEnabled);
        }

        private void OnMusicToggleValueChanged(bool isMusicEnabled)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetMusicToggle(isMusicEnabled);
            ToggleHelper.UpdateMusicToggleSprite(musicToggle, musicOnSprite, musicOffSprite, SettingsManager.Instance.IsMusicEnabled);
        }

        private void OnEffectsSliderChanged(float effectsVolume)
        {
            SettingsManager.Instance.SetSoundEffectsVolume(effectsVolume);
        }

        private void OnMusicSliderChanged(float musicVolume)
        {
            SettingsManager.Instance.SetMusicVolume(musicVolume);
        }

        private void OnBackButtonClicked()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(MainMenuScreen), true);
        }

        private void CloseScreen()
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.CloseScreen();
        }
    }
}
