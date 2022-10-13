using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class QualityLevelSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY = "GRAPHIC_SETTING_QUALITY_LEVEL";
        public const string SAVE_KEY_CUSTOM_LEVEL = "GRAPHIC_SETTING_QUALITY_LEVEL_CUSTOM";
        public int setting;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.GetQualityLevel() == setting);
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
                QualitySettings.SetQualityLevel(setting);
                PlayerPrefs.SetInt(SAVE_KEY, setting);
                PlayerPrefs.SetInt(SAVE_KEY_CUSTOM_LEVEL, 0);
                PlayerPrefs.Save();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
                QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(SAVE_KEY));
        }

        public static void MarkAsCustomLevel()
        {
            PlayerPrefs.SetInt(SAVE_KEY_CUSTOM_LEVEL, 1);
            PlayerPrefs.Save();
        }

        public static bool IsCustomQualityLevel()
        {
            return PlayerPrefs.GetInt(SAVE_KEY_CUSTOM_LEVEL, 0) > 0;
        }
    }
}
