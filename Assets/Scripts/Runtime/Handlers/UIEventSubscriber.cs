using System;
using Runtime.Enums.UI;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Fields

        [SerializeField] private UIEventSubscriptionType type;
        [SerializeField] Button button;
        
        #endregion

        #region Private Variables

        private UIManager _uiManager;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionType.OnPlay:
                    button.onClick.AddListener(_uiManager.Play);
                    break;
                case UIEventSubscriptionType.OnNextLevel:
                    button.onClick.AddListener(_uiManager.NextLevel);
                    break;
                case UIEventSubscriptionType.OnRestartLevel:
                    button.onClick.AddListener(_uiManager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UnsubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionType.OnPlay:
                    button.onClick.RemoveListener(_uiManager.Play);
                    break;
                case UIEventSubscriptionType.OnNextLevel:
                    button.onClick.RemoveListener(_uiManager.NextLevel);
                    break;
                case UIEventSubscriptionType.OnRestartLevel:
                    button.onClick.RemoveListener(_uiManager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}