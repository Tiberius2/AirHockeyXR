using UnityEngine;
using TT.Interfaces;

namespace TT.BaseClasses
{
    public class BaseScreen : MonoBehaviour, IScreen
    {
        public virtual void DisableScreen()
        {
            gameObject.SetActive(false);
        }

        public virtual void EnableScreen(bool requiredToCloseItself)
        {
            gameObject.SetActive(true);
        }
    }
}