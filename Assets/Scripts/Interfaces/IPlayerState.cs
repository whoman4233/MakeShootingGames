using Chapter.Base;

namespace Chapter.State
{
    public interface IPlayerState
    {
        void Enter(PlayerBase character) { }
        void Update() { }
        void Exit() { }
    }
}