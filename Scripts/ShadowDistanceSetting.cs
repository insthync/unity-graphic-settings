using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Insthync.GraphicSettings
{
    public class ShadowDistanceSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY = "GRAPHIC_SETTING_SHADOW_DISTANCE";
        public Slider slider;
        public float valueChangeStepOnClick = 1f;
        public Button buttonIncrease;
        public Button buttonDecrease;
        public Text textValue;
        public TMP_Text tmpTextValue;
        public string valueFormat = "{0}";
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private void Start()
        {
            slider.SetValueWithoutNotify(QualitySettings.shadowDistance);
            slider.onValueChanged.AddListener(OnValueChanged);
            if (buttonIncrease != null)
                buttonIncrease.onClick.AddListener(OnClickIncrease);
            if (buttonDecrease != null)
                buttonDecrease.onClick.AddListener(OnClickDecrease);
            if (textValue != null)
                textValue.text = string.Format(valueFormat, slider.value.ToString("N0"));
            if (tmpTextValue != null)
                tmpTextValue.text = string.Format(valueFormat, slider.value.ToString("N0"));
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
            if (buttonIncrease != null)
                buttonIncrease.onClick.RemoveListener(OnClickIncrease);
            if (buttonDecrease != null)
                buttonDecrease.onClick.RemoveListener(OnClickDecrease);
        }

        public void OnClickIncrease()
        {
            slider.value += valueChangeStepOnClick;
        }

        public void OnClickDecrease()
        {
            slider.value -= valueChangeStepOnClick;
        }

        public void OnValueChanged(float value)
        {
            if (ApplyImmediately)
                Apply();
            if (textValue != null)
                textValue.text = string.Format(valueFormat, value.ToString("N0"));
            if (tmpTextValue != null)
                tmpTextValue.text = string.Format(valueFormat, value.ToString("N0"));
        }

        public void Apply()
        {
            QualitySettings.shadowDistance = slider.value;
            PlayerPrefs.SetFloat(SAVE_KEY, slider.value);
            PlayerPrefs.Save();
            QualityLevelSetting.MarkAsCustomLevel();
        }

        public static void Load()
        {
            if (QualityLevelSetting.IsCustomQualityLevel())
                QualitySettings.shadowDistance = PlayerPrefs.GetFloat(SAVE_KEY, QualitySettings.shadowDistance);
        }
    }
}
