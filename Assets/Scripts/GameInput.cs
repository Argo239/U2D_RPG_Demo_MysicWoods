using System;
using UnityEngine;
using UnityEngine.InputSystem;

using static Argo_Utils.Utils;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    #region Event

    public event EventHandler OnPlayerMoving;
    public event EventHandler OnPlayerMoveCanceled;

    #endregion

    private PlayerInputActions _playerInputActions;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); 
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player.Move.performed += Move_performed;
        _playerInputActions.Player.Move.canceled += Move_canceled;

        _playerInputActions.Player.Enable();

        //手机设置画面默认向左横屏
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    private void Start() {
        InitializeUISetUp();
    }

    private void OnDestroy() {
        _playerInputActions.Player.Move.performed -= Move_performed;
        _playerInputActions.Player.Move.canceled -= Move_canceled;

        _playerInputActions.Player.Disable();
    }

    private void Move_performed(InputAction.CallbackContext obj) => OnPlayerMoving?.Invoke(this, EventArgs.Empty);
    private void Move_canceled(InputAction.CallbackContext obj) => OnPlayerMoveCanceled?.Invoke(this, EventArgs.Empty);

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public void InitializeUISetUp() {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}