using SingletonTemplate;
using System.Collections.Generic;
using TT.Globals;
using UnityEngine;

namespace TT.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool IsGameOver = false;
        public bool CanPlayersMove = false;

        private Dictionary<GameObject, Vector3> objectsToReset = new Dictionary<GameObject, Vector3>();

        public void AddToDictionaryForReset(GameObject objectToReset, Vector3 objectInitialLocalPosition)
        {
            if (!objectsToReset.ContainsKey(objectToReset))
            {
                objectsToReset.Add(objectToReset, objectInitialLocalPosition);
            }
            else
            {
                objectsToReset[objectToReset] = objectInitialLocalPosition;
            }
        }

        public void ResetObjectPositions()
        {
            foreach (var pair in objectsToReset)
            {
                GameObject objectReference = pair.Key;
                Vector3 initialPosition = pair.Value;
                if (objectReference != null)
                {
                    Rigidbody rb = objectReference.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        if (Application.platform != RuntimePlatform.WindowsPlayer && objectReference.name == GameConstants.PlayerStrikerTag)
                        {
                            objectReference.transform.localPosition = initialPosition;
                        }
                        else
                        {
                            rb.velocity = Vector3.zero;
                            rb.position = initialPosition;
                        }
                    }
                    objectReference.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
