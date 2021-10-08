using UnityEngine;

namespace GraphicSettings
{
    public class ShadowsSetting : MonoBehaviour
    {
        public ShadowQuality setting = ShadowQuality.Disable;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.shadows = setting;
        }
    }
}
