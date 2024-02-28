using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TT.BaseClasses
{
    public abstract class PopupTemplateBase : MonoBehaviour
    {
        public TMP_Text PopupText;
        public TMP_Text PositiveButtonText;
        public TMP_Text NegativeButtonText;

        public virtual void ShowPopup()
        {
            gameObject.SetActive(true);
        }

        public virtual void HidePopup()
        {
            gameObject.SetActive(false);
        }

        public abstract void SubscribeToYesButton(UnityAction action);

        public abstract void SubscribeToNoButton(UnityAction action);
    }
}