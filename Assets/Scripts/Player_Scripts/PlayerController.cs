using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }       // Bu kodun heryerden erişilir olmasını sağla.

    [Header("PlayerProperties")]
    public float _speed;
    public float _fallSpeed;
    public float _coin;
    public float _necesserrayCoin;                                                                                    // Bölümü geçmek için gerekli olan coin sayısı.
    [HideInInspector] private List<int> _invisibleCollisionLayer;                                                     // Görünmezlik modunda karakterin hangi katmanlar ile çarpışmayacağı.

    [Header("InputReferences")] 
    [SerializeField] private InputActionAsset _inputs;

    // Components
    [HideInInspector] public Rigidbody _rb;
    [HideInInspector] private SphereCollider _sphereCollider;
    [HideInInspector] private MeshRenderer _meshRenderer;

    // State Bools
    [HideInInspector] public bool _isGrounded;
    [HideInInspector] public bool _isWalking;
    [HideInInspector] public bool _isFalling;
    [HideInInspector] public bool _isDestroying;
    [HideInInspector] public bool _isInvisible;
    
    // Movement Variable
    [HideInInspector] public Vector2 _moveDirection;
    [HideInInspector] public bool _interactionInput;

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

        _rb = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        SetNecesserrayCoin();
        _inputs.Enable();
    }

    private void Update()
    {
        /* Transition */
        TransitionRunState();
        TransitionFallState();

        /* Inputs */
        _moveDirection = _inputs.FindActionMap("Movement").FindAction("Move").ReadValue<Vector2>();      // Inputtan gelen Vector2 değerini _moveDirection değişkenine ata.
        _interactionInput = _inputs.FindActionMap("Interaction").FindAction("Interact").triggered;       // Inputtan gelen girdiyi _interactionInputı true çevirmek için kullan.

        /* Functions */     
        BecomeInvisible();
       
        _currentState.Update(this);                                                                      // Güncel State'in update'inde ne varsa onu yap.
    }

    #region Functions

    public void ChangeState(IState_Player newState)
    {
        _currentState.Exit(this);                       // Güncel state'in Exit() fonksiyonunu çalıştır.
        _currentState = newState;                       // State'i newState'e güncelle.
        _currentState.Enter(this);                      // Yeni State'in giriş "Enter()" fonksiyonunu çalıştır.
    }

    void BecomeInvisible()
    {
        if (_isInvisible)
        {
            gameObject.tag = "Invisible";               // Tag'i Invisible yap
            gameObject.layer = 17;                      // Layeri 17. index'e ata. ( Invisible )

            _meshRenderer.enabled = false;              // Meshrenderer'i kapat. ( görünürlüğü kapat )
        }
        
        else
        {
            gameObject.tag = "Player";                  // Tag'i Player yap.
            gameObject.layer = 12;                      // Layeri 12. index'e ata. ( Characters )

            _meshRenderer.enabled = true;               // Meshrenderer'ı aç
        }
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

    void SetNecesserrayCoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _necesserrayCoin = 4;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _necesserrayCoin = 10;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            _necesserrayCoin = 15;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            _necesserrayCoin = 20;
        }
    }

    #endregion


}
