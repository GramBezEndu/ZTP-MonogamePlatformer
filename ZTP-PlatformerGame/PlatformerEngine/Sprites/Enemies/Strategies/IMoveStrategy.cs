namespace PlatformerEngine.Sprites.Enemies
{
    public interface IMoveStrategy : IComponent
    {
        void Move(Enemy enemy);
    }
}
