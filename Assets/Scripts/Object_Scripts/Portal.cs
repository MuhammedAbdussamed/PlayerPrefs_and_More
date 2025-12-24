using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("TextReference")]
    [SerializeField] private TextMeshProUGUI _textInteract;

    [Header("ScriptReferences")]
    private PlayerController _playerScript;

    [Header("Animator")]
    [SerializeField] private Animator _blackAnimator;

    [Header("Bools")]
    private bool _isPlayerIn;

    void Start()
    {
        _playerScript = PlayerController.Instance;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Invisible"))                   // Player ya da Invisible tag'li bir obje collidera girerse devam et...
        {
            _textInteract.gameObject.SetActive(true);                                  // Etkileşim yazisini aktif et. 
            _isPlayerIn = true;                                                        // Karakter içerde mi değişkenini true çevir.
        }
    }

    void OnTriggerExit(Collider col)                    
    {
        if (col.CompareTag("Player") || col.CompareTag("Invisible"))                   // Player ya da Invisible tag'li bir obje colliderdan çıkarsa devam et...
        {
            _textInteract.gameObject.SetActive(false);                                 // Etkileşim yazısını deaktif et.
            _isPlayerIn = false;                                                       // Karakter içerde mi değişkenini false çevir
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(ChangeSceneSaveCoin(0));
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(FinishGame());
        }
        
    }

    IEnumerator ChangeSceneSaveCoin(int coinNumber)
    {
        if (_playerScript._coin >= coinNumber && _playerScript._interactionInput && _isPlayerIn) // Coin sayısı yeterince fazlaysa , etkileşim tuşuna basıldıysa ve karakter içerdeyse devam et...
        {
            _blackAnimator.SetBool("isClosed",true);
            yield return new WaitForSeconds(1.25f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Bu sahnenin indexini al ona 1 ekle ve sahneyi yükle.
        }
    }

    /*---------------------------------------------------------------*/

    IEnumerator FinishGame()
    {
        if (_playerScript._preciousStuff && _isPlayerIn && _playerScript._interactionInput)
        {
            _blackAnimator.SetBool("isClosed",true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
