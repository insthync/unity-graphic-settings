using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class ScreenResolutionSetting : MonoBehaviour, IGraphicSetting
    {
        public const string SAVE_KEY_SCREEN_WIDTH = "GRAPHIC_SETTING_SCREEN_WIDTH";
        public const string SAVE_KEY_SCREEN_HEIGHT = "GRAPHIC_SETTING_SCREEN_HEIGHT";
#if UNITY_2022_2_OR_NEWER
        public const string SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR = "GRAPHIC_SETTING_REFRESH_RATE_RATIO_NUMERATOR";
        public const string SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR = "GRAPHIC_SETTING_REFRESH_RATE_RATIO_DENOMINATOR";
#else
        public const string SAVE_KEY_REFRESH_RATE = "GRAPHIC_SETTING_REFRESH_RATE";
#endif
        public Dropdown dropdown;
        public TMPro.TMP_Dropdown tmpDropdown;
        public Text text;
        public TMPro.TMP_Text tmpText;
        public string format = "{0}x{1} @ {2}Hz";
        public string customFormat = "{0}x{1} @ {2}Hz (Custom)";
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private FullScreenMode _dirtyFullScreenMode;
        private List<string> _options = new List<string>();
        private List<Resolution> _resolutions = new List<Resolution>();
        private bool _hasCustomResolution;
        private int _currentSetting;

        private void Start()
        {
            if (dropdown != null)
                dropdown.onValueChanged.AddListener(OnValueChanged);
            if (tmpDropdown != null)
                tmpDropdown.onValueChanged.AddListener(OnValueChanged);
            UpdateOptions();
            _dirtyFullScreenMode = Screen.fullScreenMode;
        }

        private void OnDestroy()
        {
            if (dropdown != null)
                dropdown.onValueChanged.RemoveListener(OnValueChanged);
            if (tmpDropdown != null)
                tmpDropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Update()
        {
            if (_dirtyFullScreenMode != Screen.fullScreenMode)
            {
                _dirtyFullScreenMode = Screen.fullScreenMode;
                UpdateOptions();
            }
        }

        public void UpdateOptions()
        {
            _currentSetting = -1;
            _hasCustomResolution = false;
            _options.Clear();
            _resolutions.Clear();
            foreach (Resolution resolution in Screen.resolutions)
            {
#if UNITY_2022_2_OR_NEWER
                _options.Add(string.Format(format, resolution.width, resolution.height, resolution.refreshRateRatio.value.ToString("N2")));
#else
                _options.Add(string.Format(format, resolution.width, resolution.height, resolution.refreshRate.ToString("N2")));
#endif
                _resolutions.Add(resolution);
                if (resolution.width != Screen.width || resolution.height != Screen.height)
                    continue;

#if UNITY_2022_2_OR_NEWER
                if (!PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR) || !PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR) || resolution.refreshRateRatio.Equals(new RefreshRate()
                {
                    numerator = (uint)PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR, 50),
                    denominator = (uint)PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR, 1),
                }))
                {
                    _currentSetting = _options.Count - 1;
                }
#else
                if (!PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE) || resolution.refreshRate == PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE, 50))
                {
                    _currentSetting = _options.Count - 1;
                }
#endif
            }
            if (_currentSetting < 0)
            {
#if UNITY_2022_2_OR_NEWER
                _options.Add(string.Format(customFormat, Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRateRatio.value.ToString("N2")));
#else
                _options.Add(string.Format(customFormat, Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRate.ToString("N2")));
#endif

#if UNITY_2022_2_OR_NEWER
                _resolutions.Add(new Resolution()
                {
                    width = Screen.currentResolution.width,
                    height = Screen.currentResolution.height,
                    refreshRateRatio = Screen.currentResolution.refreshRateRatio,
                });
#else
                _resolutions.Add(new Resolution()
                {
                    width = Screen.currentResolution.width,
                    height = Screen.currentResolution.height,
                    refreshRate = Screen.currentResolution.refreshRate,
                });
#endif
                _currentSetting = _options.Count - 1;
                _hasCustomResolution = true;
            }
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                dropdown.AddOptions(_options);
                dropdown.SetValueWithoutNotify(_currentSetting);
            }
            if (tmpDropdown != null)
            {
                tmpDropdown.ClearOptions();
                tmpDropdown.AddOptions(_options);
                tmpDropdown.SetValueWithoutNotify(_currentSetting);
            }
            if (text != null)
                text.text = _options[_currentSetting];
            if (tmpText != null)
                tmpText.text = _options[_currentSetting];
        }

        public void OnClickPrevious()
        {
            OnValueChanged(_currentSetting - 1);
        }

        public void OnClickNext()
        {
            OnValueChanged(_currentSetting + 1);
        }

        public void OnValueChanged(int value)
        {
            if (value < 0)
                value = 0;
            if (value >= _options.Count)
                value = _options.Count - 1;
            _currentSetting = value;
            if (text != null)
                text.text = _options[value];
            if (tmpText != null)
                tmpText.text = _options[value];
            if (ApplyImmediately)
                Apply();
        }

        public void Apply()
        {
            if (_hasCustomResolution && _currentSetting < _resolutions.Count - 1)
            {
                _resolutions.RemoveAt(_resolutions.Count - 1);
                _hasCustomResolution = false;
            }
            int screenWidth = _resolutions[_currentSetting].width;
            PlayerPrefs.SetInt(SAVE_KEY_SCREEN_WIDTH, screenWidth);

            int screenHeight = _resolutions[_currentSetting].height;
            PlayerPrefs.SetInt(SAVE_KEY_SCREEN_HEIGHT, screenHeight);
#if UNITY_2022_2_OR_NEWER
            RefreshRate refreshRateRatio = _resolutions[_currentSetting].refreshRateRatio;
            PlayerPrefs.SetInt(SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR, (int)refreshRateRatio.numerator);
            PlayerPrefs.SetInt(SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR, (int)refreshRateRatio.denominator);
            Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreenMode, refreshRateRatio);
#else
            int refreshRate = _resolutions[_currentSetting].refreshRate;
            PlayerPrefs.SetInt(SAVE_KEY_REFRESH_RATE, refreshRate);
            Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreenMode, refreshRate);
#endif
            PlayerPrefs.Save();
        }

        public static void Load()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY_SCREEN_WIDTH) || !PlayerPrefs.HasKey(SAVE_KEY_SCREEN_HEIGHT))
                return;
#if UNITY_2022_2_OR_NEWER
            if (!PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR) || !PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR))
                return;
            Screen.SetResolution(PlayerPrefs.GetInt(SAVE_KEY_SCREEN_WIDTH), PlayerPrefs.GetInt(SAVE_KEY_SCREEN_HEIGHT), Screen.fullScreenMode, new RefreshRate()
            {
                numerator = (uint)PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE_RATIO_NUMERATOR),
                denominator = (uint)PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE_RATIO_DENOMINATOR),
            });
#else
            if (!PlayerPrefs.HasKey(SAVE_KEY_REFRESH_RATE))
                return;
            Screen.SetResolution(PlayerPrefs.GetInt(SAVE_KEY_SCREEN_WIDTH), PlayerPrefs.GetInt(SAVE_KEY_SCREEN_HEIGHT), Screen.fullScreenMode, PlayerPrefs.GetInt(SAVE_KEY_REFRESH_RATE));

#endif
        }
    }
}
