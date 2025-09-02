using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Super_Power_Object : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerController _playerScript;                 // Karakterimiz ile çarpışınca değer alacak olan boş bir değişken.

    [Header("Object Properties")]
    [SerializeField] private float _coolDown;

    [Header("Variables")]
    [HideInInspector] public float _superPowerEndTime;      // UI elementine iletmek için duration değerini tutacak ( saniye )

    [Header("SuperPowers")]
    [SerializeField] public Base_Class _speedPower;         // Super güç referansı.
    [SerializeField] public Base_Class _invisiblePower;     // Süper güç referansı.
    [SerializeField] public Base_Class _destroyPower;       // Süper güç referansı.

    [Header("Bools")]
    [HideInInspector] public bool _usedOnce;            // Süper gücü 1 kere kullandiğimizdan emin olmak için bool değişkeni
    [HideInInspector] public bool _useSpeedUp;          // Speed Up değişkenini kullanmak istersek kullanacağimiz bool.
    [HideInInspector] public bool _useInvisible;        // Invisible değişkenini kullanmak istersek kullanacağimiz bool.
    [HideInInspector] public bool _useDestroy;          // Destroy değişkenini kullanmak istersek kullanacağimiz bool.

    void Update()
    {
        _superPowerEndTime -= Time.deltaTime;
        Debug.Log(_superPowerEndTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !_usedOnce)
        {
            _usedOnce = true;

            _playerScript = col.GetComponent<PlayerController>();

            RandomPowerChase();

            UseSuperPower();

            StartCoroutine(ResetObject());
        }
    }

    #region Functions

    void UseSuperPower()
    {
        if (_useSpeedUp) { StartCoroutine(SpeedUp()); StartCoroutine(AssignDuration(_speedPower)); }

        if (_useInvisible) { StartCoroutine(BecomeInvisible()); StartCoroutine(AssignDuration(_invisiblePower)); }

        if (_useDestroy) { StartCoroutine(TerminatorMode()); StartCoroutine(AssignDuration(_destroyPower)); }
    }

    /*-----------------------------------------------------*/

    IEnumerator SpeedUp()
    {
        _playerScript._speed += _speedPower._speedUpValue;          // Artık nadirlik seçildi ve değişkenler ona göre ayarlandi. Hız değişkenini uygula.

        yield return new WaitForSeconds(_speedPower._durationTime); // Süper gücün etki süresi kadar bekle

        _playerScript._speed -= _speedPower._speedUpValue;          // Hız değişkenini geri çek . ( Etkiyi bitir )

        _useSpeedUp = false;
    }

    /*-----------------------------------------------------*/

    IEnumerator BecomeInvisible()
    {
        _playerScript._isInvisible = true;                                  // _isInvisible değişkenini true çevir

        yield return new WaitForSeconds(_invisiblePower._durationTime);     // Etki süresi kadar bekle

        _playerScript._isInvisible = false;                                 // _isInvisible değişkenini false çevir

        _useInvisible = false;
    }

    /*-----------------------------------------------------*/

    IEnumerator TerminatorMode()
    {
        _playerScript._isDestroying = true;

        yield return new WaitForSeconds(_destroyPower._durationTime);

        _playerScript._isDestroying = false;

        _useDestroy = false;
    }

    /*-----------------------------------------------------*/

    void RandomPowerChase()
    {
        int randomNumbers = Random.Range(0, 10);          // 0 ila 10 arasinda rasgele bir sayı seç. 0 dahil , 10 hariç

        if (randomNumbers < 4)      // 4 ihtimal. % 40 ihtimalle destroy özelliği   
        {
            _useDestroy = true;
            _destroyPower.RandomizeRarity();
        }

        else if (randomNumbers < 7) // 3 ihtimal. % 30 ihtimalle speedUp özelliği
        {
            _useSpeedUp = true;
            _speedPower.RandomizeRarity();
        }

        else                        // 3 ihtimal. % 30 ihtimalle Invisible özelliği
        {
            _useInvisible = true;
            _invisiblePower.RandomizeRarity();
        }
    }

    /*--------------------------------------------------------*/

    IEnumerator AssignDuration(Base_Class superPowerName)
    {
        _superPowerEndTime = superPowerName._durationTime;
        yield break;    // Coroutine'i sonlandir. Bir değer döndürme.
    }

    /*--------------------------------------------------------*/

    IEnumerator ResetObject()
    {
        yield return new WaitForSeconds(_coolDown);
        _usedOnce = false;
    }
    
    #endregion

}
