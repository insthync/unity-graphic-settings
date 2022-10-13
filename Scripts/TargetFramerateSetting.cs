using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class TargetFramerateSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY = "GRAPHIC_SETTING_TARGET_FRAME_RATE";
        public int setting = 60;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(Application.targetFrameRate == setting);
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
                Application.targetFrameRate = setting;
                PlayerPrefs.SetInt(SAVE_KEY, setting);
                PlayerPrefs.Save();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
                Application.targetFrameRate = PlayerPrefs.GetInt(SAVE_KEY);
        }
    }
}
