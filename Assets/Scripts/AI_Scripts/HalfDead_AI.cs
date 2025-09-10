using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HalfDead_AI : MonoBehaviour
{
    [Header("Half-Dead Properties")]
    [SerializeField] public float reDeadTime;       // Diriltikten sonra tekrar ölme sayacı.
    [SerializeField] public bool isDead;            // Ölü mü , değil mi ?
    private bool isFollowing;                       // Karakteri takip etmeye başladi mi?
    private bool reDeadCoroutineRunning;

    [SerializeField] private Rigidbody rb;          // Rigidbody componenti
    [SerializeField] public NavMeshAgent botAI;     // Yapay zeka componenti.


    [Header("Script References")]
    [SerializeField] public AI_Controller aiScript; // AI Script referansı
    private PlayerController playerScript;          // PlayerController script referansı

    void Start()
    {
        rb.isKinematic = true;
        playerScript = PlayerController.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ChangeState();

        if (isFollowing && !reDeadCoroutineRunning)
        {
            StartCoroutine(ReDead());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        KillPlayer(col);
    }

    #region

    void ChangeState()
    {
        if (!isDead)
        {
            isFollowing = true;
            rb.isKinematic = false;
            botAI.SetDestination(playerScript.transform.position);
        }
        else
        {
            rb.isKinematic = true;                      // Rigidbody tarafinda botu durdur.
            botAI.isStopped = true;                     // Botu durdur
            botAI.ResetPath();                          // Hedefi sıfırla
        }
    }

    void KillPlayer(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator ReDead()
    {
        if (!isFollowing) { yield break; }                // Kovaliyor false ise döngüyü kır

        reDeadCoroutineRunning = true;
        yield return new WaitForSeconds(reDeadTime);
        isDead = true;
        reDeadCoroutineRunning = false;
    }

    public void Revive()
    {
        StopAllCoroutines();
        isDead = false;
        isFollowing = false;
        reDeadCoroutineRunning = false;
    }

    #endregion
}
