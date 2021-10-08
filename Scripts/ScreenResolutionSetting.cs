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
            List<Resolution> resolutions = new List<Resolution>(Screen.resolutions);
            resolutions.Sort((a, b) =>
            {
                return ((float)a.width + ((float)a.refreshRate * 0.1f)).CompareTo((float)b.width + ((float)b.refreshRate * 0.1f));
            });
            foreach (Resolution resolution in resolutions)
            {
                options.Add(string.Format(format, resolution.width, resolution.height, resolution.refreshRate));
            }
            if (dropdown != null)
            {
                dropdown.ClearOptions();
                dropdown.AddOptions(options);
            }
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
        }
    }
}
