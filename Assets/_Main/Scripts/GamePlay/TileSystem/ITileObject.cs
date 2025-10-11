namespace _Main.GamePlay.TileSystem
{
    
    public interface ITileObject 
    {
        public Tile Tile { get; set; }

        public void Execute();
    }
}