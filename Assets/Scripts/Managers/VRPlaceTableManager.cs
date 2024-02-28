using SingletonTemplate;
using UnityEngine;
using TT.HelperClasses;
using TT.Micellaneous;

namespace TT.Managers
{
    public class VRPlaceTableManager : MonoSingleton<VRPlaceTableManager>
    {
        [SerializeField] private GameObject tableParent;
        [SerializeField] private CharacterTimerController characterTimerController;
        [SerializeField] private GameObject tablePrefab;
        [SerializeField] private Collider timerTriggerCollider;
        [SerializeField] private GameObject podiumGameObject;

        private GameObject instantiatedTable;

        public void InstantiateTable()
        {
            var tablePosition = tableParent.transform.position;
            var tableRotation = tableParent.transform.rotation;

            instantiatedTable = Instantiate(tablePrefab, tablePosition, tableRotation);
            GameManager.Instance.CanPlayersMove = false;
            timerTriggerCollider.enabled = true;
            if (instantiatedTable != null)
            {
                podiumGameObject.SetActive(true);
            }
            else
            {
                podiumGameObject.SetActive(false);
            }
        }

        public void CheckGameState()
        {
            var tableController = instantiatedTable.GetComponent<TableComponentsController>();
            if (characterTimerController.isPlayerInPosition)
            {
                tableController.TimerController.StartTimer();
            }
        }

        public bool CheckTablePresence()
        {
            return instantiatedTable;
        }

        public void ResetTimer()
        {
            var tableController = instantiatedTable.GetComponent<TableComponentsController>();
            tableController.TimerController.ResetTimer();
            tableController.TimerController.StartTimer();
        }
        public void DestroyTable()
        {
            if (instantiatedTable != null)
            {
                Destroy(instantiatedTable);
                timerTriggerCollider.enabled = false;
            }
        }
    }
}