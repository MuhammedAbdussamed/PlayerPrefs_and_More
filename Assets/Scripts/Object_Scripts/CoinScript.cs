using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [Header("Coin Properties")]
    [SerializeField] private float _turnSpeed;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _coinSound;

    [Header("ScriptReferences")]
    private PlayerController _playerData;

    [Header("Variables")]
    private bool _isTurning;

    void Start()
    {
        _playerData = PlayerController.Instance;
    }

    void OnTriggerEnter(Collider col)
    {
        StartCoroutine(CoinFunction(col));
    }

    void Update()
    {                                                                            // Bu kod = (0f , _turnSpeed *Time.deltaTime, 0f) ve dünya ekseninde döndür. 
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime, Space.World); // Vector3.up yönünde dünyaya göre döndür.
    }                                                                            // Spce.Self yazilsaydi objenin kendi etrainda döndürürdü.

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
