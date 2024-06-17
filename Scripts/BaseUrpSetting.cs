using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GraphicSettings
{
    public abstract class BaseUrpSetting : MonoBehaviour, IGraphicSetting
    {
        public static UniversalRenderPipelineAsset GetUniversalRenderPipelineAsset()
        {
            return (UniversalRenderPipelineAsset)QualitySettings.GetRenderPipelineAssetAt(QualitySettings.GetQualityLevel());
        }

        public bool applyImmediately = true;
        public bool ApplyImmediately { get { return applyImmediately; } set { applyImmediately = value; } }

        public abstract void Apply();
    }
}
