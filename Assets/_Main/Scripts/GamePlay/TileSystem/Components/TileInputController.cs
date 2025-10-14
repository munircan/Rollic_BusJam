using _Main.Scripts.GamePlay.InputSystem.Interactables;
using _Main.Scripts.GamePlay.PersonSystem.Components;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SaveSystem;
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

            ExecuteWithObjectManager(true);
        }

        public void ExecuteWithObjectManager(bool save)
        {
            var isInstant = !save;
            if (BaseComp.TileObject.Tile.Data.Type == TileType.Person)
            {
                ServiceLocator.GetService<PersonManager>().MovePersonInPath((Person)BaseComp.TileObject,save,isInstant).Forget();
            }
        }
    }
}