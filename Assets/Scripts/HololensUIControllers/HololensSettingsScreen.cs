using UnityEngine;
using MixedReality.Toolkit.UX;
using UnityEngine.XR.Interaction.Toolkit;
using TT.BaseClasses;
using TT.Managers;
using TT.Globals;
using TT.HelperClasses;

namespace TT.UIControllers
{
    public class HololensSettingsScreen : BaseScreen
    {
        [SerializeField] private PressableButton musicToggle;
        [SerializeField] private PressableButton sfxToggle;
        [SerializeField] private PressableButton backToMenuButton;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        public override void EnableScreen(bool requiredToCloseItself)
        {
            base.EnableScreen(requiredToCloseItself);
            ScreenManager.Instance.ChangeSlateTitle(GameConstants.SettingsMenuTitle);


            musicToggle.selectExited.AddListener(OnMusicToggleValueChanged);
            sfxToggle.selectExited.AddListener(OnSoundEffectsToggleValueChanged);
            musicSlider.OnValueUpdated.AddListener(OnMusicSliderValueUpdated);
            sfxSlider.OnValueUpdated.AddListener(OnSFXSliderValueUpdated);
            if (requiredToCloseItself)
            {
                backToMenuButton.selectEntered.AddListener(CloseScreen);
            }
            else
            {
                backToMenuButton.selectEntered.AddListener(OnBackButtonClicked);
            }
            musicToggle.ForceSetToggled(SettingsManager.Instance.IsMusicEnabled, false);
            sfxToggle.ForceSetToggled(SettingsManager.Instance.AreSoundEffectsEnabled, false);
            musicSlider.Value = SettingsManager.Instance.MusicVolume;
            sfxSlider.Value = SettingsManager.Instance.SoundEffectsVolume;

        }

        public override void DisableScreen()
        {
            base.DisableScreen();
            musicToggle.selectExited.RemoveAllListeners();
            sfxToggle.selectExited.RemoveAllListeners();
            backToMenuButton.selectEntered.RemoveAllListeners();
            musicSlider.OnValueUpdated.RemoveAllListeners();
            sfxSlider.OnValueUpdated.RemoveAllListeners();
        }

        private void OnMusicToggleValueChanged(SelectExitEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetMusicToggle(musicToggle.IsToggled);

        }

        private void OnSoundEffectsToggleValueChanged(SelectExitEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            SettingsManager.Instance.SetSoundEffectsToggle(sfxToggle.IsToggled);
        }

        private void OnSFXSliderValueUpdated(SliderEventData arg0)
        {
            SettingsManager.Instance.SetSoundEffectsVolume(sfxSlider.Value);
        }

        private void OnMusicSliderValueUpdated(SliderEventData arg0)
        {
            SettingsManager.Instance.SetMusicVolume(musicSlider.Value);
        }


        private void OnBackButtonClicked(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.OpenScreen(typeof(HololensMainMenuScreen), true);
        }
        private void CloseScreen(SelectEnterEventArgs arg0)
        {
            ButtonSoundHelper.PlayButtonSound();
            ScreenManager.Instance.CloseScreen();
        }
    }
}
