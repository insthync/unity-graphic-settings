using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Insthync.GraphicSettings
{
    public class UrpShadowResolutionSetting : BaseUrpSetting
    {
        public enum Setting : int
        {
            Off = 0,
            VeryLow = 256,
            Low = 512,
            Medium = 1024,
            High = 2048,
            VeryHigh = 4096,
        }
        public const string SAVE_KEY = "URP_GRAPHIC_SETTING_SHADOW_RESOLUTION";
        public Setting setting = Setting.High;
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
                SetValue(GetValue());
        }

        public static Setting GetValue()
        {
            var asset = GetUniversalRenderPipelineAsset();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo mainLightShadowsSupportedFieldInfo = asset.GetType().GetField("m_MainLightShadowsSupported", flags);
            bool isEnabled = (bool)mainLightShadowsSupportedFieldInfo.GetValue(asset);
            if (!isEnabled)
                return Setting.Off;
            FieldInfo mainLightShadowResolutionFieldInfo = asset.GetType().GetField("m_MainLightShadowmapResolution", flags);
            int resolution = (int)mainLightShadowResolutionFieldInfo.GetValue(asset);
            if (resolution >= (int)Setting.VeryHigh)
                return Setting.VeryHigh;
            if (resolution >= (int)Setting.High)
                return Setting.High;
            if (resolution >= (int)Setting.Medium)
                return Setting.Medium;
            if (resolution >= (int)Setting.Low)
                return Setting.Low;
            if (resolution >= (int)Setting.VeryLow)
                return Setting.VeryLow;
            return Setting.Off;
        }

        public static void SetValue(Setting value)
        {
            var asset = GetUniversalRenderPipelineAsset();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo mainLightShadowResolutionFieldInfo = asset.GetType().GetField("m_MainLightShadowmapResolution", flags);
            FieldInfo additionalLightShadowResolutionFieldInfo = asset.GetType().GetField("m_AdditionalLightsShadowmapResolution", flags);
            FieldInfo mainLightShadowsSupportedFieldInfo = asset.GetType().GetField("m_MainLightShadowsSupported", flags);
            FieldInfo additionalLightShadowsSupportedFieldInfo = asset.GetType().GetField("m_AdditionalLightShadowsSupported", flags);

            bool isEnabled = value != Setting.Off;
            if (isEnabled)
            {
                mainLightShadowResolutionFieldInfo.SetValue(asset, (int)value);
                additionalLightShadowResolutionFieldInfo.SetValue(asset, (int)value);
            }
            else
            {
                mainLightShadowResolutionFieldInfo.SetValue(asset, 256);
                additionalLightShadowResolutionFieldInfo.SetValue(asset, 256);
            }
            mainLightShadowsSupportedFieldInfo.SetValue(asset, isEnabled);
            additionalLightShadowsSupportedFieldInfo.SetValue(asset, isEnabled);
        }
    }
}
