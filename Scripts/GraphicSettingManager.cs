using UnityEngine;

namespace GraphicSettings
{
    public class GraphicSettingManager : MonoBehaviour
    {
        private IGraphicSetting[] graphicSettings;

        private void Start()
        {
            graphicSettings = GetComponentsInChildren<IGraphicSetting>(true);
        }

        public void Apply()
        {
            foreach (IGraphicSetting graphicSetting in graphicSettings)
            {
                graphicSetting.Apply();
            }
        }
    }
}
