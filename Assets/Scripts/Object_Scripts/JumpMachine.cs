using UnityEngine;

public class JumpMachine : MonoBehaviour
{
    [Header("Jump Value")]
    [SerializeField] private float _jumpPower;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Rigidbody _playerRigidbody = col.GetComponent<Rigidbody>();
            _playerRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }
}
