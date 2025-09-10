using System.Collections;
using UnityEngine;

public class DeathState : IState
{
    public void Enter(AI_Controller _aiData)
    {
        _aiData.StartCoroutine(ChangeTransform(_aiData));
    }

    /*-----------------------*/

    public void Exit(AI_Controller _aiData) { }

    /*-----------------------*/

    public void Update(AI_Controller _aiData)
    {
        ClampPosition(_aiData);

        if (!_aiData._isDeath)
        {
            _aiData.ChangeState(_aiData._patrolState);
        }
    }

    IEnumerator ChangeTransform(AI_Controller _aiData)
    {
        _aiData.gameObject.GetComponent<CapsuleCollider>().enabled = false;      // Botun colliderı kapatılıyor.
        _aiData.gameObject.GetComponent<MeshRenderer>().enabled = false;         // Botun görünürlüğü kapatılıyor.

        _aiData._botNoise.GetComponent<BoxCollider>().enabled = false;          // Botun burnunun colliderı'nı kapat.
        _aiData._botNoise.GetComponent<MeshRenderer>().enabled = false;         // Botun burnunun görünürlüğünü kapat.

        for (int i = 0; i < _aiData._pointBools.Length; i++)
        {
            _aiData._pointBools[i] = false;
        }

        yield return new WaitForSeconds(_aiData._respawnTime);

        _aiData.gameObject.GetComponent<CapsuleCollider>().enabled = true;         // 
        _aiData.gameObject.GetComponent<MeshRenderer>().enabled = true;           //
                                                                                 //     Kapattığımız özellikleri geri açıyoruz.
        _aiData._botNoise.GetComponent<BoxCollider>().enabled = true;            //
        _aiData._botNoise.GetComponent<MeshRenderer>().enabled = true;          //

        _aiData._isDeath = false;
    }

    void ClampPosition(AI_Controller _aiData)
    {
        _aiData.transform.position = _aiData._pointTransforms[0].position;     // Botun pozisyonu patrol state'in ilk noktasina alınıyor.
    }
}
