using MixedReality.Toolkit.Input;
using UnityEngine;
using TT.Enums;
using SingletonTemplate;
using TT.Globals;
using TT.HelperClasses;


namespace TT.Managers
{
    public class EyeTrackerManager : MonoSingleton<EyeTrackerManager>
    {
        [SerializeField] private GazeInteractor gazeInteractor;
        [SerializeField] private GameObject validPlacementVisualizer;
        [SerializeField] private GameObject invalidPlacementVisualizer;
        [SerializeField] private GameObject smallTablePrefab;
        [SerializeField] private GameObject largeTablePrefab;
        [SerializeField] private GameObject infoMessageSlate;

        private GameObject validHitPointDisplayer;
        private GameObject invalidPointDisplayer;
        private GameObject newPlacedObject;
        private GameObject objectToSpawnPrefab;
        private bool canPlaceObject;
        private bool visualizerChanged = false;
        private bool isEyeTrackingActive = false;
        private GameObject hitPointDisplayer;
        private VisualizerState currentVisualizerState;

        public override void Awake()
        {
            base.Awake();
            validHitPointDisplayer = Instantiate(validPlacementVisualizer);
            invalidPointDisplayer = Instantiate(invalidPlacementVisualizer);
            invalidPointDisplayer.SetActive(false);
            currentVisualizerState = VisualizerState.Valid;
        }
        private void OnEnable()
        {
            HololensGestureUtilities.OnPinchGesture += HandlePinchGesture;
        }

        private void OnDisable()
        {
            HololensGestureUtilities.OnPinchGesture -= HandlePinchGesture;
        }

        private void Update()
        {
            if (isEyeTrackingActive)
            {
                var ray = new Ray(gazeInteractor.rayOriginTransform.position, gazeInteractor.rayOriginTransform.forward * GameConstants.ForwardDistance);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.gameObject != null && newPlacedObject == null)
                    {
                        AdjustVisualizerByAngle(hit);
                        var offset = hit.normal * GameConstants.OffsetDistance;
                        if (visualizerChanged)
                        {
                            hitPointDisplayer.transform.position = hit.point + offset;
                            visualizerChanged = false;
                        }
                        else
                        {
                            hitPointDisplayer.transform.position = Vector3.Lerp(hitPointDisplayer.transform.position, hit.point + offset, GameConstants.LerpSpeed * Time.deltaTime);
                        }
                        hitPointDisplayer.transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
                    }
                }
            }
            if (!HololensGestureUtilities.wasObjectPlaced)
            {
                HololensGestureUtilities.CheckPinchState();
            }
        }

        public void SetEyeTrackingState(bool isActive)
        {
            HololensGestureUtilities.wasObjectPlaced = !isActive;
            isEyeTrackingActive = isActive;
            infoMessageSlate.SetActive(isActive);
            validHitPointDisplayer.SetActive(isActive);
            invalidPointDisplayer.SetActive(isActive);
        }

        public void SetTableOption(bool isLarge)
        {
            if (isLarge)
            {
                objectToSpawnPrefab = largeTablePrefab;
            }
            else
            {
                objectToSpawnPrefab = smallTablePrefab;
            }
        }
        public bool CheckTableOption()
        {
            return objectToSpawnPrefab == largeTablePrefab;
        }

        public void DestroyTable()
        {
            if (newPlacedObject != null)
            {
                Destroy(newPlacedObject);
            }
        }

        private void AdjustVisualizerByAngle(RaycastHit hitPoint)
        {
            var angle = Vector3.Angle(hitPoint.normal, Vector3.up);
            if (angle > GameConstants.AngleThreshold)
            {
                canPlaceObject = false;
                UpdateVisualizerState(VisualizerState.Invalid);
            }
            else
            {
                canPlaceObject = true;
                UpdateVisualizerState(VisualizerState.Valid);
            }
        }

        private void HandlePinchGesture()
        {
            if (!HololensGestureUtilities.wasObjectPlaced)
            {
                var direction = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
                if (canPlaceObject)
                {
                    newPlacedObject = Instantiate(objectToSpawnPrefab, validHitPointDisplayer.transform.position, Quaternion.identity);
                    newPlacedObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                    validHitPointDisplayer.SetActive(false);
                    invalidPointDisplayer.SetActive(false);
                    HololensGestureUtilities.wasObjectPlaced = true;
                    infoMessageSlate.SetActive(false);
                    isEyeTrackingActive = false;
                }
                else
                {
                    Debug.Log("INVALID PLACEMENT, CHOOSE ANOTHER SPOT");
                }
            }
        }

        private void UpdateVisualizerState(VisualizerState newState)
        {
            if (newState != currentVisualizerState)
            {
                visualizerChanged = true;
            }
            currentVisualizerState = newState;
            if (newState == VisualizerState.Valid)
            {
                validHitPointDisplayer.SetActive(true);
                invalidPointDisplayer.SetActive(false);
                hitPointDisplayer = validHitPointDisplayer;
            }
            else
            {
                validHitPointDisplayer.SetActive(false);
                invalidPointDisplayer.SetActive(true);
                hitPointDisplayer = invalidPointDisplayer;
            }
        }
    }
}
