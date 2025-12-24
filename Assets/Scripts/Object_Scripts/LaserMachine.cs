
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserMachine : MonoBehaviour
{

    [SerializeField] private PlayerController playerScript;

    [Header("Raycast Layers")]
    [Tooltip("Layers that the laser can hit")]
    [SerializeField] private LayerMask _raycastLayers;

    [Header("Turn Values")]
    [SerializeField] private float target1;
    [SerializeField] private float target2;
    [SerializeField] private float _turnSpeed;
    private float _targetY;

    [Header("Move Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private bool canMove;
    private bool _in1;                                                          // Lazer makinesinin point1 noktasinda olup olmadiğini kontrol eden bool.

    [Header("Turn Bool")]
    private bool _isFacingRight;
    private bool _isTurning;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, target1, 0f));
        if (moveSpeed > 0)
        {
            transform.position = point1.position;
        }
    }

    void Update()
    {
        /* Turn Functions */
        LaserFunction();

        if (!_isTurning)
        {
            StartCoroutine(Turn());
        }

        /* Move Functions */
        CheckPosition();
        Move();

        if(moveSpeed > 0){ canMove = true; }
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
                playerScript._isDeath = true;                                                                       // Karakteri öldür
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
        if (Mathf.Abs(Mathf.DeltaAngle(y, target1)) < 0.1f)  // DeltaAngle fonksiyonu açıları karşilaştirmak için idealdir. Sonrasinda mutlak değer için Abs fonksiyonunu kullaniriz.
        {
            _isFacingRight = true;
        }
        else if (Mathf.Abs(Mathf.DeltaAngle(y, target2)) < 0.1f)
        {
            _isFacingRight = false;
        }

        _targetY = _isFacingRight ? target2 : target1;
    }

    /*--------------------------------------------------------------------------*/

    void Move()
    {
        if (canMove)
        {
            if (_in1)   // Değişken true ise bu lazer makinesinin point1 noktasinda olduğunu gösterir.
            {
                transform.position = Vector3.MoveTowards(transform.position, point2.position, moveSpeed * 0.01f);   // Bu yüzden point2 noktasina ilerle
            }
            else        // Değişken false ise bu sefer point2 noktasindadir.
            {
                transform.position = Vector3.MoveTowards(transform.position, point1.position, moveSpeed * 0.01f);   // O zaman point1 noktasina ilerle.
            }
        }
    }

    /*--------------------------------------------------------------------------*/

    void CheckPosition()
    {
        if (Vector3.Distance(transform.position, point1.position) < 0.1f)               // Point1'ye 0.1f'den yakinsa...
        {
            _in1 = true;                                                                // Değişkeni true çevir.
        }

        else if (Vector3.Distance(transform.position, point2.position) < 0.1f)          // Point2'ye 0.1f'den yakinsa...
        {
            _in1 = false;                                                               // Değişkeni false çevir.
        }
    }

    #endregion

}
