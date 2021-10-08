using UnityEngine;

namespace GraphicSettings
{
    public class AntiAliasingSetting : MonoBehaviour
    {
        public enum Setting : int
        {
            Disabled = 0,
            x2 = 2,
            x4 = 4,
            x8 = 8,
        }
        public Setting setting = Setting.Disabled;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.antiAliasing = (int)setting;
        }
    }
}
