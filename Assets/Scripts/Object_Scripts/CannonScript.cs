using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [Header("Transform Reference")]
    [SerializeField] private Transform _bullet;
    [SerializeField] private Transform _fireTarget;

    [Header("Variables")]
    [SerializeField] private float _bulletSpeed;
    private Rigidbody _rb;
    private Vector3 _distance;

    void Start()
    {
        _rb = _bullet.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        _distance = (_fireTarget.position - transform.position).normalized; // İki position değerini çıkararak normalized edersek yön buluruz. (Top atar'dan atılacak hedefe doğru yön)

        _rb.linearVelocity = Vector3.zero;                                  // Update içerisinde uygulanan hızın üst üste binmemesi için her frame sıfırlıyoruz.

        _rb.AddForce(_distance * _bulletSpeed, ForceMode.Impulse);          // Mermiye belirlenen doğrultuda ANINDA kuvvet uygula.

    }

}
