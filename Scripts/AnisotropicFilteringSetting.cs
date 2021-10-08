using UnityEngine;

namespace GraphicSettings
{
    public class AnisotropicFilteringSetting : MonoBehaviour
    {
        public AnisotropicFiltering setting = AnisotropicFiltering.Disable;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.anisotropicFiltering = setting;
        }
    }
}
