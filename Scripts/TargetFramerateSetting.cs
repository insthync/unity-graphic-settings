using UnityEngine;

namespace GraphicSettings
{
    public class TargetFramerateSetting : MonoBehaviour
    {
        public int setting = 60;

        public void OnToggle(bool isOn)
        {
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            Application.targetFrameRate = setting;
        }
    }
}
