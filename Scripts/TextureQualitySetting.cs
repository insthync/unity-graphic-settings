using UnityEngine;
using UnityEngine.UI;

namespace GraphicSettings
{
    public class TextureQualitySetting : MonoBehaviour, IGraphicSetting
    {
        public enum Setting : int
        {
            FullRes = 0,
            HalfRes = 1,
            QuaterRes = 2,
            EighthRes = 3,
        }
        public Setting setting = Setting.FullRes;
        public Toggle toggle;
        public Button button;
        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        private bool isOn;

        private void Start()
        {
            if (toggle != null)
            {
                toggle.SetIsOnWithoutNotify(QualitySettings.masterTextureLimit == (int)setting);
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
            this.isOn = isOn;
            if (isOn)
                OnClick();
        }

        public void OnClick()
        {
            if (ApplyImmediately)
                Apply();
        }

        public void Apply()
        {
            if (isOn)
                QualitySettings.masterTextureLimit = (int)setting;
        }
    }
}
