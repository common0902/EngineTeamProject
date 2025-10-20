namespace _00.Work.Yeonwoo._01.Scripts.UI
{
    public interface ISettingPanel
    {
        bool IsOpen { get; }
        void Open();
        void Close();
    }
}