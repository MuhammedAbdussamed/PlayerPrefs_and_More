using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
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

    [Header("Image Reference")]
    [SerializeField] private UI.Image _superPowerImage;

    [Header("Image List")]
    [SerializeField] private List<Sprite> _superPowerImageList;

    [Header("Bools")]
    private bool _isCounting;

    void Start()
    {
        _playerScript = PlayerController.Instance;
        _superPowerImage.gameObject.SetActive(false);
    }

    void Update()
    {
        _coinText.text = _playerScript._coin.ToString();
        _necesserrayCoinText.text = "/ " + _playerScript._necesserrayCoin.ToString();

        ChangeTextColor();
        SuperPowerImage();
        SuperPowerDuration();
    }

    #region Functions

    void ChangeTextColor()
    {
        if (_playerScript._coin >= _playerScript._necesserrayCoin)
        {
            _coinText.color = Color.green;
            _necesserrayCoinText.color = Color.green;
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
        if (_superObjectScript._useDestroy)
        {
            _superPowerImage.gameObject.SetActive(true);
            _superPowerImage.sprite = _superPowerImageList[0];
        }

        else if (_superObjectScript._useSpeedUp)
        {
            _superPowerImage.gameObject.SetActive(true);
            _superPowerImage.sprite = _superPowerImageList[1];
        }

        else if (_superObjectScript._useInvisible)
        {
            _superPowerImage.gameObject.SetActive(true);
            _superPowerImage.sprite = _superPowerImageList[2];
        }

        else
        {
            _superPowerImage.gameObject.SetActive(false);
        }
    }

    /*-------------------------------------------------------*/

    void SuperPowerDuration()
    {
        if (_superObjectScript._superPowerEndTime < 0f)
        {
            _isCounting = false;
            _durationText.text = "";
        }

        else
        {
            _durationText.text = _superObjectScript._superPowerEndTime.ToString("F1");    // F1 virgülden sonra tekbir ondalik sayi olmasini sağlar. (4,9 gibi)
        }
    }

    #endregion
}
