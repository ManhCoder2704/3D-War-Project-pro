using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private UISO _uiSO;
        [SerializeField] private Transform _uiContainer;

        private Dictionary<UIType, UIBase> _uiDict = new Dictionary<UIType, UIBase>();
        private Stack<UIBase> _popupStack = new Stack<UIBase>();
        private UIBase _currentUI;
        private UIBase _currentPopup;

        private UIBase GetUI(UIType uiType)
        {
            if (_uiDict.ContainsKey(uiType))
            {
                return _uiDict[uiType];
            }
            UIBase ui = Instantiate(_uiSO.GetUI(uiType), _uiContainer);
            ui.transform.localPosition = Vector3.zero;
            _uiDict.Add(uiType, ui);

            return ui;
        }

        public void OpenUI(UIType uIType)
        {
            UIBase ui = GetUI(uIType);
            if (ui == null || ui == _currentUI) return;
            if (ui.IsPopup)
            {
                if (_currentPopup != null)
                {
                    _currentPopup.gameObject.SetActive(false);
                    _popupStack.Push(_currentPopup);
                }
                ui.gameObject.SetActive(true);
                _currentPopup = ui;
            }
            else
            {
                if (_currentUI != null) _currentUI.gameObject.SetActive(false);
                _popupStack.Clear();
                _currentPopup?.gameObject.SetActive(false);
                ui.gameObject.SetActive(true);
                _currentUI = ui;
            }
        }

    }
}
