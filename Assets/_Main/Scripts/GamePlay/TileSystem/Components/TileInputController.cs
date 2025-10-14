using _Main.Scripts.GamePlay.InputSystem.Interactables;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.TileSystem.Data;
using _Main.Scripts.Patterns.ModuleSystem;
using _Main.Scripts.Patterns.ServiceLocation;
using Cysharp.Threading.Tasks;

namespace _Main.Scripts.GamePlay.TileSystem.Components
{
    public class TileInputController : ComponentModule<Tile>, IPressable
    {
        public void OnPressDown()
        {
        }

        public void OnPressUp()
        {
            var tileObject = BaseComp.TileObject;
            
            if (tileObject == null)
            {
                return;
            }
            
            if (tileObject.Tile.Data.Type == TileType.Person)
            {
                ServiceLocator.GetService<PersonManager>().MovePersonInPath((Person)tileObject).Forget();
            }
        }
    }
}