using TT.Globals;
using UnityEngine;

namespace TT.HelperClasses
{
    public class PuckSpeedLimiter : MonoBehaviour
    {
        private Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, GameConstants.PuckSpeedAndroidiOS);
        }
    }
}