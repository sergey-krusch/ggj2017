using UnityEngine;

public class StateReporter : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var listeners = animator.GetComponents<IStateReporterListener>();
        foreach (var listener in listeners)
            listener.Enter(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var listeners = animator.GetComponents<IStateReporterListener>();
        foreach (var listener in listeners)
            listener.Exit(stateInfo.shortNameHash);
    }
}