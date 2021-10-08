using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class ResolutionScalingSetting : MonoBehaviour, IGraphicSetting
    {
        public Slider slider;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

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
            if (ApplyImmediately)
                Apply();
        }

        public void Apply()
        {
            QualitySettings.resolutionScalingFixedDPIFactor = slider.value;
        }
    }
}
