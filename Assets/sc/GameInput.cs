using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputs : MonoBehaviour {

    private const string IS_MOVING = "is_Moving";

    public static GameInput instance { get; private set; }

    public enum KeyBinding {
        Move_Up, Move_Down, Move_Left, Move_Right,
        Interact,
        Attack,
    }

    
}
