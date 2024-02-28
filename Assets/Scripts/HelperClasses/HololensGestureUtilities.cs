using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using UnityEngine.XR;


namespace TT.HelperClasses
{
    public static class HololensGestureUtilities
    {
        public delegate void PinchGestureEventHandler();
        public static event PinchGestureEventHandler OnPinchGesture;
        public static bool wasObjectPlaced = false;

        private const float pinchThreshold = 0.7f;
        public static void CheckPinchState()
        {
            var aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
            bool RightHandIsValid = aggregator.TryGetPinchProgress(XRNode.RightHand, out bool isReadyToPinchRight, out bool isPinchingRight, out float pinchAmountRight);
            bool LeftHandIsValid = aggregator.TryGetPinchProgress(XRNode.LeftHand, out bool isReadyToPinchLeft, out bool isPinchingLeft, out float pinchAmountLeft);

            if (((RightHandIsValid && isReadyToPinchRight && isPinchingRight && pinchAmountRight >= pinchThreshold) ||
                (LeftHandIsValid && isReadyToPinchLeft && isPinchingLeft && pinchAmountLeft >= pinchThreshold)) &&
                !wasObjectPlaced)
            {
                OnPinchGesture?.Invoke();
            }
        }
    }
}
