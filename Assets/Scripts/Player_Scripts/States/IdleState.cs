using UnityEngine;

public class IdleState : IState_Player
{
    public void Enter(PlayerController _playerData){}
    
    /*---------------------------------------------*/

    public void Exit(PlayerController _playerData){}

    /*---------------------------------------------*/

    public void Update(PlayerController _playerData)
    {
        if (_playerData._isWalking)                             // Karakter yürüyorsa 
        {
            _playerData.ChangeState(_playerData._walkState);        // Yürüme State'ine geç.
        }
        else if (_playerData._isFalling)                        // Karakter düşüyorsa
        {
            _playerData.ChangeState(_playerData._fallState);        // Düşme state'ine geç
        }
    }  
}
