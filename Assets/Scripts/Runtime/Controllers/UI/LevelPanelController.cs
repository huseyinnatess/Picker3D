using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables
        
        [SerializeField] private List<Image> stageImages = new List<Image>();
        [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue += OnSetLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
        }
        
        [Button("SetStageColor")]
        private void OnSetStageColor(byte stageValue)
        {
            stageImages[stageValue].DOColor(new Color(0.9960784f, 0.4117647f, 0.02352941f), 0.5f);
        }
        
        private void OnSetLevelValue(byte levelValue)
        {
            levelTexts[0].text = (++levelValue).ToString();
            levelTexts[1].text = (levelValue + 1).ToString();
        }


        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}