using UnityEngine;
namespace TT.Behaviours
{
    public class ObjectPhysics : MonoBehaviour
    {
        private Vector3 velocity;
        private Vector3 lastPosition;
        private void Start()
        {
            lastPosition = transform.position;
        }

        private void Update()
        {
            velocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
        }

        public float GetMovementSpeed()
        {
            return velocity.magnitude;
        }
    }
}