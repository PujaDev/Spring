public interface IChangable
{
    void OnStateChanged(GameState newState, GameState oldState);
}
