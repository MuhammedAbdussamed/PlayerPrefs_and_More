using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactionInputs;
    [SerializeField] private PlayerController _playerData;
    [SerializeField] private TextMeshProUGUI _textInteract;
    private bool _letsGo;

    void Start()
    {
        _interactionInputs.action.Enable();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _textInteract.gameObject.SetActive(true);
            _letsGo = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _textInteract.gameObject.SetActive(false);
            _letsGo = false;
        }
    }

    void Update()
    {
        ChangeScene(14);
    }

    void ChangeScene(int coinNumber)
    {
        if (_playerData._coin >= coinNumber && _interactionInputs.action.triggered && _letsGo)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Bu sahnenin indexini al ona 1 ekle ve sahneyi yükle.
        }
    }

}
