using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }       // Bu kodun heryerden erişilir olmasını sağla.

    [Header("PlayerProperties")]
    public float _speed;
    public float _fallSpeed;
    public float _coin; 
    public float _necesserrayCoin;                                      // Bölümü geçmek için gerekli olan coin sayısı.

    [Header("InputReferences")]
    public InputActionAsset _inputs;                                    // Inputlari alacağimiz değişken

    [Header("Materials")]
    [SerializeField] Material _material0;
    [SerializeField] Material _material1;
    [Tooltip("Character's common materials array")]
    private Material[] _playerMaterial;

    [Tooltip("Character's color/material when DESTROY power is active.")]
    [SerializeField] private Material _terminatorMaterial;

    [Tooltip("Character's color/material when INVİSİBLE power is active.")]
    [SerializeField] private Material _invisibleMaterial;

    // Components
    private MeshRenderer _playerMeshRenderer;
    [HideInInspector] public Rigidbody _rb;

    // Objects References
    public CinemachineFollow _playerCamera;
    [HideInInspector] public Transform _turner;

    // Effects References
    [HideInInspector] public ParticleSystem _speedEffect;
    [HideInInspector] public ParticleSystem _destroyEffect;
    private Material[] mats;

    // State Bools
    [HideInInspector] public bool _isGrounded;
    [HideInInspector] public bool _isWalking;
    [HideInInspector] public bool _isFalling;
    [HideInInspector] public bool _isDestroying;
    [HideInInspector] public bool _isInvisible;

    // Movement Variable
    [HideInInspector] public Vector2 _moveDirection;
    [HideInInspector] public bool _interactionInput;
    [HideInInspector] public bool _isLooking;
    [HideInInspector] public bool _preciousStuff;
    [HideInInspector] public bool iskey;

    // States
    [HideInInspector] public IState_Player _idleState;
    [HideInInspector] public IState_Player _walkState;
    [HideInInspector] public IState_Player _fallState;
    [HideInInspector] public IState_Player _currentState;

    private void Awake()
    {
        if (Instance == null)                             //    Bu kalıplaşmış bir koddur. Heryerden ulaşılabilecek olan script eğer nullsa yani yoksa...
        {                                                //
            Instance = this;                            //      Bu scripti seç.
        }                                                //
        else                                            //      Eğer birden fazla varsa...
        {                                              //
            Destroy(gameObject);                        //      O zaman bunu yok et.
        }                                              //

        _idleState = new IdleState();
        _walkState = new WalkState();
        _fallState = new FallState();

        _currentState = _idleState;

        /* Components Assign */
        _rb = GetComponent<Rigidbody>();

        _playerMeshRenderer = transform.Find("Player").GetComponent<MeshRenderer>();

        _turner = transform.Find("Turner").GetComponent<Transform>();

        /* Effects Assign */
        _speedEffect = transform.Find("SpeedEffect").GetComponent<ParticleSystem>();        // Speed up efekti
        _destroyEffect = transform.Find("DestroyEffect").GetComponent<ParticleSystem>();    // Destroy efekti

        /* Array Assign */
        mats = _playerMeshRenderer.materials;
        _playerMaterial = new Material[] { _material0, _material1 };        // Karaktere materyal ekleneceği zaman buraya yazmak yeterli olur. <-----------
    }

    private void Start()
    {
        _inputs.Enable();
    }

    private void Update()
    {
        /* Transition */
        TransitionRunState();
        TransitionFallState();

        /* Inputs */
        _moveDirection = _inputs.FindActionMap("Movement").FindAction("Move").ReadValue<Vector2>();     // Inputtan gelen Vector2 değerini _moveDirection değişkenine ata.
        _interactionInput = _inputs.FindActionMap("interaction").FindAction("interact").triggered;      // Inputtan gelen girdiyi _interactionInputı true çevirmek için kullan.

        /* Functions */
        BecomeInvisible();            // Görünmez olma fonksiyonu
        ChangeMaterial();             // Süper güce göre material (renk) değiştirme fonksiyonu.

        _currentState.Update(this);   // Güncel State'in update'inde ne varsa onu yap.
    }

    #region Functions

    /*--------------------------------------------*/

    public void ChangeState(IState_Player newState)
    {
        _currentState.Exit(this);                       // Güncel state'in Exit() fonksiyonunu çalıştır.
        _currentState = newState;                       // State'i newState'e güncelle.
        _currentState.Enter(this);                      // Yeni State'in giriş "Enter()" fonksiyonunu çalıştır.
    }

    /*--------------------------------------------*/

    void BecomeInvisible()
    {
        if (_isInvisible)
        {
            gameObject.tag = "Invisible";               // Tag'i Invisible yap
            gameObject.layer = 17;                      // Layeri 17. index'e ata. ( Invisible )
        }

        else
        {
            gameObject.tag = "Player";                  // Tag'i Player yap.
            gameObject.layer = 12;                      // Layeri 12. index'e ata. ( Characters )
        }
    }

    /*--------------------------------------------*/

    void ChangeMaterial()
    {

        if (_isDestroying)                                         // Destroy özel gücü açıksa...
        {
            ChangeMaterialMiniFunction(_terminatorMaterial);            // Destroy materiali kuşan.
        }

        else if (_isInvisible)                                     // Görünmezlik özel gücü açıksa...
        {
            ChangeMaterialMiniFunction(_invisibleMaterial);             // Görünmezlik materiali kuşan.
        }

        else                                                       // Hiçbir süper güç açık değilse ya da speed up özel gücü açıksa...
        {
            for (int i = 0; i < _playerMaterial.Length; i++)
            {
                mats[i] = _playerMaterial[i];                       // Karakterin normal materyalini bütün materyallere kuşan.
            }
            _playerMeshRenderer.materials = mats;                   // Sonra da atamasini yap
        }
    }

    /*--------------------------------------------*/

    void ChangeMaterialMiniFunction(Material material)
    {
        for (int i = 0; i < mats.Length; i++)                       // Material dizisini al...
        {
            mats[i] = material;                                     // Dizide ki bütün elemanlari materiale eşitle
        }
        _playerMeshRenderer.materials = mats;                       // Uygula.
    }

    #endregion


    #region Transition

    void TransitionRunState()
    {
        if (_moveDirection.x != 0f || _moveDirection.y != 0f)     // Eğer input değeri 0'dan büyükse devam et...
        {
            if (_isGrounded)                                      // Karakter yerdeyse devam et...
            {
                _isWalking = true;                                // Yürüme değişkenini true çevir.
            }
            else
            {
                _isWalking = false;
            }
        }
        else
        {
            _isWalking = false;
        }
    }

    /*--------------------------------------------*/

    void TransitionFallState()
    {
        if (_rb.linearVelocity.y < 0f)
        {
            _isFalling = true;
        }
        else if (_isGrounded)
        {
            _isFalling = false;
        }
    }

    #endregion
}
