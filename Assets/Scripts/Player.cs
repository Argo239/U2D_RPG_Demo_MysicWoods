using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    private Vector3 moveDir;
    private bool isWalking;
    private bool isSpeedingUp = false;
    private float initialVelocity;
    private const string PLAYERLAYER = "Player";

    [SerializeField] private bool consoleMessage = false;

    [Space]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;

    [SerializeField] private float testingPlayerRadius = .25f;
    [SerializeField] private float testingPlayerHeight = .25f;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        initialVelocity = moveSpeed;
        gameInput.OnSpeedUpAction += GameInput_OnSpeedUpAction;
    }

    private void GameInput_OnSpeedUpAction(object sender, EventArgs e) {
        isSpeedingUp = !isSpeedingUp;
        if (isSpeedingUp) {
            moveSpeed *= 2f;
        } else {
            moveSpeed = initialVelocity;
        }
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        moveDir = new Vector2(inputVector.x, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = transform.localScale.x * testingPlayerRadius;
        float playerHeight = transform.localScale.y * testingPlayerHeight;

        int playerLayer = LayerMask.NameToLayer(PLAYERLAYER);
        LayerMask layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(playerLayer));

        bool canMove = !Physics2D.BoxCast(transform.position, new Vector2(playerRadius, playerHeight), 0f, moveDir, moveDistance, layerMask);

        if (!canMove) {
            Vector2 moveDirX = new Vector2(moveDir.x, 0).normalized;
            canMove = (inputVector.x < -.5f || inputVector.x > 0.5f) && !Physics2D.BoxCast(transform.position, new Vector2(playerRadius, playerHeight), 0f, moveDirX, moveDistance, layerMask);
            if (canMove) {
                moveDir = moveDirX;
            } else {
                Vector2 moveDirY = new Vector2(0, moveDir.y).normalized;
                canMove = (inputVector.y < -.5f || inputVector.y > 0.5f) && !Physics2D.BoxCast(transform.position, new Vector2(playerRadius, playerHeight), 0f, moveDirY, moveDistance, layerMask);
                if (canMove) {
                    moveDir = moveDirY;
                }else {
                    Utils.LogMessage(consoleMessage, "Cannot move in ant direction canMove: " + canMove);
                }
            }
        }
        if (canMove) {
            Utils.LogMessage(consoleMessage, "Can move" + " " + moveDir);
            transform.position += moveDir * moveDistance;
        }
        isWalking = moveDir != Vector3.zero;

    }

    public bool IsWalking() {
        return isWalking;
    }

    public Vector3 GetMoveDir() {
        return moveDir;
    }
}