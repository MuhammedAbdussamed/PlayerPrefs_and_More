using UnityEngine;

public interface IState_Player
{
    void Enter(PlayerController _playerScript);
    void Exit(PlayerController _playerScript);
    void Update(PlayerController _playerScript);
}
