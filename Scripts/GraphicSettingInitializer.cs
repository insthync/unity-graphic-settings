namespace GraphicSettings
{
    public static class GraphicSettingInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            QualityLevelSetting.Load();
            FullScreenSetting.Load();
            ScreenResolutionSetting.Load();
            AnisotropicFilteringSetting.Load();
            AntiAliasingSetting.Load();
            TextureQualitySetting.Load();
            ShadowsSetting.Load();
            ShadowDistanceSetting.Load();
            SoftParticlesSetting.Load();
            RealtimeReflectionProbesSetting.Load();
            TargetFramerateSetting.Load();
            ResolutionScalingSetting.Load();
            UrpAntiAliasingSetting.Load();
            UrpShadowResolutionSetting.Load();
            UrpShadowDistanceSetting.Load();
            UrpShadowCascadeSetting.Load();
            UrpSoftShadowSetting.Load();
            UrpResolutionScalingSetting.Load();
        }
    }
}
