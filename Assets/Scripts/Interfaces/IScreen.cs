namespace TT.Interfaces
{
    public interface IScreen
    {
        void EnableScreen(bool requiredToCloseItself);
        void DisableScreen();
    }
}