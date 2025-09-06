using UnityEngine;

public abstract class CollectableObject_BaseClass : MonoBehaviour
{
    [Header("TurnSpeed")]
    public float TurnSpeed;

    [Header("Sounds")]
    public AudioClip EffectSound;
    public AudioSource audioSource;

    // Script reference
    [HideInInspector] public PlayerController playerScript;

    protected virtual void Start()
    {
        playerScript = PlayerController.Instance;
    }

    protected virtual void Update()
    {
        Turn();
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Invisible"))
        {
            EnterFunction();
        }
    }                                                                                                                          

    protected void Turn()
    {                                                                                 // Bu kod = (0f , _turnSpeed *Time.deltaTime, 0f) ve dünya ekseninde döndür.   
        transform.Rotate(Vector3.up * TurnSpeed * 10f * Time.deltaTime, Space.World); // Vector3.up yönünde dünyaya göre döndür.
    }                                                                                 // Spce.Self yazilsaydi objenin kendi etrainda döndürürdü.

    public abstract void EnterFunction();
}
