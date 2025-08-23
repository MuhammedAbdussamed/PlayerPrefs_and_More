using UnityEngine;

public class AlarmState : IState
{
    public void Enter(AI_Controller _aiData)
    {
        Debug.Log("Gel buraya!!!");
    }

    public void Exit(AI_Controller _aiData)
    {

    }

    public void Update(AI_Controller _aiData)
    {
        if (!_aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._patrolState);
        }
        else if (_aiData._isFollowing)
        {
            _aiData.ChangeState(_aiData._followState);
        }
    }

}
