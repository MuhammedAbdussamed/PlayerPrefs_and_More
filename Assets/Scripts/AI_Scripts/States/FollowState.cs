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
        _aiData._botAI.updateRotation = false;                      // Herhangibir şekilde bota rotasyon verme işini bana birak.
        _aiData._botAI.SetDestination(_aiData._playerScript.transform.position);    // Karakterin pozisyonunu hedef olarak ayarla.
        _aiData.transform.LookAt(_aiData._playerScript.transform.position);
    }

    void PlayerIsMissing(AI_Controller _aiData)
    {
        if (_aiData._playerScript._isInvisible)
        {
            _aiData._isFollowing = false;
        }
    }
    
}
