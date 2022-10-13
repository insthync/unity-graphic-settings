using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class AntiAliasingSetting : MonoBehaviour, IGraphicSetting
    {
        public enum Setting : int
        {
            Disabled = 0,
            x2 = 2,
            x4 = 4,
            x8 = 8,
        }
        public const string SAVE_KEY = "GRAPHIC_SETTING_ANTI_ALIASING";
        public Setting setting = Setting.Disabled;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

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
            this._isOn = isOn;
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            if (ApplyImmediately)
            {
                _isOn = true;
                Apply();
            }
        }

        public void Apply()
        {
            if (_isOn)
            {
                QualitySettings.antiAliasing = (int)setting;
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY) && QualityLevelSetting.IsCustomQualityLevel())
                QualitySettings.antiAliasing = PlayerPrefs.GetInt(SAVE_KEY);
        }
    }
}
