using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }
    
    #region Component attributes
    
    [Header("Testing attribute")]
    [SerializeField] private bool consoleLogOn;

    [Header("Base attribute")]
    [SerializeField] private GameInput gameInput;
    
    #endregion
    
    #region Player attribute
    
    public enum State { Idling, Moving, Attacking }
    private Dictionary<State, IAction> _playerActions;

    private Rigidbody2D _rigidbody2D;
    
    private State _currentState;
    #endregion
    
    
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if(Instance != null) LogMessage(consoleLogOn, "There is more than one Player instance");
        if(_rigidbody2D == null) return;
        
        Instance = this;
        _currentState = State.Idling;
        _playerActions = new Dictionary<State, IAction> {
            { State.Moving, gameObject.AddComponent<PlayerMove>() },
            { State.Attacking, gameObject.AddComponent<PlayerAttack>() }
        };
    }

    private void Start() {
        gameInput.OnPlayerCancelMove += GameInput_OnPlayerIdling;
        gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        gameInput.OnPlayerAttacking += GameInput_OnPlayerAttack;
    }

    private void OnDestroy() {
        gameInput.OnPlayerCancelMove -= GameInput_OnPlayerIdling;
        gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        gameInput.OnPlayerAttacking -= GameInput_OnPlayerAttack; 
    }

    private void Update() {
        if (_playerActions.ContainsKey(_currentState)) {
            _playerActions[_currentState].Execute(); 
        }
    }
    
    private void GameInput_OnPlayerIdling(object sender, EventArgs e) => SetState(State.Idling);

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) {
        if(_currentState != State.Attacking) SetState(State.Moving);
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e) {
        SetState(State.Attacking);
        PlayerMove.Instance.Stop();
    }

    private void SetState(State newState) {
        if (_currentState == newState) return; 
        if (_playerActions.ContainsKey(_currentState)) 
            _playerActions[_currentState].Stop();
        _currentState = newState;
    }

    public void SetStateIdle() => SetState(State.Idling);
    public State GetCurrentState() => _currentState;
    public GameInput GetGameInput() => gameInput;
}