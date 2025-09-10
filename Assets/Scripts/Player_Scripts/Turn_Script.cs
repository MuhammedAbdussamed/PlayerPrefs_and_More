using UnityEngine;

public class Turn_Script : MonoBehaviour
{
    [Header("Script References")]
    private PlayerController _playerScript;

    [Header("Variables")]
    private Direction _currentDirection;

    void Awake()
    {
        
        _currentDirection = Direction.Forward;          // Karakterimiz en başta ileri bakacak şekilde ayarlı.
    }

    void Start()
    {
        _playerScript = PlayerController.Instance;      // Instance script ataması awake'de yapılırsa null hatası verir.
    }

    void Update()
    {
        Turn();
        SetEffectRotation();
        ClampEffectRotation();
    }

    void Turn()
    {
        if (_playerScript._inputs.FindActionMap("Look").FindAction("LookLeft").triggered)
        {
            _currentDirection = (Direction)(((int)_currentDirection + 1) % 4); // Şimdiki yönün indexini bir sonra ki yöne ata. = (Direction)(Bir sonra ki yönün indexi)
        }

        else if (_playerScript._inputs.FindActionMap("Look").FindAction("LookRight").triggered)
        {
            _currentDirection = (Direction)(((int)_currentDirection + 3) % 4); // +3 ile -1 aynı sonucu verir. Bizim istediğimiz pozitif bir sonuç olduğu için +3 yaziyoruz.
        }

        switch (_currentDirection)      // Switch case yapıları Vector3 karşılaştıramaz. Bu yüzden enumdan yararlandik
        {
            case Direction.Forward:                                                     //
                SetCamera(new Vector3(10f, 10f, 0f), new Vector3(0f, -90f, 0f));        // Eğer Direction.Forward ise SetCamera fonksiyonunu bu değişkenler ile çalıştır.
                break;                                                                  //

            case Direction.Right:
                SetCamera(new Vector3(0f, 10f, 10f), new Vector3(0f, -180f, 0f));
                break;

            case Direction.Left:
                SetCamera(new Vector3(-10f, 10f, 0f), new Vector3(0f, -270f, 0f));
                break;

            case Direction.Back:
                SetCamera(new Vector3(0f, 10f, -10f), new Vector3(0f, 0f, 0f));
                break;
        }
    }

    void SetCamera(Vector3 cameraOffSet, Vector3 rotation)
    {
        _playerScript._playerCamera.FollowOffset = cameraOffSet;    // Kamera uzaklığını ayarla. Switch case de verilen değerler gelecek.
        transform.rotation = Quaternion.Euler(rotation);            // Bu objenin rotasyonunu rotation değişkeni yap. 
    }

    void SetEffectRotation()
    {
        Vector3 effectRotation = -transform.forward;                // Bu değişkeni karakterin tersi rotasyonu alacak şekilde ayarla.

        _playerScript._speedEffect.transform.rotation = Quaternion.LookRotation(effectRotation,Vector3.up); // Rotasyon verip objenin oraya bakmasini sağlar.
    }

    void ClampEffectRotation()
    {
        Vector3 effectRotation = new Vector3(-90f, 180f, 0f);
        _playerScript._speedEffect.transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
    }

    enum Direction
    {
        Forward, Right, Left, Back      // Kamera ve karakterin döneceği yönü belirlemek için bir enum oluşturduk.
    }
}
