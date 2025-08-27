using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI_Controller : MonoBehaviour
{
    [Header("Bot Components")]
    public NavMeshAgent _botAI;
    public Transform _transform;

    [Header("Transform References")]
    public Transform[] _pointTransforms = new Transform[4];

    [Header("Variables")]
    public Vector3 _lookAtDelay;

    [Header("Script References")]
    [HideInInspector] public PlayerController _playerScript;

    [Header("Distances")]
    [HideInInspector] public float[] _distances = new float[4];

    [Header("Bools")]
    [HideInInspector] public bool _alarmMode;
    [HideInInspector] public bool[] _pointBools = new bool[4];
    [HideInInspector] public bool _isPlayerIn;
    [HideInInspector] public bool _isFollowing;

    [Header("States")]
    [HideInInspector] public IState _currentState;
    [HideInInspector] public IState _patrolState;
    [HideInInspector] public IState _alarmState;
    [HideInInspector] public IState _followState;

    void Awake()
    {
        _patrolState = new PatrolState();
        _alarmState = new AlarmState();
        _followState = new FollowState();

        _currentState = _patrolState;
    }

    void Start()
    {
        _playerScript = PlayerController.Instance;
        _currentState.Enter(this);
    }

    void Update()
    {
        _currentState.Update(this);             // Güncel State'in update fonksiyonunu yerine getir.
        StartCoroutine(UpdatePosition());
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

    IEnumerator UpdatePosition()
    {
        for (int i = 0; i < _distances.Length; i++)
        {
            _distances[i] = Vector3.Distance(_transform.position, _pointTransforms[i].position);    // Atanan transformların hepsini teker teker dön ve bu objenin pozisyonuna uzaklıklarını _distances dizisine kaydet.
        }

        for (int i = 0; i < _distances.Length; i++)
        {
            if (_distances[i] < 0.2f)                                                               // Distance dizisini dönüp uzakliklara bak. Eğer 0.2den yakin olan varsa onun booleanını true çevir.
            {
                if (i == 3)                                                                         // Eğer 3. bool'da true dönerse devam et.
                {
                    _pointBools[i] = true;                                                          // 3. bool'u true çevir.
                    yield return new WaitForSeconds(1f);                                            // 1 saniye bekle.

                    for (int il = 0; il < _pointBools.Length; il++)                                 // Bütün pointBools değerlerini dön .
                    {
                        _pointBools[il] = false;                                                    // Bütün bool değerlerini false çevir.
                    }
                }
                else                                                                                // Eğer bool 3 değilse yani döngü 0 , 1 ya da 2. boolda ise devam et...
                {
                    _pointBools[i] = true;                                                          // Bool'u true çevir.
                }
            }
        }
    }

    /*----------------------------------*/

    void KillPlayer(Collision col)
    {
        if (col.collider.CompareTag("Player"))                                                      // Eğer Player tag'li bir obje ile çarpışırsa devam et...
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);                       // Sahneyi tekrar yükle.                                 
        }
    }

    #endregion

}
