using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("TextReference")]
    [SerializeField] private TextMeshProUGUI _textInteract;

    [Header("ScriptReferences")]
    private PlayerController _playerData;

    [Header("Bools")]
    private bool _isPlayerIn;

    void Start()
    {
        _playerData = PlayerController.Instance;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))                   // Player tag'li bir obje collidera girerse devam et...
        {
            _textInteract.gameObject.SetActive(true);   // Etkileşim yazisini aktif et. 
            _isPlayerIn = true;                         // Karakter içerde mi değişkenini true çevir.
        }
    }

    void OnTriggerExit(Collider col)                    
    {
        if (col.CompareTag("Player"))                   // Player tag'li bir obje colliderdan çıkarsa devam et...
        {
            _textInteract.gameObject.SetActive(false);  // Etkileşim yazısını deaktif et.
            _isPlayerIn = false;                        // Karakter içerde mi değişkenini false çevir
        }
    }

    void Update()
    {
        ChangeSceneSaveCoin(8);
    }

    void ChangeSceneSaveCoin(int coinNumber)
    {
        if (_playerData._coin >= coinNumber && _playerData._interactionInput && _isPlayerIn) // Coin sayısı yeterince fazlaysa , etkileşim tuşuna basıldıysa ve karakter içerdeyse devam et...
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Bu sahnenin indexini al ona 1 ekle ve sahneyi yükle.
            PlayerPrefs.SetFloat("Coins", _playerData._coin);                       // Oyuncuda bulunan coin sayısını "Coins" başlığı altına kaydet.
            PlayerPrefs.Save();
        }
    }

}
