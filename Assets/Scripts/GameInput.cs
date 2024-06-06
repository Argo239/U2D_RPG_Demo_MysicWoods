using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    #region Event

    public event EventHandler OnPlayerMoving;
    public event EventHandler OnPlayerRunning;
    public event EventHandler OnPlayerAttacking;
    public event EventHandler OnPlayerCancelMove;
    public event EventHandler OnPlayerCancelRun;

    #endregion

    private PlayerInputActions _playerInputActions;

    private void Awake() {
        if (Instance != null) Destroy(Instance);
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Move.performed += Move_performed;
        _playerInputActions.Player.Run.performed += Run_performed;
        _playerInputActions.Player.Attack.performed += Attack_performed;
        _playerInputActions.Player.Move.canceled += Move_canceled;
        _playerInputActions.Player.Run.canceled += Run_canceled;
    }

    private void OnDestroy() {
        _playerInputActions.Player.Move.performed -= Move_performed;
        _playerInputActions.Player.Run.performed -= Run_performed;
        _playerInputActions.Player.Attack.performed -= Attack_performed;
        _playerInputActions.Player.Move.canceled -= Move_canceled;
        _playerInputActions.Player.Run.canceled -= Run_canceled;
    }

    private void Move_performed(InputAction.CallbackContext obj) => OnPlayerMoving?.Invoke(this, EventArgs.Empty);
    private void Run_performed(InputAction.CallbackContext obj) => OnPlayerRunning?.Invoke(this, EventArgs.Empty);
    private void Attack_performed(InputAction.CallbackContext obj) => OnPlayerAttacking?.Invoke(this, EventArgs.Empty);
    private void Move_canceled(InputAction.CallbackContext obj) => OnPlayerCancelMove?.Invoke(this, EventArgs.Empty);
    private void Run_canceled(InputAction.CallbackContext obj) => OnPlayerCancelRun?.Invoke(this, EventArgs.Empty);

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public bool CheckRunnningState() => _playerInputActions.Player.Run.ReadValue<float>() > 0;
}