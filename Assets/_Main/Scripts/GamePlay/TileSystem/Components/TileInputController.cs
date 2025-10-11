using _Main.Patterns.ModuleSystem;
using _Main.Scripts.GamePlay.InputSystem;
using UnityEngine;

namespace _Main.GamePlay.TileSystem
{
    public class TileInputController : ComponentModule<Tile>, IPressable
    {
        public void OnPressDown()
        {
            
        }

        public void OnPressUp()
        {
            Debug.Log("I have been Clicked!");
            var tileObject = BaseComp.TileObject;
            if (tileObject != null)
            {
                tileObject.Execute();
            }
        }
    }
}