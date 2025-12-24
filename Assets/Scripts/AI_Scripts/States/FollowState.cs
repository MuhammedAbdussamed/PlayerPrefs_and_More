using UnityEngine;

public class FollowState : IState
{
    public void Enter(AI_Controller _aiData) { }

    public void Exit(AI_Controller _aiData) { }

    public void Update(AI_Controller _aiData)
    {
        Follow(_aiData);
        PlayerIsMissing(_aiData);

        if (!_aiData._isFollowing && !_aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._patrolState);
        }
        else if (!_aiData._isFollowing && _aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._alarmState);
        }
        else if (_aiData._isDeath)
        {
            _aiData.ChangeState(_aiData._deathState);
        }
    }

    void Follow(AI_Controller _aiData)
    {
        Vector3 target = _aiData._playerScript.transform.position;  // Karakterin pozisyonunu hedef olarak ayarla.
        target.y = _aiData.transform.position.y;                    // Y ekseninde hedef atamasi yapma.

        _aiData._botAI.SetDestination(target);                      // Hedefe git
        _aiData.transform.LookAt(target);                           // Hedefe dön
    }

    void PlayerIsMissing(AI_Controller _aiData)
    {
        if (_aiData._playerScript._isInvisible)
        {
            _aiData._isFollowing = false;
        }
    }
    
}
