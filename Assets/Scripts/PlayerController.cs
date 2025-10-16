using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    Rigidbody2D _rb;
    Animator _anim;

    InputSystem_Actions _inputs;
    Vector2 _moveVec;

    public static PlayerController Instance { get; private set; }
    void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        Instance = this;

        _inputs = new();
        _inputs.Enable();

        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.Move.canceled += OnMoveStop;
    }

    void Start()
    {
        // game started not from Character Select Screen
        if ( Game.Instance == null ) {
            SwitchCharacterSprite( Character.FEMALE );
        }
    }

    void OnMove( InputAction.CallbackContext context ) =>
        _moveVec = context.ReadValue<Vector2>();

    void OnMoveStop( InputAction.CallbackContext context ) =>
        _moveVec = Vector2.zero;

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveVec.normalized * _moveSpeed;
    }

    void Update()
    {
        if ( _moveVec.normalized.magnitude != 0 ) {
            _anim.SetFloat( "X", _moveVec.normalized.x );
            _anim.SetFloat( "Y", _moveVec.normalized.y );
        }
        _anim.SetFloat( "Speed", _rb.linearVelocity.magnitude );
    }

    public void SwitchCharacterSprite( Character c )
    {
        for ( int i = 0; i < transform.childCount; i++ ) {
            var isChar =  i == (int)c;
            var child = transform.GetChild( i ).gameObject;
            child.SetActive( isChar );

            if ( isChar ) {
                _anim = child.GetComponent<Animator>();
            }
        }
    }
}
