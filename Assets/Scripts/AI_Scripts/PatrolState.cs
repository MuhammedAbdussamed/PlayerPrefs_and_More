using UnityEngine;

public class PatrolState : IState
{
    public void Enter(AI_Controller _aiData)
    {
        Debug.Log("En iyisi nöbetime döneyim");
    }

    public void Exit(AI_Controller _aiData)
    {

    }

    public void Update(AI_Controller _aiData)
    {
        Patrol(_aiData);

        if (_aiData._alarmMode)
        {
            _aiData.ChangeState(_aiData._alarmState);
        }
        else if (_aiData._isFollowing)
        {
            _aiData.ChangeState(_aiData._followState);
        }
    }

    void Patrol(AI_Controller _aiData)
    {
        for (int i = 0; i < _aiData._pointBools.Length; i++)
        {
            if (_aiData._pointBools[i])
            {
                if (i == 3)
                {
                    _aiData._botAI.SetDestination(_aiData._pointTransforms[0].position);
                    _aiData._transform.LookAt(_aiData._pointTransforms[0].position + _aiData._lookAtDelay);
                }
                else
                {
                    _aiData._botAI.SetDestination(_aiData._pointTransforms[i + 1].position);
                    _aiData._transform.LookAt(_aiData._pointTransforms[i + 1].position + _aiData._lookAtDelay);
                }
            }
        }
    }                                                   
}
