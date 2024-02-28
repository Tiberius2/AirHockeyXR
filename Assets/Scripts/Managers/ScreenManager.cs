using SingletonTemplate;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using TT.BaseClasses;
using TT.Structs;
using TT.Templates;
using MixedReality.Toolkit.SpatialManipulation;
using UnityEngine.XR;
using TT.HelperClasses;

namespace TT.Managers
{
    public class ScreenManager : MonoSingleton<ScreenManager>
    {
        [SerializeField] private GameObject uiRoot;
        [SerializeField] private BaseScreen currentScreen;
        [SerializeField] private List<BaseScreen> screens;
        [SerializeField] private PopupTemplate confirmationPopup;
        [SerializeField] private HoloPopupTemplate holoConfirmationPopup;
        [SerializeField] private TMP_Text slateTitle;
        [SerializeField] private GameObject slateObject;

        private bool hasActivePopup = false;
        private Orbital slateOrbitalState;
        private Stack<BaseScreen> screenStack = new Stack<BaseScreen>();


        public override void Awake()
        {
            base.Awake();
            if (slateObject != null)
            {
                slateOrbitalState = slateObject.GetComponent<Orbital>();
                slateOrbitalState.enabled = true;
            }
            InitFirstScreen();
        }

        public void OpenScreen(Type screenType, bool shouldClearScreensStack, bool requiredToCloseItself = false)
        {
            var screen = GetBaseScreen(screenType);
            if (shouldClearScreensStack)
            {
                while (screenStack.Count > 0)
                {
                    RemoveScreenFromStack();
                }
            }
            if (slateObject != null)
            {
                slateObject.SetActive(true);
            }
            if (IsXRDevicePresent())
            {
                UIDOtweenManager.Instance.ScreenFadeIn();
            }
            AddScreenToStack(screen, requiredToCloseItself);
        }



        public void CloseScreen()
        {
            if (slateObject != null)
            {
                slateObject.SetActive(false);
            }
            if (IsXRDevicePresent())
            {
                UIDOtweenManager.Instance.ScreenFadeOut();
            }
            RemoveScreenFromStack();
        }

        public bool HasOverlayedScreens() => screenStack.Count > 1 || hasActivePopup;

        public void OpenPopup(Type popupType, PopupStruct currentPopupStruct)
        {
            if (confirmationPopup != null && popupType == confirmationPopup.GetType())
            {
                ShowCurentPopup(confirmationPopup, popupType, currentPopupStruct);
            }
            if (holoConfirmationPopup != null && popupType == holoConfirmationPopup.GetType())
            {
                ShowCurentPopup(holoConfirmationPopup, popupType, currentPopupStruct);
            }
            if (IsXRDevicePresent())
            {
                UIDOtweenManager.Instance.PopupIntro();
            }
            hasActivePopup = true;
        }

        public BaseScreen GetBaseScreen(Type screenType)
        {
            foreach (var screen in screens)
            {
                if (screen.GetType() == screenType)
                {
                    return screen;
                }
            }
            return null;
        }

        public void ChangeSlateTitle(string titleText)
        {
            if (slateTitle.gameObject != null)
            {
                slateTitle.text = titleText;
            }
        }

        public void ClosePopup(Type popupType)
        {
            if (confirmationPopup != null && popupType == confirmationPopup.GetType())
            {
                confirmationPopup.YesButton.onClick.RemoveAllListeners();
                confirmationPopup.NoButton.onClick.RemoveAllListeners();
                confirmationPopup.HidePopup();
            }
            if (holoConfirmationPopup != null && popupType == holoConfirmationPopup.GetType())
            {
                holoConfirmationPopup.YesButton.selectEntered.RemoveAllListeners();
                holoConfirmationPopup.NoButton.selectEntered.RemoveAllListeners();
                holoConfirmationPopup.HidePopup();
            }
            if (IsXRDevicePresent())
            {
                UIDOtweenManager.Instance.PopupOutro();
            }
            hasActivePopup = false;
        }

        private bool IsXRDevicePresent()
        {
            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                if (xrDisplay.running)
                {
                    return true;
                }
            }
            return false;
        }

        private void ShowCurentPopup(PopupTemplateBase popupTemplate, Type popupType, PopupStruct currentPopupStruct)
        {
            popupTemplate.ShowPopup();
            popupTemplate.PopupText.text = currentPopupStruct.PopupText;
            popupTemplate.PositiveButtonText.text = currentPopupStruct.PositiveButtonText;
            popupTemplate.NegativeButtonText.text = currentPopupStruct.NegativeButtonText;
            popupTemplate.SubscribeToYesButton(() => OnButtonClicked(currentPopupStruct.YesButtonCallback, popupType));
            popupTemplate.SubscribeToNoButton(() => OnButtonClicked(currentPopupStruct.NoButtonCallback, popupType));
        }

        private void AddScreenToStack(BaseScreen screen, bool requiredToCloseItself = false)
        {
            screenStack.Push(screen);
            if (IsXRDevicePresent())
            {
                UIDOtweenManager.Instance.ScreenFadeIn();
            }
            screen.EnableScreen(requiredToCloseItself);
        }

        private void RemoveScreenFromStack()
        {
            if (screenStack.Count > 0)
            {
                var screen = screenStack.Pop();
                screen.DisableScreen();
            }
        }

        private void OnButtonClicked(Action buttonCallback, Type popupType)
        {
            ButtonSoundHelper.PlayButtonSound();
            ClosePopup(popupType);
            buttonCallback?.Invoke();
        }


        private void InitFirstScreen()
        {
            if (currentScreen == null)
            {
                Debug.LogError("The first screen is not assigned!");
            }
            else
            {
                AddScreenToStack(currentScreen);
            }

            if (slateObject != null)
            {
                slateObject.gameObject.SetActive(true);
            }
        }
    }
}