using UnityEngine;

public class Exit_Game : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatiliyor...");
    }
}
