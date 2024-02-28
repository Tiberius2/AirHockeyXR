// TODO: [Optional] Add copyright and license statement(s).

using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using UnityEngine;
using UnityEngine.Scripting;

namespace tiberius.MRTK3.Subsystems
{
    [Preserve]
    [MRTKSubsystem(
        Name = "tiberius.mrtk3.subsystems",
        DisplayName = "tiberius NewSubsystem",
        Author = "tiberius",
        ProviderType = typeof(tiberiusNewSubsystemProvider),
        SubsystemTypeOverride = typeof(tiberiusNewSubsystem),
        ConfigType = typeof(BaseSubsystemConfig))]
    public class tiberiusNewSubsystem : NewSubsystem
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Register()
        {
            // Fetch subsystem metadata from the attribute.
            var cinfo = XRSubsystemHelpers.ConstructCinfo<tiberiusNewSubsystem, NewSubsystemCinfo>();

            if (!tiberiusNewSubsystem.Register(cinfo))
            {
                Debug.LogError($"Failed to register the {cinfo.Name} subsystem.");
            }
        }

        [Preserve]
        class tiberiusNewSubsystemProvider : Provider
        {

            #region INewSubsystem implementation

            // TODO: Add the provider implementation.

            #endregion NewSubsystem implementation
        }
    }
}
