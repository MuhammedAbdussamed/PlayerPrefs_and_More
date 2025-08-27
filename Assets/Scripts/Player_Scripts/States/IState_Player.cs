using UnityEngine;

public interface IState_Player
{
    void Enter(PlayerController _playerData);
    void Exit(PlayerController _playerData);
    void Update(PlayerController _playerData);
}
