using UnityEngine;
using TT.Structs;
using TT.Enums;
using TT.Managers;

namespace TT.PlayerInteractions
{
    public class DragStriker : MonoBehaviour
    {
        [SerializeField] private Transform boundaryHolder;
        [SerializeField] private Transform tableTransform;
        [SerializeField] private float speed = 1.1f;
        [SerializeField] private Collider planeCollider;

        private Camera mainCamera;
        private Ray ray;
        private Vector3 position;
        private Vector3 finalPosition;
        private Vector3 initialPosition;
        private Boundary playerBoundary;
        private InputMethod currentInputMethod;
        private bool isDraggingStriker;
        private Rigidbody rb;

        private void Start()
        {
            finalPosition = transform.position;
            initialPosition = transform.localPosition;
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody>();

            playerBoundary = new Boundary(boundaryHolder.GetChild(0).localPosition.z,
                                          boundaryHolder.GetChild(1).localPosition.z,
                                          boundaryHolder.GetChild(2).localPosition.x,
                                          boundaryHolder.GetChild(3).localPosition.x);
            DetermineInputMethod();
            GameManager.Instance.AddToDictionaryForReset(gameObject, initialPosition);

        }

        private void Update()
        {
            if (!ScreenManager.Instance.HasOverlayedScreens())
            {
                HandleInputMethod();
            }
            else
            {
                isDraggingStriker = false;
            }
        }

        private void FixedUpdate()
        {
            if (isDraggingStriker && GameManager.Instance.CanPlayersMove)
            {
                position = Vector3.Lerp(transform.position, finalPosition, speed * Time.fixedDeltaTime);
                position = tableTransform.InverseTransformPoint(position);
                position.x = Mathf.Clamp(position.x, playerBoundary.Left, playerBoundary.Right);
                position.z = Mathf.Clamp(position.z, playerBoundary.Down, playerBoundary.Up);
                position = tableTransform.TransformPoint(position);
                rb.MovePosition(position);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherRigidbody = collision.collider.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                var forceDirection = collision.GetContact(0).normal;
                otherRigidbody.AddForce(-forceDirection * rb.velocity.magnitude, ForceMode.Impulse);
            }
        }

        #region Input Handling
        private void DetermineInputMethod()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                currentInputMethod = InputMethod.Mouse;
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                currentInputMethod = InputMethod.Touch;
            }
            else
            {
                currentInputMethod = InputMethod.Mouse;
            }
        }

        private void HandleInputMethod()
        {
            switch (currentInputMethod)
            {
                case InputMethod.Mouse:
                    HandleMouseInput();
                    break;
                case InputMethod.Touch:
                    HandleTouchInput();
                    break;

                default:
                    break;
            }
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                isDraggingStriker = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingStriker = false;
            }
            if (isDraggingStriker)
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                HandleRaycast();
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isDraggingStriker = true;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isDraggingStriker = false;
                }
                if (isDraggingStriker)
                {
                    ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                    HandleRaycast();
                }
            }
        }

        private void HandleRaycast()
        {
            var hits = Physics.RaycastAll(ray);
            if (hits.Length != 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider == planeCollider)
                    {
                        finalPosition = hit.point;
                    }
                }
            }
        }
        #endregion
    }
}