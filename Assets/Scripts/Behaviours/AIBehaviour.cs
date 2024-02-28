using UnityEngine;
using TT.Structs;
using TT.Managers;
using TT.HelperClasses;
using TT.Globals;

namespace TT.Behaviours
{
    public class AIBehaviour : MonoBehaviour
    {
        public float MaxMovementSpeed;
        public float MinOffsetRange;
        public float MaxOffsetRange;
        public float MinMovementSpeedMultipier;
        public float MaxMovementSpeedMultipier;

        [SerializeField] private Transform tableTransform;
        [SerializeField] private Transform aIBoundaryHolder;
        [SerializeField] private Transform puckBoundaryHolder;
        [SerializeField] private Rigidbody puck;

        private Vector3 aiStartingLocalPosition;
        private Vector3 targetLocalPosition;
        private Boundary aiBoundary;
        private Boundary puckBoundary;
        private Rigidbody rb;
        private Transform puckTransform;
        private bool isFirstTimeInOpponentHalf = true;
        private float aiMovementSpeed;
        private float offsetXFromTarget;
        private float clampedX;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }

        private void Start()
        {
            puckTransform = puck.transform;
            rb = GetComponent<Rigidbody>();
            rb.position = transform.position;
            aiStartingLocalPosition = transform.localPosition;
            aiBoundary = new Boundary(aIBoundaryHolder.GetChild(0).localPosition.z,
                                          aIBoundaryHolder.GetChild(1).localPosition.z,
                                          aIBoundaryHolder.GetChild(2).localPosition.x,
                                          aIBoundaryHolder.GetChild(3).localPosition.x);
            puckBoundary = new Boundary(puckBoundaryHolder.GetChild(0).localPosition.z,
                                        puckBoundaryHolder.GetChild(1).localPosition.z,
                                        puckBoundaryHolder.GetChild(2).localPosition.x,
                                        puckBoundaryHolder.GetChild(3).localPosition.x);

            GameManager.Instance.AddToDictionaryForReset(gameObject, rb.position);
            DifficultySetterHololens.SetDifficultyValues(this, GameValues.DifficultySetting);
        }

        private void FixedUpdate()
        {
            if (PuckBehaviour.WasGoal || !GameManager.Instance.CanPlayersMove)
            {
                return;
            }
            if (puckTransform.localPosition.z < puckBoundary.Up)
            {
                if (isFirstTimeInOpponentHalf)
                {
                    isFirstTimeInOpponentHalf = false;
                    offsetXFromTarget = Random.Range(MinOffsetRange, MaxOffsetRange);
                }
                clampedX = Mathf.Clamp(puckTransform.localPosition.x + offsetXFromTarget, aiBoundary.Right, aiBoundary.Left);
                aiMovementSpeed = MaxMovementSpeed * Random.Range(MinMovementSpeedMultipier, MaxMovementSpeedMultipier) + GameConstants.EasySpeedMultiplierMin;
                targetLocalPosition = new Vector3(clampedX, aiStartingLocalPosition.y, aiStartingLocalPosition.z);
            }
            else
            {
                isFirstTimeInOpponentHalf = true;
                aiMovementSpeed = GameConstants.EasySpeedMultiplierMax + Random.Range(MaxMovementSpeed * MaxMovementSpeedMultipier, MaxMovementSpeed);
                targetLocalPosition = new Vector3(Mathf.Clamp(puckTransform.localPosition.x, aiBoundary.Right, aiBoundary.Left),
                    aiStartingLocalPosition.y,
                    Mathf.Clamp(puckTransform.localPosition.z, aiBoundary.Up, aiBoundary.Down));
                if (puckTransform.localPosition.z > puckBoundary.Up)
                {
                    targetLocalPosition = puckTransform.localPosition;
                }
            }
            rb.MovePosition(Vector3.MoveTowards(rb.position, tableTransform.TransformPoint(targetLocalPosition), aiMovementSpeed * Time.fixedDeltaTime));
        }
    }
}