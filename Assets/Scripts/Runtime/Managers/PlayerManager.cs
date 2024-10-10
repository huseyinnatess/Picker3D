using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;
        internal ForceBallsToPoolCommand ForceCommand;

        #endregion

        #region Seralized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        #endregion

        #region Private Variables

        private PlayerData _playerData;

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SendDataToControllers();
            Init();
        }

        private void SendDataToControllers()
        {
            playerMovementController.SetData(_playerData.MovementData);
            playerMeshController.SetData(_playerData.MeshData);
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _playerData.ForceData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnInputTaken;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            UISignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessfull += OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessfull += OnStageAreaSuccessfull;
            CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnReset()
        {
            StageValue = 0;
            playerMovementController.OnReset();
            playerMeshController.OnReset();
        }

        private void OnFinishAreaEntered()
        {
            CoreGameSignals.Instance.onLevelSuccessfull?.Invoke();
            // Mini Game yazılaacak
        }

        private void OnStageAreaSuccessfull(byte levelValue)
        {
            StageValue = ++levelValue;
        }

        private void OnStageAreaEntered()
        {
            playerMovementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }

        private void OnLevelSuccessfull()
        {
            playerMovementController.IsReadyToPlay(false);
        }

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputParams(inputParams);
        }

        private void OnInputReleased()
        {
            playerMovementController.IsReadyToMove(false);
        }

        private void OnInputTaken()
        {
            playerMovementController.IsReadyToMove(true);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnInputTaken;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessfull -= OnLevelSuccessfull;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessfull -= OnStageAreaSuccessfull;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}