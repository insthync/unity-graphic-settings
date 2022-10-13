using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class TextureQualitySetting : MonoBehaviour, IGraphicSetting
    {
        public enum Setting : int
        {
            FullRes = 0,
            HalfRes = 1,
            QuaterRes = 2,
            EighthRes = 3,
        }
        public const string SAVE_KEY = "GRAPHIC_SETTING_TEXTURE_QUALITY";
        public Setting setting = Setting.FullRes;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.masterTextureLimit == (int)setting);
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
                QualitySettings.masterTextureLimit = (int)setting;
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY) && QualityLevelSetting.IsCustomQualityLevel())
                QualitySettings.masterTextureLimit = PlayerPrefs.GetInt(SAVE_KEY);
        }
    }
}
