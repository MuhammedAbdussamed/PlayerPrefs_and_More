using UnityEngine;

public class WalkState : IState_Player
{
    public void Enter(PlayerController _playerScript){}
    
    /*---------------------------------------------*/

    public void Exit(PlayerController _playerScript){}

    /*---------------------------------------------*/

    public void Update(PlayerController _playerScript)
    {
        Run(_playerScript);

        if (!_playerScript._isWalking && _playerScript._isGrounded) // Eğer karakter yürümüyorsa ve yerdeyse devam et...
        {
            _playerScript.ChangeState(_playerScript._idleState);    // IdleState'e geç
        }
        else if (_playerScript._isFalling)                        // Eğer karakter düşüyorsa
        {
            _playerScript.ChangeState(_playerScript._fallState);    // Düşme state'ine geç
        }
    }

    void Run(PlayerController _playerScript)
    {
        Vector3 _moveInput = new Vector3(_playerScript._moveDirection.x, 0f, _playerScript._moveDirection.y) * _playerScript._speed;  // _moveInput değişkenine input girdilerini ata ve speed değişkeni ile çarp.
        Vector3 move = _playerScript.transform.TransformDirection(_moveInput);                                                        // Hareketin dünyaya göre değil de local pozisyona göre yapılmasını sağlar.
        move.y = _playerScript._rb.linearVelocity.y;                                                                                  // y değerini değiştirmiyoruz çünkü sadece yürüyecek.
        _playerScript._rb.linearVelocity = move;                                                                                  
    }
}
