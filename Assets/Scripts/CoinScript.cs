using System.Collections;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private PlayerController _playerData;

    void OnTriggerEnter(Collider col)
    {
        CoinFunction(col);
    }

    public void CoinFunction(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _playerData._coin++;
            Destroy(gameObject);
        }
    }
}
