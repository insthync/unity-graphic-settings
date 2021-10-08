using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class AntiAliasingSetting : MonoBehaviour
    {
        public enum Setting : int
        {
            Disabled = 0,
            x2 = 2,
            x4 = 4,
            x8 = 8,
        }
        public Setting setting = Setting.Disabled;
        public Toggle toggle;
        public Button button;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.antiAliasing == (int)setting);
                toggle.onValueChanged.AddListener(OnToggle);
            }
            if (button != null)
                button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (toggle != null)
                toggle.onValueChanged.RemoveListener(OnToggle);
            if (button != null)
                button.onClick.RemoveListener(OnClick);
        }

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.antiAliasing = (int)setting;
        }
    }
}
