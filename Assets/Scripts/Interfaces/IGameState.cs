using Chapter.Base;
using Chapter.Singleton;

namespace Chapter.State
{
    public interface IGameState
    {
        void Enter(GameManager gameManager) { }
        void Update() { }
        void Exit() { }
    }
}