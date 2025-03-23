using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Insthync.GraphicSettings
{
    public class UrpPostProcessSetting : MonoBehaviour, IGraphicSetting
    {
        public enum Setting : int
        {
            Off = 0,
            On = 1,
        }
        public const string SAVE_KEY = "URP_POST_PROCESS_SETTING";
        public static Setting CurrentSetting = Setting.On;
        public Setting setting = Setting.On;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(GetValue() == setting);
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
                SetValue(setting);
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
            }
        }

        public static void Load()
        {
            SetValue((Setting)PlayerPrefs.GetInt(SAVE_KEY, (int)GetValue()));
        }

        public static Setting GetValue()
        {
            return CurrentSetting;
        }

        public static void SetValue(Setting setting)
        {
            CurrentSetting = setting;
            bool enabled = setting == Setting.On;
            Volume[] volumes = FindObjectsOfType<Volume>();
            foreach (Volume volume in volumes)
            {
                volume.enabled = enabled;
            }
        }
    }
}
