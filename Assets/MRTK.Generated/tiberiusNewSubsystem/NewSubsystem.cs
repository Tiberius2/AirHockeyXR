// TODO: [Optional] Add copyright and license statement(s).

using MixedReality.Toolkit.Subsystems;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace tiberius.MRTK3.Subsystems
{
    [Preserve]
    public class NewSubsystem :
        MRTKSubsystem<NewSubsystem, NewSubsystemDescriptor, NewSubsystem.Provider>,
        INewSubsystem
    {
        /// <summary>
        /// Construct the <c>NewSubsystem</c>.
        /// </summary>
        public NewSubsystem()
        { }

        [Preserve]
        public abstract class Provider : MRTKSubsystemProvider<NewSubsystem>, INewSubsystem
        {
            #region INewSubsystem implementation

            // TODO: Implement abstract Provider class.

            #endregion INewSubsystem implementation
        }

        #region INewSubsystem implementation

        // TODO: Calls into abstract Provider (ex: public int MaxValue => provider.MaxValue;

        #endregion INewSubsystem implementation

        /// <summary>
        /// Registers a NewSubsystem implementation based on the given subsystem parameters.
        /// </summary>
        /// <param name="subsystemParams">The parameters defining the NewSubsystem
        /// functionality implemented by the subsystem provider.</param>
        /// <returns>
        /// <c>true</c> if the subsystem implementation is registered. Otherwise, <c>false</c>.
        /// </returns>
        public static bool Register(NewSubsystemCinfo subsystemParams)
        {
            NewSubsystemDescriptor Descriptor = NewSubsystemDescriptor.Create(subsystemParams);
            SubsystemDescriptorStore.RegisterDescriptor(Descriptor);
            return true;
        }
    }
}
