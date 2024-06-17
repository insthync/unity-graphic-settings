using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class UrpSoftShadowSetting : BaseUrpSetting
    {
        public enum Setting : int
        {
            Off = 0,
            On = 1,
        }
        public const string SAVE_KEY = "URP_GRAPHIC_SETTING_SOFT_SHADOW";
        public Setting setting = Setting.On;
        public Toggle toggle;
        public Button button;

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

        public override void Apply()
        {
            if (_isOn)
            {
                SetValue(setting);
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (QualityLevelSetting.IsCustomQualityLevel())
                SetValue((Setting)PlayerPrefs.GetInt(SAVE_KEY, (int)GetValue()));
        }

        public static Setting GetValue()
        {
            var asset = GetUniversalRenderPipelineAsset();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo softShadowFieldInfo = asset.GetType().GetField("m_SoftShadowsSupported", flags);
            return (bool)softShadowFieldInfo.GetValue(asset) ? Setting.On : Setting.Off;
        }

        public static void SetValue(Setting setting)
        {
            var asset = GetUniversalRenderPipelineAsset();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo softShadowFieldInfo = asset.GetType().GetField("m_SoftShadowsSupported", flags);
            softShadowFieldInfo.SetValue(asset, setting == Setting.On);
        }
    }
}
