using UnityEngine;

namespace GraphicSettings
{
    public class TextureQualitySetting : MonoBehaviour
    {
        public int value = 0;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.masterTextureLimit = 0;
        }
    }
}
