namespace Insthync.GraphicSettings
{
    public interface IGraphicSetting
    {
        bool ApplyImmediately { get; set; }
        void Apply();
    }
}
