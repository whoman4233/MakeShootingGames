namespace Chapter.State
{
    public interface IPlainState
    {
        void Handle(PlainController controller);
    }
}