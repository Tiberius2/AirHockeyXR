using UnityEngine;
using SingletonTemplate;
using TT.Globals;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace TT.Managers
{
    public class PlaceManager : MonoSingleton<PlaceManager>
    {
        [SerializeField] private ARRaycastManager raycastManager;
        [SerializeField] private GameObject objectToPlace;
        [SerializeField] private GameObject placeIndicator;

        private GameObject newPlacedObject;
        private List<ARRaycastHit> hitList = new List<ARRaycastHit>();
        private Pose hitPose;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (raycastManager.Raycast(ray, hitList, TrackableType.Planes) && newPlacedObject == null)
            {
                hitPose = hitList[0].pose;
                placeIndicator.transform.position = Vector3.Lerp(placeIndicator.transform.position, hitPose.position, GameConstants.LerpSpeed * Time.deltaTime);
                placeIndicator.transform.rotation = hitPose.rotation;
                placeIndicator.SetActive(true);

            }
            else
            {
                placeIndicator.SetActive(false);
            }
        }

        public void PlaceObject()
        {
            var direction = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            newPlacedObject = Instantiate(objectToPlace, placeIndicator.transform.localPosition, Quaternion.identity);
            newPlacedObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            newPlacedObject.layer = LayerMask.NameToLayer(GameConstants.PlacedObjectLayer);
            placeIndicator.SetActive(false);
        }

        public void DestroyTable()
        {
            if (newPlacedObject != null)
                Destroy(newPlacedObject);
        }
    }
}