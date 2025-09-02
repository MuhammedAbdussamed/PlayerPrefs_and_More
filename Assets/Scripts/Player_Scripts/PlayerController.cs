using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }       // Bu kodun heryerden erişilir olmasını sağla.

    [Header("PlayerProperties")]
    public float _speed;
    public float _fallSpeed;
    public float _coin;
    public float _necesserrayCoin;                                      // Bölümü geçmek için gerekli olan coin sayısı.
    public float _mouseSensivity;                                       // Mouse hassasiyeti.
    
    [Header("InputReferences")] 
    [SerializeField] private InputActionAsset _inputs;

    [Header("Materials")]
    [Tooltip("Character's common material")]
    [SerializeField] private Material _playerMaterial;

    [Tooltip("Character's color/material when DESTROY power is active.")]
    [SerializeField] private Material _terminatorMaterial;

    [Tooltip("Character's color/material when INVİSİBLE power is active.")]
    [SerializeField] private Material _invisibleMaterial;

    // Components
    [SerializeField] private CinemachineFollow _playerCamera;
    [HideInInspector] public Rigidbody _rb;
    [HideInInspector] private Transform _noise;
    [HideInInspector] private MeshRenderer _playerMeshRenderer;

    // Effects References
    [HideInInspector] public ParticleSystem _speedEffect;
    [HideInInspector] public ParticleSystem _destroyEffect;
    
    // State Bools
    [HideInInspector] public bool _isGrounded;
    [HideInInspector] public bool _isWalking;
    [HideInInspector] public bool _isFalling;
    [HideInInspector] public bool _isDestroying;
    [HideInInspector] public bool _isInvisible;
    
    // Movement Variable
    [HideInInspector] public Vector2 _moveDirection;
    [HideInInspector] public Vector3 _turnDirection;
    [HideInInspector] public bool _cameraInputRight;
    [HideInInspector] public bool _cameraInputLeft;
    [HideInInspector] public bool _interactionInput;
    [HideInInspector] public bool _isLooking;
    [HideInInspector] private List<int> _invisibleCollisionLayer;       // Görünmezlik modunda karakterin hangi katmanlar ile çarpışmayacağı.

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

        _noise = transform.Find("Noise").GetComponent<Transform>();

        /* Effects Assign */

        _speedEffect = transform.Find("SpeedEffect").GetComponent<ParticleSystem>();
        _destroyEffect = transform.Find("DestroyEffect").GetComponent<ParticleSystem>();


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
        _cameraInputRight = _inputs.FindActionMap("Look").FindAction("LookRight").triggered;             // Inputtan gelen Vector2 değerini _cameraInput değişkenine ata.
        _cameraInputLeft = _inputs.FindActionMap("Look").FindAction("LookLeft").triggered;

        /* Functions */
        BecomeInvisible();
        ChangeMaterial();
        LookTurnRight();
        LookTurnLeft();
       
        _currentState.Update(this);                                                                      // Güncel State'in update'inde ne varsa onu yap.
    }

    #region Functions

    void LookTurnRight()
    {
        if (_cameraInputRight)
        {
            if (_playerCamera.FollowOffset == new Vector3(10f, 10f, 0f))
            {
                _playerCamera.FollowOffset = new Vector3(0f, 10f, 10f);
                _turnDirection = new Vector3(0f, -180f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(0f, 10f, 10f))
            {
                _playerCamera.FollowOffset = new Vector3(-10f, 10f, 0f);
                _turnDirection = new Vector3(0f, -270f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, -270f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(-10f, 10f, 0f))
            {
                _playerCamera.FollowOffset = new Vector3(0f, 10f, -10f);
                _turnDirection = new Vector3(0f, 0f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(0f, 10f, -10f))
            {
                _playerCamera.FollowOffset = new Vector3(10f, 10f, 0f);
                _turnDirection = new Vector3(0f, -90f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            }
            transform.rotation = Quaternion.Euler(_turnDirection);
        }
    }

    void LookTurnLeft()
    {
        if (_cameraInputLeft)
        {
            if (_playerCamera.FollowOffset == new Vector3(10f, 10f, 0f))
            {
                _playerCamera.FollowOffset = new Vector3(0f, 10f, -10f);
                _turnDirection = new Vector3(0f, 0f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(0f, 10f, -10f))
            {
                _playerCamera.FollowOffset = new Vector3(-10f, 10f, 0f);
                _turnDirection = new Vector3(0f, 90f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(-10f, 10f, 0f))
            {
                _playerCamera.FollowOffset = new Vector3(0f, 10f, 10f);
                _turnDirection = new Vector3(0f, 180f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            else if (_playerCamera.FollowOffset == new Vector3(0f, 10f, 10f))
            {
                _playerCamera.FollowOffset = new Vector3(10f, 10f, 0f);
                _turnDirection = new Vector3(0f, 270f, 0f);
                _noise.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            }
            transform.rotation = Quaternion.Euler(_turnDirection);
        }
    }

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
        if (_isDestroying)
        {
            _playerMeshRenderer.material = _terminatorMaterial;
        }
        else if (_isInvisible)
        {
            _playerMeshRenderer.material = _invisibleMaterial;
        }
        else
        {
            _playerMeshRenderer.material = _playerMaterial;
        }
    }

    /*--------------------------------------------*/
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

    /*--------------------------------------------*/

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
