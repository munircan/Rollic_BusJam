namespace _Main.GamePlay.TileSystem
{
    // CAN BE CLICKED MAYBE
    public interface ITileObject 
    {
        public Tile Tile { get; set; }

        public void ClickAction();
    }
}