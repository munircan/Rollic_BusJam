namespace _Main.Scripts.GamePlay.InputSystem
{
    public interface IPressable : IInteractable
    {
        public void OnPressDown();
        
        public void OnPressUp();
    }
}