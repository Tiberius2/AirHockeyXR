using SingletonTemplate;
using UnityEngine;

namespace TT.Managers
{
    public class ParticleSystemManager : MonoSingleton<ParticleSystemManager>
    {
        [SerializeField] private ParticleSystem goalConfetti;

        public void PlayGoalConfettiAtPosition(Vector3 position)
        {
            if (goalConfetti != null)
            {
                goalConfetti.transform.position = position;
                goalConfetti.Play();
            }
        }
    }
}