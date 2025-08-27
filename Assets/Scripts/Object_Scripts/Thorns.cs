using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorns : MonoBehaviour
{
    [Header("Point Transforms")]
    [SerializeField] private Transform[] _points = new Transform[2];

    [Header("Move Bools")]
    private bool _isMoving;

    [Header("Variable")]
    [SerializeField] private float _speed;
    private int _targetPointIndex;
    
    void Update()
    {
        CheckPosition();

        if (!_isMoving)
        {
            StartCoroutine(MoveThorns());
        }
    }

    void OnCollisionEnter(Collision col)
    {
        col.gameObject.SetActive(false);

        if (col.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    #region Functions

    IEnumerator MoveThorns()
    {
        _isMoving = true;

        while (Vector3.Distance(transform.position, _points[_targetPointIndex].position) > 0.2f)   // Hedef noktaya olan uzakliğin 0.2f'den büyükse devam et...
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_targetPointIndex].position, _speed * Time.deltaTime);    // Pozisyonu hedef pozisyonuna eşitle.
            yield return null;  // Her frame güncelle.
        }

        _isMoving = false;                      // _isMoving false çevirelim ki döngü tekrar başlasın.
    }

    void CheckPosition()
    {
        if (Vector3.Distance(transform.position, _points[0].position) <= 0.2f)        // Eğer pozisyon 1. point'in pozisyonuna neredeyse eşitse...
        {
            _targetPointIndex = 1;    // Hedef pointIndexini 1 olarak ayarlar.
        }

        else if (Vector3.Distance(transform.position, _points[1].position) <= 0.2f)   // Eğer pozisyon 2. point'in pozisonuna neredeyse eşitse...
        {
            _targetPointIndex = 0;    // Hedef pointIndexini 0 olarak ayarla.
        }
    }
    
    #endregion
}
