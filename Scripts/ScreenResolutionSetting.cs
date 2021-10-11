using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class ScreenResolutionSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY_SCREEN_WIDTH = "GRAPHIC_SETTING_SCREEN_WIDTH";
        public const string SAVE_KEY_SCREEN_HEIGHT = "GRAPHIC_SETTING_SCREEN_HEIGHT";
        public const string SAVE_KEY_REFRESH_RATE = "GRAPHIC_SETTING_REFRESH_RATE";
        public Dropdown dropdown;
        public Text text;
        public string format = "{0}x{1} @ {2}Hz";
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private FullScreenMode dirtyFullScreenMode;
        private List<string> options = new List<string>();
        private int currentSetting;

        private void Start()
        {
            if (dropdown != null)
                dropdown.onValueChanged.AddListener(OnValueChanged);
            UpdateOptions();
            dirtyFullScreenMode = Screen.fullScreenMode;
        }

        private void OnDestroy()
        {
            if (dropdown != null)
                dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Update()
        {
            if (dirtyFullScreenMode != Screen.fullScreenMode)
            {
                dirtyFullScreenMode = Screen.fullScreenMode;
                UpdateOptions();
            }
        }

        public void UpdateOptions()
        {
            currentSetting = 0;
            options.Clear();
            foreach (Resolution resolution in Screen.resolutions)
            {
                options.Add(string.Format(format, resolution.width, resolution.height, resolution.refreshRate));
                if (resolution.width == Screen.width &&
                    resolution.height == Screen.height &&
                    resolution.refreshRate == PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE))
                {
                    currentSetting = options.Count - 1;
                }
            }
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                dropdown.AddOptions(options);
                dropdown.SetValueWithoutNotify(currentSetting);
            }
            if (text != null)
                text.text = options[currentSetting];
        }

        public void OnClickPrevious()
        {
            OnValueChanged(currentSetting - 1);
        }

        public void OnClickNext()
        {
            OnValueChanged(currentSetting + 1);
        }

        public void OnValueChanged(int value)
        {
            if (value < 0)
                value = 0;
            if (value >= options.Count)
                value = options.Count - 1;
            currentSetting = value;
            if (text != null)
                text.text = options[value];
            if (ApplyImmediately)
                Apply();
        }

        public void Apply()
        {
            int screenWidth = Screen.resolutions[currentSetting].width;
            int screenHeight = Screen.resolutions[currentSetting].height;
            int refreshRate = Screen.resolutions[currentSetting].refreshRate;
            PlayerPrefs.SetInt(SAVE_KEY_SCREEN_WIDTH, screenWidth);
            PlayerPrefs.SetInt(SAVE_KEY_SCREEN_HEIGHT, screenHeight);
            PlayerPrefs.SetInt(SAVE_KEY_REFRESH_RATE, refreshRate);
            PlayerPrefs.Save();
            Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreenMode, refreshRate);
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY_SCREEN_WIDTH) &&
                PlayerPrefs.HasKey(SAVE_KEY_SCREEN_HEIGHT) &&
                PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE))
                Screen.SetResolution(PlayerPrefs.GetInt(SAVE_KEY_SCREEN_WIDTH), PlayerPrefs.GetInt(SAVE_KEY_SCREEN_HEIGHT), Screen.fullScreenMode, PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE));
        }
    }
}
