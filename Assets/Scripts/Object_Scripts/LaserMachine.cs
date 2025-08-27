
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserMachine : MonoBehaviour
{
    [Header("Raycast Layers")]
    [SerializeField] private LayerMask _raycastLayers;

    [Header("Turn Values")]
    [SerializeField] private float _turnAngle;
    [SerializeField] private float _turnSpeed;
    private float _targetY;

    [Header("Turn Bool")]
    private bool _isFacingRight;
    private bool _isTurning;

    void Update()
    {
        LaserFunction();

        if (!_isTurning)
        {
            StartCoroutine(Turn());
        }
    }

    #region Function

    void LaserFunction()
    {
        Vector3 _direction = transform.TransformDirection(Vector3.left);

        if (Physics.Raycast(transform.position, _direction, out RaycastHit _hit, Mathf.Infinity, _raycastLayers)) // Mevcut pozisyondan sola doğru ışın at. Bilgileri _hit değişkenine yaz. RaycastLayerstan bir layera çarparsa devam et.
        {
            Debug.DrawRay(transform.position, _direction * 10f, Color.green);                                         // Işını yeşil renkte görselleştir. 

            if (_hit.collider.CompareTag("Player"))                                                                     // Eğer çarpılan objenin tag'i Player ise devam et...
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);                                           // Sahneyi tekrar yükle.
            }
        }
        else                                                                                                        // Eğer ışın raycastLayers'tan herhangi bir katmana çarpmassa devam et...
        {
            Debug.DrawRay(transform.position, _direction * 10f, Color.red);                                               // Işını kırmızı renkte görselleştir.
        }
    }

    /*-------------------------------------------------------------------------*/

    IEnumerator Turn()
    {
        _isTurning = true;

        CheckFace();

        while (transform.rotation != Quaternion.Euler(0f, _targetY, 0f))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, _targetY, 0f), _turnSpeed * 0.1f);
            yield return null; // Her frame güncelle.
        }

        yield return new WaitForSeconds(2f);

        _isTurning = false;
    }

    /*-------------------------------------------------------------------------*/

    void CheckFace()
    {
        if (transform.rotation == Quaternion.Euler(0f, _turnAngle, 0f))
        {
            _isFacingRight = true;
        }
        else if (transform.rotation == Quaternion.Euler(0f, -_turnAngle, 0f))
        {
            _isFacingRight = false;
        }

        _targetY = _isFacingRight ? -_turnAngle : _turnAngle;
    }    
    
    #endregion

}
