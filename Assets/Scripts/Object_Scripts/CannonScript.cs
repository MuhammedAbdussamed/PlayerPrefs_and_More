using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [Header("Transform Reference")]
    [SerializeField] private Transform _bullet;
    [SerializeField] private Transform _fireTarget;

    [Header("Variables")]
    [SerializeField] private float _bulletSpeed;
    private Vector3 _distance;
    private float _posY;

    void Start()
    {
        _posY = _bullet.position.y;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        _distance = (_fireTarget.position - transform.position).normalized; // İki position değerini çıkararak normalized edersek yön buluruz. (Top atar'dan atılacak hedefe doğru yön)

        _bullet.position += new Vector3(_distance.x, 0f, _distance.z) * _bulletSpeed * Time.deltaTime; // Merminin pozisyonunu güncelle.

        Vector3 newPosition = _bullet.position;
        newPosition.y = _posY;
        _bullet.position = newPosition;
    }

}
