using System;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Sirenix.OdinInspector;
using UnityEngine;
using float2 = Unity.Mathematics.float2;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMovementData _playerMovementData;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _xValue;
        private float2 _clampValues;

        #endregion

        #endregion

        internal void SetData(PlayerMovementData playerMovementData)
        {
            _playerMovementData = playerMovementData;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToMove)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }

            else
            {
                StopHorizontalMovement();
            }
        }

        private void StopPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void StopHorizontalMovement()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _playerMovementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void MovePlayer()
        {
            Vector3 velocity = rigidbody.velocity;
            velocity = new Vector3(_xValue * _playerMovementData.SidewaySpeed, velocity.y,
                _playerMovementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position = rigidbody.position;
            position = new Vector3(Mathf.Clamp(position.x, _clampValues.x, _clampValues.y),
                (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
        }

        internal void IsReadyToMove(bool isReady)
        {
            _isReadyToMove = isReady;
        }

        internal void IsReadyToPlay(bool isReady)
        {
            _isReadyToPlay = isReady;
        }

        internal void UpdateInputParams(HorizontalInputParams inputParams)
        {
            _xValue = inputParams.HorizontalValue;
            _clampValues = inputParams.ClampValues;
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }
    }
}