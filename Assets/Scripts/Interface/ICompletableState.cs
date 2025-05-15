using Interface;
using UnityEngine;

public interface ICompletableState : IPlayerState{
    bool IsComplete(ControllDirection cardinalDir, Vector2 facingDir);
}
