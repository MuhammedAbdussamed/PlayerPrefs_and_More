using System.Collections;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinSound;

    [Header("ScriptReferences")]
    [SerializeField] private PlayerController _playerData;
    
    void OnTriggerEnter(Collider col)
    {
        StartCoroutine(CoinFunction(col));
    }

    IEnumerator CoinFunction(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _playerData._coin++;
            _audioSource.PlayOneShot(_coinSound);
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
    }
}
