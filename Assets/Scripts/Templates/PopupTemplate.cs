using UnityEngine.UI;
using UnityEngine.Events;
using TT.BaseClasses;

namespace TT.Templates
{
    public class PopupTemplate : PopupTemplateBase
    {
        public Button YesButton;
        public Button NoButton;

        public override void SubscribeToYesButton(UnityAction action)
        {
            YesButton.onClick.AddListener(action);
        }

        public override void SubscribeToNoButton(UnityAction action)
        {
            NoButton.onClick.AddListener(action);
        }
    }
}