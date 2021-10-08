using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class ScreenResolutionSetting : MonoBehaviour
    {
        public Dropdown dropdown;
        public Text text;
        public string format = "{0}x{1} @ {2}Hz";
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
            options.Clear();
            foreach (Resolution resolution in Screen.resolutions)
            {
                options.Add(string.Format(format, resolution.width, resolution.height, resolution.refreshRate));
                if (resolution.width == Screen.currentResolution.width &&
                    resolution.height == Screen.currentResolution.height &&
                    resolution.refreshRate == Screen.currentResolution.refreshRate)
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
                text.text = string.Format(format, Screen.currentResolution.width, Screen.currentResolution.height, Screen.currentResolution.refreshRate);
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
            Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, Screen.fullScreenMode, Screen.resolutions[value].refreshRate);
        }
    }
}
