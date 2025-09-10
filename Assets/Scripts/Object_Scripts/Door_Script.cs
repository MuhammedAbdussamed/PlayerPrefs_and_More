using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Door_Script : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI interactionText;
    private Animator textAnimator;
    private Animator doorAnimator;

    // Script references
    private PlayerController playerScript;

    // Bools
    private bool _isPlayerIn;           // Karakterin kapi colliderinin içinde olup olmadiğini kontrol eden bool.
    private bool _usedOnce;             // Kapinin tek kullanimlik olmasi için bir bool

    void Start()
    {
        playerScript = PlayerController.Instance;                       // Script referansi.
        textAnimator = warningText.gameObject.GetComponent<Animator>(); // Metnin animator referansi.
        doorAnimator = GetComponent<Animator>();                        // Kapinin animator referansi.
    }


    void Update()
    {
        StartCoroutine(OpenDoor());
    }

    void OnTriggerEnter(Collider col) { DetectPlayer(col, true); }

    void OnTriggerExit(Collider col) { DetectPlayer(col, false); }

    #region Functions

    void DetectPlayer(Collider col, bool isEntering)
    {
        if (col.CompareTag("Player") || col.CompareTag("Invisible"))
        {
            if (!_usedOnce)
            {
                interactionText.gameObject.SetActive(isEntering);
                _isPlayerIn = isEntering;
            }
        }
    }

    IEnumerator OpenDoor()
    {
        if (_isPlayerIn && playerScript._interactionInput && !_usedOnce)  // Karakter kapının yanindaysa ve tuşa basmışsa...
        {
            if (!playerScript.iskey)    // Eğer anahtari yoksa...
            {
                interactionText.gameObject.SetActive(false);    // Etkileşim metnini kapat.
                warningText.gameObject.SetActive(true);         // Anahtar gerekli yazisini çıkar
                textAnimator.SetTrigger("Showing");             // Animasyonu başlat
                yield return new WaitForSeconds(2.5f);          // 2.5 saniye bekle
                warningText.gameObject.SetActive(false);        // Anahtar gerekli yazisini kapat.
            }

            else                        // Eğer anahtari varsa... 
            {
                doorAnimator.SetTrigger("isOpen");              // Kapiyi açma animasyonunu oynat
                _usedOnce = true;                               // Tek kullanimlik değişkeni true çevir.
                interactionText.gameObject.SetActive(false);    // Etkileşim metnini kapat.
            }
        }
    }
    
    #endregion
}
