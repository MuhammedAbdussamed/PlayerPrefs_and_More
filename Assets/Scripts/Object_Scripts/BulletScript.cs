using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (col.collider.CompareTag("Wall"))
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);                  // LocalPosition parent objecte göre pozisyon alır.
        }
        else if (col.collider.CompareTag("AI_Bot"))
        {
            AI_Controller _deathBool = col.collider.GetComponent<AI_Controller>();
            _deathBool._isDeath = true;

            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}
