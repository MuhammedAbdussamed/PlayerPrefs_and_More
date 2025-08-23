using UnityEngine;

public class FOV_Script : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _raycastStartPosition;
    private RaycastHit hit;
    

    [Header("ScriptReference")]
    public AI_Controller _aiData;

    void Update()
    {
        // SendRaycast();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _aiData._isPlayerIn = true;
            _aiData._isFollowing = true;
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            _aiData._isPlayerIn = false;
            _aiData._isFollowing = false;
        }
    }

    #region  Functions

    void SendRaycast()
    {
        if(!_aiData._isPlayerIn){ return; }                   // Eğer karakter fov içerisinde değilse döngüyü kır.

        Ray ray = new Ray(_raycastStartPosition.position, transform.forward); // Bir ray oluştur

        if (Physics.Raycast(ray, out hit, _maxDistance))      // Ray doğrultusunda maxDistance uzunluğunda bir ışın gönder ve gelen bilgiyi hit değişkenine yaz.
        {
            if (hit.collider.CompareTag("Player"))            // Eğer çarptığı şeyin tagi player ise devam et
            {
                _aiData._alarmMode = true;                    // Alarm moda geç...
            }
        }
    }
    
    #endregion
}
