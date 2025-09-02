using UnityEngine;

public class IdleState : IState_Player
{
    public void Enter(PlayerController _playerScript){}
    
    /*---------------------------------------------*/

    public void Exit(PlayerController _playerScript){}

    /*---------------------------------------------*/

    public void Update(PlayerController _playerScript)
    {
        if (_playerScript._isWalking)                             // Karakter yürüyorsa 
        {
            _playerScript.ChangeState(_playerScript._walkState);        // Yürüme State'ine geç.
        }
        else if (_playerScript._isFalling)                        // Karakter düşüyorsa
        {
            _playerScript.ChangeState(_playerScript._fallState);        // Düşme state'ine geç
        }
    }  
}
