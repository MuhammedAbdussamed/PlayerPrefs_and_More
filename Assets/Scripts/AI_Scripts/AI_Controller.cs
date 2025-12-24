using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI_Controller : MonoBehaviour
{
    [Header("Bot Properties")]
    public float _respawnTime;
    public float CornerWaitTime;
    public Rigidbody botRb;
    public NavMeshAgent _botAI;
    public GameObject _botNoise;

    [Header("Transform References")]
    public Transform[] _pointTransforms = new Transform[4];

    [Header("Script References")]
    [HideInInspector] public PlayerController _playerScript;

    [Header("Distances")]
    [HideInInspector] public float[] _distances = new float[4];

    [Header("Bools")]
    [HideInInspector] public bool[] _pointBools = new bool[4];
    [HideInInspector] public bool isLookingRight;
    [HideInInspector] public bool _alarmMode;
    [HideInInspector] public bool _isPlayerIn;
    [HideInInspector] public bool _isFollowing;
    [HideInInspector] public bool _isDeath;
    [HideInInspector] public bool isChasingCorner;

    [Header("States")]
    [HideInInspector] public IState _currentState;
    [HideInInspector] public IState _patrolState;
    [HideInInspector] public IState _alarmState;
    [HideInInspector] public IState _followState;
    [HideInInspector] public IState _deathState;
    

    void Awake()
    {
        _patrolState = new PatrolState();
        _alarmState = new AlarmState();
        _followState = new FollowState();
        _deathState = new DeathState();

        _currentState = _patrolState;
    }

    void Start()
    {
        _playerScript = PlayerController.Instance;

        transform.position = _pointTransforms[0].position;

        _currentState.Enter(this);
    }

    public virtual void Update()
    {
        _currentState.Update(this);             // Güncel State'in update fonksiyonunu yerine getir.

        StartCoroutine(UpdatePosition());       // Güncel pozisyonu boolarda tutan fonksiyon
    }

    void OnCollisionEnter(Collision col)
    {
        KillPlayer(col);
    }

    #region Functions

    public void ChangeState(IState _newState)           
    {
        _currentState.Exit(this);               // Güncel State'in Exit() fonksiyonunu çalıştır.
        _currentState = _newState;              // State'i newState'e güncelle.
        _currentState.Enter(this);              // Yeni state'in enter fonksiyonunu çalıştır.
    }

    /*----------------------------------*/

    public virtual IEnumerator UpdatePosition()
    {
        
        for (int i = 0; i < _distances.Length; i++)
        {
            _distances[i] = Vector3.Distance(transform.position, _pointTransforms[i].position);     // Atanan transformların hepsini teker teker dön ve bu objenin pozisyonuna uzaklıklarını _distances dizisine kaydet.

            if (_distances[i] < 0.5f)                                                               // Distance dizisini dönüp uzakliklara bak. Eğer 0.2den yakin olan varsa onun booleanını true çevir.
            {
                yield return new WaitForSeconds(CornerWaitTime);

                _pointBools[i] = true;
            }
            else
            {
                yield return new WaitForSeconds(CornerWaitTime);

                _pointBools[i] = false;
            }
        }
    }

    /*----------------------------------*/

    void KillPlayer(Collision col)
    {
        if (col.collider.CompareTag("Player") && _playerScript._isDestroying)
        {
            _isDeath = true;
        }

        else if (col.collider.CompareTag("Player") && !_playerScript._isDestroying)                 // Eğer Player tag'li bir obje ile çarpışırsa devam et...
        {
            _playerScript._isDeath = true;                                 
        }
    }

    #endregion

}
