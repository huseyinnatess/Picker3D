using Cinemachine;
using Runtime.Signals;
using UnityEngine;
using float3 = Unity.Mathematics.float3;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion

        #region Private Variables
        
        private float3 _firstPosition;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _firstPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnReset()
        {
            transform.position = _firstPosition;
        }

        private void OnSetCameraTarget()
        {
            // var player = FindObjectOfType<PlayerManager>();
            // virtualCamera.Follow = player;
            // virtualCamera.LookAt = player;
        }
        
        private void UnsubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;    
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}