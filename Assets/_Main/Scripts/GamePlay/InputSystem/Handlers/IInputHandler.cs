using _Main.Scripts.GamePlay.InputSystem.Interactables;

namespace _Main.Scripts.GamePlay.InputSystem.Handlers
{
    public interface IInputHandler
    {
        public bool IsHandling { get; protected set; }

        public void Cast();

        public void StartHandle(IInteractable interactable);
        
        public void StopHandle();
    }
}