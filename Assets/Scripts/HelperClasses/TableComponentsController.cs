using TT.Miscellaneous;
using UnityEngine;

namespace TT.HelperClasses
{
    public class TableComponentsController : MonoBehaviour
    {
        [SerializeField] private TimerController timerController;

        public TimerController TimerController
        {
            get { return timerController; }
            private set { timerController = value; }
        }
    }
}
