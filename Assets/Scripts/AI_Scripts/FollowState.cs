using UnityEngine;

public class FollowState : IState
{
    public void Enter(AI_Controller _aiData){}

    public void Exit(AI_Controller _aiData){}

    public void Update(AI_Controller _aiData)
    {
        Follow(_aiData);
        
        if (!_aiData._isFollowing && !_aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._patrolState);
        }
        else if (!_aiData._isFollowing && _aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._alarmState);
        }
    }

    void Follow(AI_Controller _aiData)
    {
        _aiData._botAI.SetDestination(_aiData._playerScript.transform.position);    // Karakterin pozisyonunu hedef olarak ayarla.
    }
    
}
