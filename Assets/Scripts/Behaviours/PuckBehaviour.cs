using System.Collections;
using UnityEngine;
using TT.Enums;
using TT.Managers;
using TT.Globals;
using TT.HelperClasses;
using System;

namespace TT.Behaviours
{
    public class PuckBehaviour : MonoBehaviour
    {
        public static bool WasGoal { get; private set; }

        [SerializeField] private float waitTime = 0.5f;
        [SerializeField] private Transform aiGoalTransfrom;
        [SerializeField] private Transform playerGoalTransform;

        private ParticleSystemManager particleSystemManager;
        private ScoreManager scoreManager;
        private Rigidbody rb;
        private Transform puckVisual;
        private Vector3 goalPosition;
        private Vector3 initialPosition;
        private string goalSound;
        private bool wasPlayerGoal = true;

        private void Start()
        {
            scoreManager = ScoreManager.Instance;
            particleSystemManager = ParticleSystemManager.Instance;
            initialPosition = transform.localPosition;
            puckVisual = transform.GetChild(0);
            rb = GetComponent<Rigidbody>();
            WasGoal = false;
            GameManager.Instance.AddToDictionaryForReset(gameObject, transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != GameConstants.HololensHandTag && other.tag != GameConstants.TimerCollider)
            {
                if (!WasGoal)
                {
                    if (other.tag == GameConstants.AIGoalTag)
                    {
                        scoreManager.IncrementScore(Score.PlayerScore);
                        scoreManager.FlashScore(Score.PlayerScore);
                        goalPosition = aiGoalTransfrom != null ? aiGoalTransfrom.position : transform.position;
                        goalPosition.y = goalPosition.y + GameConstants.ParticleOffset;
                        goalSound = GameConstants.PlayerPointWinSound;
                        wasPlayerGoal = true;
                    }
                    else if (other.tag == GameConstants.PlayerGoalTag)
                    {
                        scoreManager.IncrementScore(Score.AIScore);
                        scoreManager.FlashScore(Score.AIScore);
                        goalPosition = playerGoalTransform != null ? playerGoalTransform.position : transform.position;
                        goalPosition.y = goalPosition.y + GameConstants.ParticleOffset;
                        goalSound = GameConstants.AiPointWinSound;
                        wasPlayerGoal = false;
                    }
                    PlayGoalSound(goalSound);
                    StartParticleSystem(goalPosition);
                }
                if (!GameManager.Instance.IsGameOver)
                {
                    WasGoal = true;
                    StartCoroutine(ResetGame());
                    ResetPuckCallAction();
                }
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(GameConstants.CollisionLayer))
            {
                AudioManager.Instance.PlaySound(GameConstants.EdgeImpactSound);
            }
            else if (collision.gameObject.CompareTag(GameConstants.PlayerStrikerTag) || collision.gameObject.CompareTag(GameConstants.AiStrikerTag))
            {
                AudioManager.Instance.PlaySound(GameConstants.StrikerImpactSound);
            }
        }

        private void StartParticleSystem(Vector3 whichGoalPosition)
        {
            particleSystemManager?.PlayGoalConfettiAtPosition(whichGoalPosition);
        }

        private void PlayGoalSound(string soundName)
        {
            AudioManager.Instance.PlaySound(soundName);
        }

        private IEnumerator ResetGame()
        {
            var puckCollider = gameObject.GetComponent<SphereCollider>();
            puckCollider.enabled = false;
            puckVisual.gameObject.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(waitTime);
            WasGoal = false;
            puckVisual.gameObject.GetComponent<MeshRenderer>().enabled = true;
            puckCollider.enabled = true;
        }

        private void ResetPuckCallAction()
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            if (wasPlayerGoal)
            {
                rb.transform.localPosition = new Vector3(0, transform.localPosition.y, GameConstants.puckZPositionOnGoal);
            }
            else
            {
                rb.transform.localPosition = new Vector3(0, transform.localPosition.y, -GameConstants.puckZPositionOnGoal);
            }
        }
    }
}