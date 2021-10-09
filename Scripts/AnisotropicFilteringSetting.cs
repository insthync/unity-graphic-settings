﻿using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class AnisotropicFilteringSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY = "GRAPHIC_SETTING_ANISOTROPIC_FILTERING";
        public AnisotropicFiltering setting = AnisotropicFiltering.Disable;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.anisotropicFiltering == setting);
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
            this.isOn = isOn;
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            if (ApplyImmediately)
                Apply();
        }

        public void Apply()
        {
            if (isOn)
            {
                QualitySettings.anisotropicFiltering = setting;
                PlayerPrefs.SetInt(SAVE_KEY, (int)setting);
                PlayerPrefs.Save();
            }
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
                QualitySettings.anisotropicFiltering = (AnisotropicFiltering)PlayerPrefs.GetInt(SAVE_KEY);
        }
    }
}
