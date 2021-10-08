using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class ResolutionScalingSetting : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider.minValue = 0.1f;
            slider.maxValue = 1f;
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            QualitySettings.resolutionScalingFixedDPIFactor = value;
        }
    }
}
