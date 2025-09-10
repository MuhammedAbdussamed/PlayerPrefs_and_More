using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PatrolState : IState
{
    public void Enter(AI_Controller _aiData) { }

    /*-------------------------------------*/

    public void Exit(AI_Controller _aiData) { }

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
        else if (_aiData._isDeath)
        {
            _aiData.ChangeState(_aiData._deathState);
        }
    }

    void Patrol(AI_Controller _aiData)
    {
        _aiData._botAI.updateRotation = true;                      // Herhangibir şekilde bota rotasyon verme işini bana birak.
        for (int i = 0; i < _aiData._pointBools.Length; i++)        // Bütün pointBools dizisini dön
        {
            if (_aiData._pointBools[i])                                 // Eğer pointBools true ise devam et...
            {
                int nextIndex = (i + 1) % _aiData._pointTransforms.Length;
                _aiData._botAI.SetDestination(_aiData._pointTransforms[nextIndex].position); // Indexe bir ekle ve hedefi o noktaya ayarla. ( bir sonra ki noktaya hareket et. )
            }
        }
    }
}
