﻿using UnityEngine;
using UnityEngine.UI;

namespace Insthync.GraphicSettings
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
#if UNITY_2022_2_OR_NEWER
                toggle.SetIsOnWithoutNotify(QualitySettings.globalTextureMipmapLimit == (int)setting);
#else
                toggle.SetIsOnWithoutNotify(QualitySettings.masterTextureLimit == (int)setting);
#endif
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
#if UNITY_2022_2_OR_NEWER
                QualitySettings.globalTextureMipmapLimit = (int)setting;
#else
                QualitySettings.masterTextureLimit = (int)setting;
#endif
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
                QualityLevelSetting.MarkAsCustomLevel();
            }
        }

        public static void Load()
        {
            if (QualityLevelSetting.IsCustomQualityLevel())
            {
#if UNITY_2022_2_OR_NEWER
                QualitySettings.globalTextureMipmapLimit = PlayerPrefs.GetInt(SAVE_KEY, QualitySettings.globalTextureMipmapLimit);
#else
                QualitySettings.masterTextureLimit = PlayerPrefs.GetInt(SAVE_KEY, QualitySettings.masterTextureLimit);
#endif
            }
        }
    }
}
