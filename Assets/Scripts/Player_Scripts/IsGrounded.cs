using UnityEngine;

public class IsGrounded : MonoBehaviour
{

    [Header("ScriptReference")]
    private PlayerController _playerScript;

    [Tooltip("Raycast'in çarpabileceği katmanlar")]
    [SerializeField] private LayerMask _raycastLayers;

    void Start()
    {
        _playerScript = PlayerController.Instance;
    }

    void LateUpdate()       // LateUpdate frame sırasını önemser. Child object ya da camera gibi objelerde lateUpdate ile daha yumuşak geçişler sağlarız. ( önce Update hemen 1 frame ardından lateUpdate gerçekleşir )
    {
        transform.position = _playerScript.transform.position + new Vector3(0f, -0.4f, 0f); // Bu objenin pozisyonunu daima player objesinin pozisyonunun 0.4f altında tut.
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit _hit, 0.2f , _raycastLayers)) // Bu objenin transformundan aşağı doğru 0.2f uzunluğunda bir ışın at ve bilgileri _hit değişkenine yaz. Sadece _layerMask katmanlarina çarpsin.
        {
            _playerScript._isGrounded = true;                                               // Eğer ışın çarpmışsa karakter yerdeyi true çevir.
            Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.green);            // Ayrıca çarpmışsa giden ışını yeşil renkli olarak görselleştir.
        }
        else
        {
            _playerScript._isGrounded = false;                                              // Eğer çarpmamışsa karakter yerdeyi false çevir.
            Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);              // Ayrıca çarpmamışsa giden ışını kırmızı renkli olarak görselleştir.
        }
    }
}
