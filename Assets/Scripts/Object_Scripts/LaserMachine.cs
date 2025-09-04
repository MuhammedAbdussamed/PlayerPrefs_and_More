
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserMachine : MonoBehaviour
{
    [Header("Raycast Layers")]
    [Tooltip("Layers that the laser can hit")]
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
        Vector3 _direction = transform.TransformDirection(Vector3.left);    // Sol yönünü belirtip direction değişkenine atadik.

        if (Physics.Raycast(transform.position, _direction, out RaycastHit _hit, Mathf.Infinity, _raycastLayers)) // Mevcut pozisyondan sola doğru ışın at. Bilgileri _hit değişkenine yaz. RaycastLayerstan bir layera çarparsa devam et.
        {
            Debug.DrawRay(transform.position, _direction * 10f, Color.green);                                         // Işını yeşil renkte görselleştir. 

            if (_hit.collider.CompareTag("Player") || _hit.collider.CompareTag("Invisible"))                        // Eğer çarpılan objenin tag'i Player ise devam et...
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

        while (transform.rotation != Quaternion.Euler(0f, _targetY, 0f))    // Eğer rotasyon açısı hedef açıya eşit değilse... 
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, _targetY, 0f), _turnSpeed * 0.1f);   // Rotasyon açısını yumuşak bir şekilde hedef açıya çevir.
            yield return null; // Her frame güncelle.
        }

        yield return new WaitForSeconds(2f);  

        _isTurning = false;
    }

    /*-------------------------------------------------------------------------*/

    void CheckFace()
    {
        float y = transform.eulerAngles.y;                              // Y ekseninde dönnüş yapacağimiz için y açisini aliyoruz.

        /* İstenilen dönüş açısı ile şimdi ki açı arasinda ki fark 0.1f den az olursa sağa bakiyor değişkenini true çevir*/
        if (Mathf.Abs(Mathf.DeltaAngle(y, _turnAngle)) < 0.1f)          // DeltaAngle fonksiyonu açıları karşilaştirmak için idealdir. Sonrasinda mutlak değer için Abs fonksiyonunu kullaniriz.
        {
            _isFacingRight = true;
        }
        else if (Mathf.Abs(Mathf.DeltaAngle(y, -_turnAngle)) < 0.1f)
        {
            _isFacingRight = false;
        }

        _targetY = _isFacingRight ? -_turnAngle : _turnAngle;
    }    
    
    #endregion

}
