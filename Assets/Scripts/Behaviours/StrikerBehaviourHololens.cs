using UnityEngine;
using TT.Managers;

namespace TT.Behaviours
{
    public class StrikerBehaviourHololens : MonoBehaviour
    {
        [SerializeField] private Transform boundaryHolder;

        private Vector3 initialPosition;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            initialPosition = transform.localPosition;
        }

        private void Start()
        {
            rb.position = transform.position;
            GameManager.Instance.AddToDictionaryForReset(gameObject, rb.position);
        }
    }
}
