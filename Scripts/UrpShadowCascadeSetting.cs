using UnityEngine;
using UnityEngine.UI;

namespace Insthync.GraphicSettings
{
    public class UrpShadowCascadeSetting : BaseUrpSetting
    {
        public enum Setting : int
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
        }
        public const string SAVE_KEY = "URP_GRAPHIC_SETTING_SHADOW_CASCADE";
        public Setting setting = Setting.One;
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
            int cascadeCount = asset.shadowCascadeCount;
            if (cascadeCount >= (int)Setting.Four)
                return Setting.Four;
            if (cascadeCount >= (int)Setting.Three)
                return Setting.Three;
            if (cascadeCount >= (int)Setting.Two)
                return Setting.Two;
            return Setting.One;
        }

        public static void SetValue(Setting value)
        {
            var asset = GetUniversalRenderPipelineAsset();
            asset.shadowCascadeCount = (int)value;
        }
    }
}
