using System;

namespace TT.Structs
{
    public struct PopupStruct
    {
        public string PopupText;
        public string PositiveButtonText;
        public string NegativeButtonText;
        public Action YesButtonCallback;
        public Action NoButtonCallback;
        public PopupStruct(string popupText, string positiveButtonText, string negativeButtonText, Action yesButtonCallback, Action noButtonCallback)
        {
            PopupText = popupText;
            PositiveButtonText = positiveButtonText;
            NegativeButtonText = negativeButtonText;
            YesButtonCallback = yesButtonCallback;
            NoButtonCallback = noButtonCallback;
        }
    }
}