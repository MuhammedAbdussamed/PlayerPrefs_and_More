using System.Collections;
using UnityEngine;

public class AlarmState : IState
{
    public void Enter(AI_Controller _aiData){}

    public void Exit(AI_Controller _aiData){}

    public void Update(AI_Controller _aiData)
    {
        AlarmMode(_aiData);

        if (!_aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._patrolState);
        }
        else if (_aiData._isFollowing)
        {
            _aiData.ChangeState(_aiData._followState);
        }
        else if (_aiData._isDeath)
        {
            _aiData.ChangeState(_aiData._deathState);
        }
    }

    void AlarmMode(AI_Controller _aiData)
    {
        _aiData._transform.LookAt(_aiData._playerScript.transform.position);        // Karakterin pozisyonuna bak.
        _aiData._botAI.SetDestination(_aiData._playerScript.transform.position);    // Karakterin pozisyonunu hedef olarak belirle.
    }

}
