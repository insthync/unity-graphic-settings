using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class AnisotropicFilteringSetting : MonoBehaviour
    {
        public AnisotropicFiltering setting = AnisotropicFiltering.Disable;
        public Toggle toggle;
        public Button button;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.anisotropicFiltering == setting);
                toggle.onValueChanged.AddListener(OnToggle);
            }
            if (button != null)
                button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (toggle != null)
                toggle.onValueChanged.RemoveListener(OnToggle);
            if (button != null)
                button.onClick.RemoveListener(OnClick);
        }

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
