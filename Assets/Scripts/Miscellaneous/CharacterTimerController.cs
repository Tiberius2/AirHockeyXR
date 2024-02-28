using UnityEngine;
using TT.Managers;
using TT.Globals;

namespace TT.Micellaneous
{
    public class CharacterTimerController : MonoBehaviour
    {
        public bool isPlayerInPosition = false;

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(GameConstants.TimerCollider))
            {
                isPlayerInPosition = true;

            }
            else
            {
                isPlayerInPosition = false;
            }
            VRPlaceTableManager.Instance.CheckGameState();
        }
    }
}