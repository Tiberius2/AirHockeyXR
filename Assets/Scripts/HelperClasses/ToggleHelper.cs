using UnityEngine;
using UnityEngine.UI;

namespace TT.HelperClasses
{
    public static class ToggleHelper
    {
        public static void UpdateMusicToggleSprite(Toggle musicToggle, Sprite musicOnSprite, Sprite musicOffSprite, bool isMusicEnabled)
        {
            if (musicToggle != null)
            {
                musicToggle.image.sprite = isMusicEnabled ? musicOnSprite : musicOffSprite;
            }
        }

        public static void UpdateSoundEffectsToggleSprite(Toggle soundEffectsToggle, Sprite soundEffectsOnSprite, Sprite soundEffectsOffSprite, bool areSoundEffectsEnabled)
        {
            if (soundEffectsToggle != null)
            {
                soundEffectsToggle.image.sprite = areSoundEffectsEnabled ? soundEffectsOnSprite : soundEffectsOffSprite;
            }
        }
    }
}