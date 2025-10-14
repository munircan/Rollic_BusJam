namespace _Main.Scripts.GamePlay.InputSystem.Interactables
{
    public interface IPressable : IInteractable
    {
        public void OnPressDown();
        
        public void OnPressUp();
    }
}