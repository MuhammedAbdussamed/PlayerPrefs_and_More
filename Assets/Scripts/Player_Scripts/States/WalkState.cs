using UnityEngine;

public class WalkState : IState_Player
{
    public void Enter(PlayerController _playerData){}
    
    /*---------------------------------------------*/

    public void Exit(PlayerController _playerData){}

    /*---------------------------------------------*/

    public void Update(PlayerController _playerData)
    {
        Run(_playerData);

        if (!_playerData._isWalking && _playerData._isGrounded) // Eğer karakter yürümüyorsa ve yerdeyse devam et...
        {
            _playerData.ChangeState(_playerData._idleState);    // IdleState'e geç
        }
        else if (_playerData._isFalling)                        // Eğer karakter düşüyorsa
        {
            _playerData.ChangeState(_playerData._fallState);    // Düşme state'ine geç
        }
    }

    void Run(PlayerController _playerData)
    {
        Vector3 _moveInput = new Vector3(_playerData._moveDirection.x, 0f, _playerData._moveDirection.y) * _playerData._speed;  // _moveInput değişkenine input girdilerini ata ve speed değişkeni ile çarp.
        Vector3 move = _playerData.transform.TransformDirection(_moveInput);                                                    // Hareketin dünyaya göre değil de local pozisyona göre yapılmasını sağlar.
        move.y = _playerData._rb.linearVelocity.y;                                                                              // y edğerini değiştirmiyoruz çünkü sadece yürüyecek.
        _playerData._rb.linearVelocity = move;                                                                                  
    }
}
