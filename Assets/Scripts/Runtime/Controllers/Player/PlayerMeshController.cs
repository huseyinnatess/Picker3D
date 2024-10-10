using System;
using DG.Tweening;
using Runtime.Data.ValueObjects;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Seralized Variables

        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confetti;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerMeshData _playerMeshData;

        #endregion

        #endregion

        internal void SetData(PlayerMeshData playerMeshData)
        {
            _playerMeshData = playerMeshData;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_playerMeshData.ScaleCounter, 1).SetEase(Ease.Flash);
        }

        internal void ShowUpText()
        {
            scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                scaleText.DOFade(0, .3f).SetDelay(.35f);
                scaleText.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
            });
        }

        internal void ShowConfetti()
        {
            confetti.Play();
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
    }
}