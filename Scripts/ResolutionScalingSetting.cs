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
            slider.SetValueWithoutNotify(QualitySettings.resolutionScalingFixedDPIFactor);
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void OnValueChanged(float value)
        {
            QualitySettings.resolutionScalingFixedDPIFactor = value;
        }
    }
}
