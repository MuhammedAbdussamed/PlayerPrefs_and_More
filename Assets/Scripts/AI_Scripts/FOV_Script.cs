using UnityEngine;

public class FOV_Script : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private float _viewDistance;
    [SerializeField] private LayerMask _raycastLayers;
    
    [Header("ScriptReference")]
    [SerializeField] AI_Controller _aiData;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _aiData._isPlayerIn = true;
        }
    }

    void Update()
    {
        if (_aiData._isPlayerIn)
        {
            SendRaycast();
        }
        else
        {
            _aiData._isFollowing = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _aiData._isPlayerIn = false;
        }
    }

    #region  Functions


    void SendRaycast()
    {
        if (!_aiData._isPlayerIn) { return; }           // Eğer karakter collider içerisinde değilse döngüyü kır.

        Vector3 _direction = (_aiData._playerScript.transform.position - _aiData.transform.position).normalized;           // Bot'un pozisyonunda karakterin pozisyonuna bir vector3 çiz.

        if (Physics.Raycast(_aiData.transform.position, _direction, out RaycastHit _hit, _viewDistance, _raycastLayers))   // Vector3 doğrultusunda , maxDistance uzunluğunda bir ışın gönder ve gelen bilgiyi hit değişkenine yaz. 
        {                                                                                                                   // Eğer raycastLayerstan bir katmana çarparsa devam et...

            Debug.DrawRay(_aiData.transform.position, _direction * _viewDistance, Color.green);                            // Raycast'i yeşil renkte görselleştir.

            if (_hit.collider.CompareTag("Player"))                                                                             // Eğer çarptığı objenin tagi player ise devam et...
            {
                _aiData._isFollowing = true;                                                                                        // Takip modu true döndür.
            }
            else                                                                                                                // Eğer çarptığı objenin tag'i Player değilse devam et...
            {
                _aiData._isFollowing = false;                                                                                       // Takip modunu false döndür.
            }
        }
        else                                                                                                                // Eğer hiçbirşeye çarpmassa devam et...
        {
            _aiData._isFollowing = false;                                                                                       // Takip modunu false döndür.
        }
    }
    
    #endregion
}
