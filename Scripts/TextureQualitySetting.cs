using UnityEngine;

namespace GraphicSettings
{
    public class TextureQualitySetting : MonoBehaviour
    {
        public enum Setting : int
        {
            FullRes = 0,
            HalfRes = 1,
            QuaterRes = 2,
            EighthRes = 3,
        }
        public Setting setting = Setting.FullRes;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            QualitySettings.masterTextureLimit = (int)setting;
        }
    }
}
