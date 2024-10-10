using System.Collections.Generic;
using System.Linq;
using Runtime.Enums.UI;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] private List<Transform> layers;

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

        [Button("OpenPanel")]
        private void OpenPanel(UIPanelTypes panelType, int index)
        {
            ClosePanel(index);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[index]);

        }

        [Button("ClosePanel")]
        private void ClosePanel(int index)
        {
            if (layers[index].childCount <= 0) return;
#if UNITY_EDITOR
            DestroyImmediate(layers[index].GetChild(0).gameObject);
#else
            Destroy(layers[index].GetChild(0).gameObject);
#endif
        }

        [Button("CloseAllPanels")]
        private void CloseAllPanels()
        {
            foreach (var layer in layers.Where(layer => layer.childCount > 0))
            {
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
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