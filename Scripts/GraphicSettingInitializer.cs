namespace GraphicSettings
{
    public static class GraphicSettingInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            try
            {
                QualityLevelSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                FullScreenSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                ScreenResolutionSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                AnisotropicFilteringSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                AntiAliasingSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                TextureQualitySetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                ShadowsSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                ShadowDistanceSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                SoftParticlesSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                RealtimeReflectionProbesSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                TargetFramerateSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                ResolutionScalingSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpAntiAliasingSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpShadowResolutionSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpShadowDistanceSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpShadowCascadeSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpSoftShadowSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            try
            {
                UrpResolutionScalingSetting.Load();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }
        }
    }
}
