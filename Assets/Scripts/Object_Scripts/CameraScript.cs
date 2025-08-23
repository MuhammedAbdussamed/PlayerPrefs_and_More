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

    [Header("ScriptReferences")]
    public AI_Controller[] _moreBotAI;

    [Header("UnityEvents")]
    public UnityEvent OnPlayerDetected;         // Bu tarz eventler inspectordan tetiklenebilir.

    private void Start()
    {
        _moreBotAI = FindObjectsByType<AI_Controller>(FindObjectsSortMode.None);    // Bütün AI_Controller scriptlerini topla . Siralama modu da kapalı olsun böyle daha hizli.
    }

    public void Update()
    {
        if (!_isTurning)
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

    IEnumerator Turn()
    {
        CheckTurnDirection();
        _isTurning = true;

        /*Şimdi ki ve hedef açi arasinda ki fark 0.1 den küçük olana dek bu döngüyü devam ettir*/
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, _targetY)) > 0.1f)    // Mathf.DeltaAngle iki açi arasinda ki en kisa farki verir. Açilarla çaliştiğimiz için mutlak değer almak istiyoruz bu yüzden mathf.abs kullaniriz.
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(20f, _targetY, 0f), 20f * Time.deltaTime);    // RotateTowards ile yavaş yavaş döndürürüz. İki tane de açi parametresi alir.
            yield return null; // her frame güncelle
        }

        yield return new WaitForSeconds(2f);
        _isTurning = false;
    }

    void CheckTurnDirection()
    {
        if (transform.rotation == Quaternion.Euler(20f, _turnAngle, 0f))
        {
            _isFacingRight = true;
        }
        else if (transform.rotation == Quaternion.Euler(20f, -_turnAngle, 0f))
        {
            _isFacingRight = false;
        }
        _targetY = _isFacingRight ? -_turnAngle : _turnAngle;
    }

    void PlayerDetect(Collider col, bool enterExit)
    {
        if (col.CompareTag("Player"))
        {
            OnPlayerDetected?.Invoke(); // Hiç abonesi olmasa bile eventi çağir.
        }
    }

    public void TriggerAlarmForBots()
    {
        foreach (AI_Controller _aiData in _moreBotAI)
        {
            _aiData._alarmMode = true;
        }
    }
}
