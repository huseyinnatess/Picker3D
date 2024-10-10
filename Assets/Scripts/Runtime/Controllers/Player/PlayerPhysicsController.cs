using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        private const string _stageArea = "StageArea";
        private const string _finishArea = "FinishArea";
        private const string _miniGame = "MiniGame";

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tag: _stageArea))
            {
                playerManager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();

                // Stage area kontrol süreci yazılacak
            }

            if (other.CompareTag(tag: _finishArea))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
                return;
            }

            if (other.CompareTag(_miniGame))
            {
                // MiniGame yazılacak
            }
        }
    }
}