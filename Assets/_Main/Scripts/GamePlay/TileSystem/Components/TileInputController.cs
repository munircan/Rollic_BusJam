using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.InputSystem;

namespace _Main.GamePlay.TileSystem
{
    public class TileInputController : ComponentModule<Tile>, IPressable
    {
        public void OnPressDown()
        {
            
        }

        public void OnPressUp()
        {
            var tileObject = BaseComp.TileObject;
            if (tileObject != null)
            {
                tileObject.Execute();
            }
        }
    }
}