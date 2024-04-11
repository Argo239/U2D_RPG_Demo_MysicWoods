using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

public static Testing Instance { get; private set; }

    private const string PLAYERLAYER = "Player";
    private bool isWalking;

    [SerializeField] private bool consoleLog;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private GameInput gameInput;


    private void Awake() {
        Instance = this;
    }

    private void Start() {

    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector2(inputVector.x, inputVector.y);
    }
}