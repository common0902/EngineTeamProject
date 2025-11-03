namespace _00.Work.Yeonwoo._01.Scripts.Interfaces
{
    public interface ISettingPanel
    {
        bool IsOpen { get; }
        void Open();
        void Close();
    }
}