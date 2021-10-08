using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class QualityLevelSetting : MonoBehaviour
    {
        public Toggle toggle;
        public int setting;

        private void Start()
        {
            if (toggle != null)
                toggle.SetIsOnWithoutNotify(QualitySettings.GetQualityLevel() == setting);
        }

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.SetQualityLevel(setting);
        }
    }
}
