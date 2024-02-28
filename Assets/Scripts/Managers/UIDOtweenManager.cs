using UnityEngine;
using DG.Tweening;
using SingletonTemplate;
using Unity.VisualScripting;

namespace TT.Managers
{
    public class UIDOtweenManager : MonoSingleton<UIDOtweenManager>
    {
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform confirmationPopupTransfrom;

        private Vector3 popupRectTransfromPos;
        private Vector3 screenRectTransformPos;

        public override void Awake()
        {
            base.Awake();
            if (rectTransform != null)
            {
                screenRectTransformPos = rectTransform.transform.localPosition;
            }
            if (confirmationPopupTransfrom != null)
            {
                popupRectTransfromPos = confirmationPopupTransfrom.transform.localPosition;
            }
        }

        public void ScreenFadeIn()
        {
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(screenRectTransformPos.x, -10f, screenRectTransformPos.z);
            rectTransform.DOAnchorPos(screenRectTransformPos, fadeTime, false).SetEase(Ease.InOutElastic);
            canvasGroup.DOFade(1, fadeTime);
        }

        public void ScreenFadeOut()
        {
            canvasGroup.alpha = 1f;
            rectTransform.transform.localPosition = screenRectTransformPos;
            rectTransform.DOAnchorPos(new Vector3(screenRectTransformPos.x, -10f, screenRectTransformPos.z), fadeTime, false);
            canvasGroup.DOFade(0, fadeTime);
        }

        public void PopupIntro()
        {
            confirmationPopupTransfrom.transform.localPosition = new Vector3(popupRectTransfromPos.x, 0f, popupRectTransfromPos.z);
            confirmationPopupTransfrom.DOAnchorPos(popupRectTransfromPos, fadeTime, false).SetEase(Ease.InOutElastic);
        }
        public void PopupOutro()
        {
            confirmationPopupTransfrom.transform.localPosition = popupRectTransfromPos;
            confirmationPopupTransfrom.DOAnchorPos(new Vector3(popupRectTransfromPos.x, 0f, popupRectTransfromPos.z), fadeTime, false).SetEase(Ease.InOutElastic);
        }
    }
}