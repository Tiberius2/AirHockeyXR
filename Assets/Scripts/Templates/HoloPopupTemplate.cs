using MixedReality.Toolkit.UX;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using TT.BaseClasses;

namespace TT.Templates
{
    public class HoloPopupTemplate : PopupTemplateBase
    {
        public PressableButton YesButton;
        public PressableButton NoButton;

        public override void SubscribeToYesButton(UnityAction action)
        {
            var newAction = new UnityAction<SelectEnterEventArgs>((_) => { action?.Invoke(); });
            YesButton.selectEntered.AddListener(newAction);
        }

        public override void SubscribeToNoButton(UnityAction action)
        {
            var newAction = new UnityAction<SelectEnterEventArgs>((_) => { action?.Invoke(); });
            NoButton.selectEntered.AddListener(newAction);
        }
    }
}
