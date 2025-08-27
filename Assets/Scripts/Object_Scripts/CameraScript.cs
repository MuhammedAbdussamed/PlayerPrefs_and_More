using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{
    [Header("Turn Angle")]
    [SerializeField] private float _turnAngle;
    private float _targetY;

    [Header("Bools")]
    private bool _isFacingRight;
    private bool _isTurning;

    [Tooltip("Raycast'in çarpabileceği katmanlar")]
    public LayerMask _raycastLayers;

    [Header("References")]
    private AI_Controller[] _moreBotAI;         // AI_Controller Script dizisi. 
    public Transform _playerTransform;

    [Header("UnityEvents")]
    public UnityEvent OnPlayerDetected;         // Bu tarz eventler inspectordan tetiklenebilir.

    private void Start()
    {
        _moreBotAI = FindObjectsByType<AI_Controller>(FindObjectsSortMode.None);    // Sahnede ki bütün AI_Controller scriptlerini topla . Siralama modu da kapalı olsun böyle daha hizli.
    }

    public void Update()
    {
        if (!_isTurning)                        // Kamera dönmüyorsa devam et...
        {
            StartCoroutine(Turn());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        PlayerDetect(col, true);
    }

    void OnTriggerExit(Collider col)
    {
        PlayerDetect(col, true);
    }

    #region Functions

    IEnumerator Turn()
    {
        CheckTurnDirection();
        _isTurning = true;

        /*Şimdi ki ve hedef açi arasinda ki fark 0.1 den küçük olana dek bu döngüyü devam ettir*/
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, _targetY)) > 0.1f)    // Mathf.DeltaAngle iki açi arasinda ki en kisa farki verir. Açilarla çaliştiğimiz için mutlak değer almak istiyoruz bu yüzden mathf.abs kullaniriz.
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(20f, _targetY, 0f), 20f * Time.deltaTime);    // RotateTowards ile yavaş yavaş döndürürüz. İki tane açı ve bir tane hız parametresi alır.
            yield return null; // her frame güncelle
        }

        yield return new WaitForSeconds(2f);    // Döndüğü yerde 2 saniye bekle
        _isTurning = false;                     // Dönüyor'u false çevir ki döngü tekrarlansın.
    }

    /*---------------------------------------------------------*/

    void CheckTurnDirection()
    {
        if (transform.rotation == Quaternion.Euler(20f, _turnAngle, 0f))        // Eğer kamera açısı istenen açıya gelirse devam et...
        {
            _isFacingRight = true;                                              // Kamera sağa bakiyor değişkenini true çevir.
        }
        else if (transform.rotation == Quaternion.Euler(20f, -_turnAngle, 0f))  // Eğer kamera açısı istenen açıya gelirse devam et...
        {
            _isFacingRight = false;                                             // Kamera sağa bakiyor değişkenini false çevir. ( Sola bakıyor ).
        }   
        _targetY = _isFacingRight ? -_turnAngle : _turnAngle;                   // Kameranın sağa ya da sola bakmasına göre hedef açıyı belirle.
    }

    /*---------------------------------------------------------*/

    void PlayerDetect(Collider col, bool enterExit)
    {
        if (col.CompareTag("Player"))                                                                   // Player tag'li bir obje collidera girerse devam et...
        {
            Vector3 _cameraDirection = (_playerTransform.position - transform.position).normalized;     // Cameradan karaktere doğru bir vector3 çiz.
            RaycastHit _hit;

            if (Physics.Raycast(transform.position, _cameraDirection, out _hit, 20f , _raycastLayers))  // Kamera pozisonundan çizdiğimiz vector3 doğrultusunda bir raycast at. Eğer çarparsa devam et...
            {
                Debug.DrawRay(transform.position, _cameraDirection * 20f, Color.green);                 // Raycast'i yeşil renkte görselleştir.
                if (_hit.collider.CompareTag("Player"))                                                 // Eğer raycast Player tag'li bir objeye çarparsa 
                {
                    OnPlayerDetected?.Invoke();                                                         // Hiç abonesi olmasa bile eventi çağir.
                }
            }
        }
    }

    /*---------------------------------------------------------*/

    public void TriggerAlarmForBots()
    {
        foreach (AI_Controller _aiData in _moreBotAI)       // Topladığımız AI_Controller scriptlerinin hepsini dön.
        {
            _aiData._alarmMode = true;                      // Ve orada ki alarmMode değerlerini true çevir.
        }   
    }
    #endregion
}
