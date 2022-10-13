using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class SoftParticlesSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY = "GRAPHIC_SETTING_SOFT_PARTICLES";
        public bool setting;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.softParticles == setting);
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
                QualitySettings.softParticles = setting;
                PlayerPrefs.SetInt(SAVE_KEY, setting ? 1 : 0);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY) && QualityLevelSetting.IsCustomQualityLevel())
                QualitySettings.softParticles = PlayerPrefs.GetInt(SAVE_KEY) > 0;
        }
    }
}
