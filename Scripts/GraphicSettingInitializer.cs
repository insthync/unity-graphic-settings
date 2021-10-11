namespace GraphicSettings
{
    public static class GraphicSettingInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            FullScreenSetting.Load();
            ScreenResolutionSetting.Load();
            QualityLevelSetting.Load();
            AnisotropicFilteringSetting.Load();
            AntiAliasingSetting.Load();
            TextureQualitySetting.Load();
            ShadowsSetting.Load();
            ShadowDistanceSetting.Load();
            SoftParticlesSetting.Load();
            RealtimeReflectionProbesSetting.Load();
            TargetFramerateSetting.Load();
            ResolutionScalingSetting.Load();
        }
    }
}
