using System;
using UnityEngine;

namespace BigfootSdk.SafeArea
{
    /// <summary>
    /// Safe area implementation for notched mobile devices. Usage:
    ///  (1) Add this component to the top level of any GUI panel. 
    ///  (2) If the panel uses a full screen background image, then create an immediate child and put the component on that instead, with all other elements childed below it.
    ///      This will allow the background image to stretch to the full extents of the screen behind the notch, which looks nicer.
    ///  (3) For other cases that use a mixture of full horizontal and vertical background stripes, use the Conform X & Y controls on separate elements as needed.
    /// </summary>
    public class SafeArea : MonoBehaviour
    {
        #region Simulations
        /// <summary>
        /// Simulation device that uses safe area due to a physical notch or software home bar. For use in Editor only.
        /// </summary>
        public enum SimDevice
        {
            /// <summary>
            /// Don't use a simulated safe area - GUI will be full screen as normal.
            /// </summary>
            None,
            /// <summary>
            /// Simulate the iPhone X and Xs (identical safe areas).
            /// </summary>
            iPhoneX,
            /// <summary>
            /// Simulate the iPhone Xs Max and XR (identical safe areas).
            /// </summary>
            iPhoneXsMax
        }


        /// <summary>
        /// Simulation mode for use in editor only. This can be edited at runtime to toggle between different safe areas.
        /// </summary>
        public static SimDevice Sim = SimDevice.None;

        /// <summary>
        /// Normalised safe areas for iPhone X with Home indicator (ratios are identical to iPhone Xs). Absolute values:
        ///  PortraitU x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436;
        ///  PortraitD x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436 (not supported, remains in Portrait Up);
        ///  LandscapeL x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125;
        ///  LandscapeR x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125.
        ///  Aspect Ratio: ~19.5:9.
        /// </summary>
        protected Rect[] NSA_iPhoneX = new Rect[]
        {
            new Rect (0f, 102f / 2436f, 1f, 2202f / 2436f),  // Portrait
            new Rect (132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f)  // Landscape
        };

        /// <summary>
        /// Normalised safe areas for iPhone Xs Max with Home indicator (ratios are identical to iPhone XR). Absolute values:
        ///  PortraitU x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688;
        ///  PortraitD x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688 (not supported, remains in Portrait Up);
        ///  LandscapeL x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242;
        ///  LandscapeR x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242.
        ///  Aspect Ratio: ~19.5:9.
        /// </summary>
        protected Rect[] NSA_iPhoneXsMax = new Rect[]
        {
        new Rect (0f, 102f / 2688f, 1f, 2454f / 2688f),  // Portrait
        new Rect (132f / 2688f, 63f / 1242f, 2424f / 2688f, 1179f / 1242f)  // Landscape
        };

        #endregion

        #region Bigfoot variables
        /// <summary>
        /// The current safe area
        /// </summary>
        public static Rect CurrentSA;
        /// <summary>
        /// On Update event
        /// </summary>
        public static Action OnUpdate;
        #endregion

        protected RectTransform Panel;

        protected Rect LastSafeArea = new Rect(0, 0, 0, 0);
        /// <summary>
        /// Conform to screen safe area on X-axis (default true, disable to ignore)
        /// </summary>
        [SerializeField] bool ConformX = true;
        /// <summary>
        /// Conform to screen safe area on Y-axis (default true, disable to ignore)
        /// </summary>
        [SerializeField] bool ConformY = true;

        void Awake()
        {
            Panel = GetComponent<RectTransform>();

            if (Panel == null)
            {
                Debug.LogError("Cannot apply safe area - no RectTransform found on " + name);
                Destroy(gameObject);
            }

            Refresh();
        }

        void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            Rect safeArea = GetSafeArea();

            if (safeArea != LastSafeArea)
                ApplySafeArea(safeArea);
        }

        /// <summary>
        /// Gets the safe area of the mobile device. If it is running in editor, it gets the simulation area.
        /// </summary>
        /// <returns>The safe area.</returns>
        Rect GetSafeArea()
        {
            Rect safeArea = Screen.safeArea;
#if UNITY_EDITOR
            if (Application.isEditor && Sim != SimDevice.None)
            {
                Rect nsa = new Rect(0, 0, Screen.width, Screen.height);

                switch (Sim)
                {
                    case SimDevice.iPhoneX:
                        if (Screen.height > Screen.width)  // Portrait
                            nsa = NSA_iPhoneX[0];
                        else  // Landscape
                            nsa = NSA_iPhoneX[1];
                        break;
                    case SimDevice.iPhoneXsMax:
                        if (Screen.height > Screen.width)  // Portrait
                            nsa = NSA_iPhoneXsMax[0];
                        else  // Landscape
                            nsa = NSA_iPhoneXsMax[1];
                        break;
                    default:
                        break;
                }

                safeArea = new Rect(Screen.width * nsa.x, Screen.height * nsa.y, Screen.width * nsa.width, Screen.height * nsa.height);
            }
#endif
            CurrentSA = safeArea;

            return safeArea;
        }

        /// <summary>
        /// Applies the safe area.
        /// </summary>
        /// <param name="r">The safe area.</param>
        protected virtual void ApplySafeArea(Rect r)
        {
            LastSafeArea = r;

            // Ignore x-axis?
            if (!ConformX)
            {
                r.x = 0;
                r.width = Screen.width;
            }

            // Ignore y-axis?
            if (!ConformY)
            {
                r.y = 0;
                r.height = Screen.height;
            }

            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            Panel.anchorMin = anchorMin;
            Panel.anchorMax = anchorMax;

            OnUpdate?.Invoke();
        }
    }


}