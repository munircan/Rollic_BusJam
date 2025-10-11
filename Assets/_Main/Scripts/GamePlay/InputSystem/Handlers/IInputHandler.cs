namespace _Main.Scripts.GamePlay.InputSystem
{
    public interface IInputHandler
    {
        public bool IsHandling { get; protected set; }

        public void Cast();

        public void StartHandle(IInteractable interactable);
        
        public void StopHandle();
    }
}