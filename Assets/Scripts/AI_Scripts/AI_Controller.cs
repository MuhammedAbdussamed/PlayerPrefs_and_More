using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    [Header("BotComponents")]
    public NavMeshAgent _botAI;
    public Transform _transform;

    [Header("TransformReferences")]
    public Transform _player;
    public Transform[] _pointTransforms = new Transform[4];

    [Header("Distances")]
    public float[] _distances = new float[4];

    [Header("Bools")]
    public bool _alarmMode;
    public bool[] _pointBools = new bool[4];
    public bool _isPlayerIn;
    public bool _isFollowing;

    [Header("Variables")]
    public Vector3 _lookAtDelay;

    [Header("States")]
    public IState _currentState;
    public IState _patrolState;
    public IState _alarmState;
    public IState _followState;

    void Awake()
    {
        _patrolState = new PatrolState();
        _alarmState = new AlarmState();
        _followState = new FollowState();
        _currentState = _patrolState;
    }

    void Start()
    {
        _currentState.Enter(this);
    }

    void Update()
    {
        _currentState.Update(this);
        StartCoroutine(UpdatePosition());
    }

    #region Functions

    public void ChangeState(IState _newState)
    {
        _currentState.Exit(this);
        _currentState = _newState;
        _currentState.Enter(this);
    }

    /*----------------------------------*/

    

    /*----------------------------------*/

    IEnumerator UpdatePosition()
    {
        for (int i = 0; i < _distances.Length; i++)
        {
            _distances[i] = Vector3.Distance(_transform.position, _pointTransforms[i].position);    // Atanan transformların hepsini teker teker dönüp distances float dizisine kaydet. 
        }

        for (int i = 0; i < _distances.Length; i++)
        {
            if (_distances[i] < 0.2f)                                                    // Distance dizisini dönüp uzakliklara bak. Eğer 0.2den yakin olan varsa onun booleanını true çevir.
            {
                if (i == 3)
                {
                    _pointBools[i] = true;
                    yield return new WaitForSeconds(1f);
                    for (int il = 0; il < _pointBools.Length; il++)
                    {
                        _pointBools[il] = false;
                    }
                }
                else
                {
                    _pointBools[i] = true;
                }
            }
        }
    }

    #endregion

}
