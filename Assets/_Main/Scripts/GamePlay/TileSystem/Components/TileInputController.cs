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
        private PersonManager _personManager;

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

            ExecuteWithObjectManager(true).Forget();
        }

        public async UniTask ExecuteWithObjectManager(bool save)
        {
            if (BaseComp.TileObject.Tile.Data.Type == TileType.Person)
            {
                if (_personManager == null)
                {
                    _personManager = ServiceLocator.GetService<PersonManager>();
                }

                await _personManager.MovePersonInPath((Person)BaseComp.TileObject, save);
            }
        }
    }
}