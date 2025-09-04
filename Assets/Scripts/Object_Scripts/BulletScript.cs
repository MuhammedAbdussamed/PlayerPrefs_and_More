using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player") || col.collider.CompareTag("Invisible"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (col.collider.CompareTag("Wall"))
        {
            transform.localPosition = Vector3.zero;                  // LocalPosition parent objecte göre pozisyon alır. 0 yapilirsa parent object ile ayni pozisyonda olur.
        }
        else if (col.collider.CompareTag("AI_Bot"))
        {
            AI_Controller _deathBool = col.collider.GetComponent<AI_Controller>();
            _deathBool._isDeath = true;

            transform.localPosition = Vector3.zero;                 
        }
    }
}
