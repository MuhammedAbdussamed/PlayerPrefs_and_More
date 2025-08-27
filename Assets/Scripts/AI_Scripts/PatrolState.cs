using UnityEngine;

public class PatrolState : IState
{
    public void Enter(AI_Controller _aiData){}
    
    /*-------------------------------------*/

    public void Exit(AI_Controller _aiData){}

    /*-------------------------------------*/

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
        for (int i = 0; i < _aiData._pointBools.Length; i++)        // Bütün pointBools dizisini dön
        {
            if (_aiData._pointBools[i])                                 // Eğer pointBools true ise devam et...
            {
                if (i == 3)                                                 // True olan pointBools indexi 3 ise devam et...
                {
                    _aiData._botAI.SetDestination(_aiData._pointTransforms[0].position);                        // Botun hedefini point0'a ayarla.
                    _aiData._transform.LookAt(_aiData._pointTransforms[0].position + _aiData._lookAtDelay);     // Botun baktığı yönü point0'a ayarla.
                }
                else                                                        // True olan pointBools indexi 3 değilse devam et...
                {
                    _aiData._botAI.SetDestination(_aiData._pointTransforms[i + 1].position);                    // Indexe bir ekle ve hedefi o noktaya ayarla. ( bir sonra ki noktaya hareket et. )
                    _aiData._transform.LookAt(_aiData._pointTransforms[i + 1].position + _aiData._lookAtDelay); // Bir sonra ki hareket edeceğin noktaya doğru bak.
                }
            }
        }
    }                                                   
}
