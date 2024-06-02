using Interface;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAction{
    public static PlayerAttack Instance { get; private set; }
    public void Execute() {
        //Coding~
        PlayerController.Instance.SetStateIdle();
    }

    public void Stop() {
        //Coding~ 
    }
}