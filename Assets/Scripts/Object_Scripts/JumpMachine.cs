using UnityEngine;

public class JumpMachine : MonoBehaviour
{
    [Header("Jump Value")]
    [SerializeField] private float _jumpPower;

    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("UnInteractableObject"))
        {
            Rigidbody _rigidbody = col.GetComponent<Rigidbody>();                       // Çarptığın objenin rigidbodysini al
            _rigidbody.AddForce(Vector3.up * _jumpPower * 10f, ForceMode.Impulse);      // Yukarı doğru kuvvet uygula.
        }
    }
}
