using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    [RequireComponent(typeof(Button))]
    public class QualityLevelSettingIncreaseButton : MonoBehaviour, IGraphicSetting
    {
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }
        public string[] qualitySettings = new string[] { "Low", "Medium", "High" };
        public int currentIndex = 0;
        public Text text;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            currentIndex = QualitySettings.GetQualityLevel();
            if (currentIndex >= qualitySettings.Length)
            {
                currentIndex = 0;
                Apply();
            }
            if (text != null)
                text.text = qualitySettings[currentIndex];
        }

        public void OnClick()
        {
            currentIndex++;
            Apply();
        }

        public void Apply()
        {
            if (currentIndex >= qualitySettings.Length)
                currentIndex = 0;
            if (text != null)
                text.text = qualitySettings[currentIndex];
            QualitySettings.SetQualityLevel(currentIndex);
            PlayerPrefs.SetInt(QualityLevelSetting.SAVE_KEY, currentIndex);
            PlayerPrefs.SetInt(QualityLevelSetting.SAVE_KEY_CUSTOM_LEVEL, 0);
            PlayerPrefs.Save();
        }
    }
}
