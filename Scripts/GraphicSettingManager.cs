using UnityEngine;

namespace GraphicSettings
{
    public class GraphicSettingManager : MonoBehaviour
    {
        private IGraphicSetting[] _graphicSettings;

        private void Start()
        {
            _graphicSettings = GetComponentsInChildren<IGraphicSetting>(true);
        }

        public void Apply()
        {
            foreach (IGraphicSetting graphicSetting in _graphicSettings)
            {
                graphicSetting.Apply();
            }
        }
    }
}
