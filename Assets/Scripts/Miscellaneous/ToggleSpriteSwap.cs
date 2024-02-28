using UnityEngine;
using UnityEngine.UI;

namespace TT.Micellaneous
{
    public class ToggleSpriteSwap : MonoBehaviour
    {
        [SerializeField] private Toggle targetToggle;
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite isOnSprite;
        [SerializeField] private Sprite isOffSprite;

        private void Start()
        {
            targetToggle.toggleTransition = Toggle.ToggleTransition.None;
            targetToggle.onValueChanged.AddListener(OnToggled);
        }
        private void OnToggled(bool isOn)
        {
            if (targetImage == null)
                return;
            var sprite = isOn ? isOnSprite : isOffSprite;
            targetImage.overrideSprite = sprite;
        }
    }
}