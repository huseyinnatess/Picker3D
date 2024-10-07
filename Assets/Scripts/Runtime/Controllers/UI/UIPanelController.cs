using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums.UI;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OpenPanel;
            CoreUISignals.Instance.onClosePanel += ClosePanel;
            CoreUISignals.Instance.onCloseAllPanels += CloseAllPanels;
        }

        private void OpenPanel(UIPanelTypes panelType, int index)
        {
            ClosePanel(index);
            Instantiate(Resources.Load<GameObject>($"Screens{panelType}Panel"), layers[index]);
        }

        private void ClosePanel(int index)
        {
            if (layers[index].childCount <= 0) return;
            Destroy(layers[index].GetChild(0).gameObject);
        }
        
        private void CloseAllPanels()
        {
            foreach (var layer in layers.Where(layer => layer.childCount > 0))
            {
                Destroy(layer.GetChild(0).gameObject);
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OpenPanel;
            CoreUISignals.Instance.onClosePanel -= ClosePanel;
            CoreUISignals.Instance.onCloseAllPanels -= CloseAllPanels;
        }
    }
}