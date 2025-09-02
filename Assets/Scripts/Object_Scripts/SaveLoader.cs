using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader : MonoBehaviour
{
    [Header("ScriptReferences")]
    private PlayerController _playerScript;

    void Start()
    {
        _playerScript = PlayerController.Instance;

        _playerScript._coin = PlayerPrefs.GetFloat("Coins", 0f);        // Karakterin coin sayısını "Coins" başlığı altına kaydet.

        StartCoin();
    }
    
    void StartCoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)              // Eğer sahne indexi sıfırsa devam et...
        {           
            PlayerPrefs.SetFloat("Coins", 0f);                          // Coini sıfıra eşitle.
            PlayerPrefs.Save();                                         // Kaydet
        }           
        else
        {
            _playerScript._coin = PlayerPrefs.GetFloat("Coins", 0f);    // Eğer sahne indexi 0 değilse karakterin coin sayısını kaydet.
        }
    }
    
}
