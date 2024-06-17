using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class UrpAntiAliasingSetting : BaseUrpSetting
    {
        public const string SAVE_KEY = "URP_GRAPHIC_SETTING_ANTI_ALIASING";
        public MsaaQuality setting = MsaaQuality.Disabled;
        public Toggle toggle;
        public Button button;

        private bool _isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(GetUniversalRenderPipelineAsset().msaaSampleCount == (int)setting);
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
                GetUniversalRenderPipelineAsset().msaaSampleCount = (int)setting;
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (QualityLevelSetting.IsCustomQualityLevel())
                GetUniversalRenderPipelineAsset().msaaSampleCount = PlayerPrefs.GetInt(SAVE_KEY, GetUniversalRenderPipelineAsset().msaaSampleCount);
        }
    }
}
