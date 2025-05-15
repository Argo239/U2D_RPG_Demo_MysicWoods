using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    #region Event

    public event EventHandler OnPlayerStartMoving;
    public event EventHandler OnPlayerStopMoving;
    public event EventHandler OnPlayerStartRunning;
    public event EventHandler OnPlayerStopRunning;
    public event EventHandler OnPlayerDodgePerformed;
    public event EventHandler OnPlayerAttackPerformed;
    public event EventHandler OnPlayerRevive;

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

        //屏幕設置
        Screen.orientation = ScreenOrientation.LandscapeRight;
        //禁用IME Composition Mode 
        Input.imeCompositionMode = IMECompositionMode.Off;
    }

    private void OnEnable() {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Move.performed += Move_performed;
        _playerInputActions.Player.Move.canceled += Move_canceled;
        _playerInputActions.Player.Run.performed += Run_performed;
        _playerInputActions.Player.Run.canceled += Run_canceled;
        _playerInputActions.Player.Dodge.performed += Dodge_performed;
        _playerInputActions.Player.Attack.performed += Attack_performed;
        _playerInputActions.Player.Revive.performed += Revive_performed;
        PlayerStatus.Instance.OnPlayerDeath += PlayerStatus_OnPlayerDeath;
    }

    private void OnDisable() {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Move.performed -= Move_performed;
        _playerInputActions.Player.Move.canceled -= Move_canceled;
        _playerInputActions.Player.Run.performed -= Run_performed;
        _playerInputActions.Player.Run.canceled -= Run_canceled;
        _playerInputActions.Player.Dodge.performed -= Dodge_performed;
        _playerInputActions.Player.Attack.performed -= Attack_performed;
        _playerInputActions.Player.Revive.performed -= Revive_performed;
        PlayerStatus.Instance.OnPlayerDeath -= PlayerStatus_OnPlayerDeath;
    }

    private void Start() {
        InitializeUISetUp();
    }

    private void Update() {
    }

    private void Move_performed(InputAction.CallbackContext obj) =>
        OnPlayerStartMoving?.Invoke(this, EventArgs.Empty);

    private void Move_canceled(InputAction.CallbackContext obj) =>
        OnPlayerStopMoving?.Invoke(this, EventArgs.Empty);

    private void Run_performed(InputAction.CallbackContext obj) =>
        OnPlayerStartRunning?.Invoke(this, EventArgs.Empty);

    private void Run_canceled(InputAction.CallbackContext obj) =>
        OnPlayerStopRunning?.Invoke(this, EventArgs.Empty);

    private void Dodge_performed(InputAction.CallbackContext obj) {
        OnPlayerDodgePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(InputAction.CallbackContext obj) {
        StunMovement(.5f);
        OnPlayerAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Revive_performed(InputAction.CallbackContext obj) {
        OnPlayerRevive.Invoke(this, EventArgs.Empty);
        _playerInputActions.Player.Enable();
    }

    private void PlayerStatus_OnPlayerDeath(object sender, EventArgs e) {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Revive.Enable();
    }

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

    public bool IsSprintDodgeKeyHeld() {
        return _playerInputActions.Player.Run.ReadValue<float>() > 0;
    }
    
    public void StunMovement(float duration) {
        _playerInputActions.Player.Move.Disable();
        StartCoroutine(StunMovementCoroutine(duration));
    }

    public IEnumerator StunMovementCoroutine(float duration) {
        yield return new WaitForSeconds(duration);
        _playerInputActions.Player.Move.Enable();
    }
}