using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UI = UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    [Header("Script Reference")]
    private PlayerController _playerScript;
    [SerializeField] private Super_Power_Object _superObjectScript;

    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI _necesserrayCoinText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private TextMeshProUGUI _deathText;

    [Header("Image Reference")]
    [SerializeField] private UI.Image _superPowerImage;
    [SerializeField] private Animator _blackAnimator;
    [SerializeField] private UI.Image _deathLine;

    [Header("Image List")]
    [SerializeField] private List<Sprite> _superPowerImageList;

    void Start()
    {
        _playerScript = PlayerController.Instance;
        _superPowerImage.gameObject.SetActive(false);
    }

    void Update()
    {
        _coinText.text = _playerScript._coin.ToString();                                // Toplanan coin sayısını playerScriptte ki coin değişkenine eşitle
        _necesserrayCoinText.text = "/ " + _playerScript._necesserrayCoin.ToString();   // Gereken coin sayisini playerScriptte ki necesserrayCoin değişkenine eşitle

        ChangeTextColor();
        SuperPowerImage();
        SuperPowerDuration();
        OpenDeathScreen();
    }

    #region Functions

    void ChangeTextColor()
    {
        if (_playerScript._coin >= _playerScript._necesserrayCoin)      // Toplanan coin sayisi gereken coin sayisindan fazla ise devam et...
        {
            _coinText.color = Color.green;                                  //  Coin sayacını yeşil yap.
            _necesserrayCoinText.color = Color.green;                       //
        }
        else                                                           
        {   
            _coinText.color = Color.black;                          
            _necesserrayCoinText.color = Color.black;
        }
    }

    /*-------------------------------------------------------*/

    void SuperPowerImage()
    {
        if (_superObjectScript._useDestroy)                         // Destroy süper gücü açıksa devam et...
        {
            _superPowerImage.gameObject.SetActive(true);                // süper  güç resmini aktif et
            _superPowerImage.sprite = _superPowerImageList[0];          // süper güç resmini destroy resmi ile değiştir.
        }

        else if (_superObjectScript._useSpeedUp)                    // Speed up özel gücü açıksa devam et...
        {
            _superPowerImage.gameObject.SetActive(true);                // süper güç resmini aktif et.
            _superPowerImage.sprite = _superPowerImageList[1];          // süper güç resmini speed up resmi ile değiştir.
        }

        else if (_superObjectScript._useInvisible)                  // Görünmezlik özel gücü açıksa devam et...
        {   
            _superPowerImage.gameObject.SetActive(true);                // süper güç resmini aktif et.
            _superPowerImage.sprite = _superPowerImageList[2];          // süper güç resmini görünmezlik resmi ile değiştir.
        }

        else
        {
            _superPowerImage.gameObject.SetActive(false);           // Hiçbir süper güç aktif değilse süper güç resmini kapat.
        }
    }

    /*-------------------------------------------------------*/

    void SuperPowerDuration()
    {
        if (_superObjectScript._superPowerEndTime < 0f)             // Süper güç etki süresi 0 saniyeden az ise boş bırak
        {
            _durationText.text = "";
        }

        else
        {
            /* TextMeshProyu süper gücün etki süresine ayarla */
            _durationText.text = _superObjectScript._superPowerEndTime.ToString("F1");    // F1 virgülden sonra tek bir ondalik sayi olmasini sağlar. (4,9 gibi)
        }
    }

    void OpenDeathScreen()
    {
        if (_playerScript._isDeath)
        {
            _deathLine.gameObject.SetActive(true);
            _deathText.gameObject.SetActive(true);
        }
        
        
    }

    #endregion
}
