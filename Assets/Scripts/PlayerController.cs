using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerProperties")]
    [SerializeField] private float _speed;
    public float _coin;

    [Header("References")]
    [SerializeField] private InputActionAsset _inputs;

    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    [Header("Variables")]
    private Vector2 _moveDirection;

    private void Start()
    {
        _inputs.Enable();
    }

    private void Update()
    {
        _moveDirection = _inputs.FindActionMap("Movement").FindAction("Move").ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        Vector3 _moveInput = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * _speed;
        Vector3 move = transform.TransformDirection(_moveInput) * _speed;
        move.y = _rb.linearVelocity.y;
        _rb.linearVelocity = move;
    }
}
