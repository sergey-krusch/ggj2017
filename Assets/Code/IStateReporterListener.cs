public interface IStateReporterListener
{
    void Enter(int nameHash);
    void Exit(int nameHash);
}