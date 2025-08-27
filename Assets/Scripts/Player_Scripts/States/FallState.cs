using System;
using UnityEngine;

public class FallState : IState_Player
{
    public void Enter(PlayerController _playerScript){ Debug.Log("FallState'e girildi"); }

    /*-------------------*/

    public void Exit(PlayerController _playerScript){ Debug.Log("FallState'den çıkıldı"); }

    /*-------------------*/

    public void Update(PlayerController _playerScript)
    {
        Fall(_playerScript);

        if (_playerScript._isGrounded && !_playerScript._isWalking)
        {
            _playerScript.ChangeState(_playerScript._idleState);
        }
        else if (_playerScript._isGrounded && _playerScript._isWalking)
        {
            _playerScript.ChangeState(_playerScript._walkState);
        }
    }

    void Fall(PlayerController _playerScript)
    {
        _playerScript._rb.linearVelocity += Vector3.up * Physics.gravity.y * (_playerScript._fallSpeed - 1) * Time.fixedDeltaTime; // Yukarı yönde gravity uyguluyoruz. Gravity -9.81 olduğu için aslinda aşağı yönde bir kuvvet oluyor.
    }
}
