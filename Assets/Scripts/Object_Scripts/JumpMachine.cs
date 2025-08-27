using UnityEngine;

public class JumpMachine : MonoBehaviour
{
    [Header("Jump Value")]
    [SerializeField] private float _jumpPower;

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Rigidbody _playerRigidbody = col.collider.GetComponent<Rigidbody>();
            _playerRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }
}
