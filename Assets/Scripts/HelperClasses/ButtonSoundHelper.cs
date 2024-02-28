using TT.Globals;
using TT.Managers;

namespace TT.HelperClasses
{
    public static class ButtonSoundHelper
    {
        public static void PlayButtonSound()
        {
            AudioManager.Instance.PlaySound(GameConstants.ButtonClickSound);
        }
    }
}
