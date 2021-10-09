namespace GraphicSettings
{
    public static class GraphicSettingInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            QualityLevelSetting.Load();
            AnisotropicFilteringSetting.Load();
            AntiAliasingSetting.Load();
            TextureQualitySetting.Load();
            ShadowsSetting.Load();
            TargetFramerateSetting.Load();
            ResolutionScalingSetting.Load();
        }
    }
}
