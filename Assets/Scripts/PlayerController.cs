using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    Rigidbody2D _rb;

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

    void OnMove( InputAction.CallbackContext context ) =>
        _moveVec = context.ReadValue<Vector2>();

    void OnMoveStop( InputAction.CallbackContext context ) =>
        _moveVec = Vector2.zero;

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveVec.normalized * _moveSpeed;
    }

    public void SwitchCharacterSprite( Character c )
    {
        for ( int i = 0; i < transform.childCount; i++ )
            transform.GetChild( i ).gameObject.SetActive( i == (int)c );
    }
}
