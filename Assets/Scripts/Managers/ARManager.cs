using SingletonTemplate;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace TT.Managers
{
    public class ARManager : MonoSingleton<ARManager>
    {
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private ARPlaneManager arPlaneManager;
        [SerializeField] private ARSession arSession;
        [SerializeField] private GameObject arCamera;
        [SerializeField] private ARCameraManager arCameraManager;
        public override void Awake()
        {
            base.Awake();
            DisableARSession();
            arCameraManager.requestedFacingDirection = CameraFacingDirection.World;
        }

        public void EnableARSession()
        {
            SetArSessionState(true);
        }

        public void DisableARSession()
        {
            arSession.Reset();
            SetArSessionState(false);
            PlaceManager.Instance.DestroyTable();
        }

        private void SetArSessionState(bool isActive)
        {
            arSession.enabled = isActive;
            arCamera.SetActive(isActive);
            arRaycastManager.enabled = isActive;
            arPlaneManager.enabled = isActive;
            arCameraManager.enabled = isActive;
        }
    }
}