using TMPro;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    [Header("Text Referans")]
    [SerializeField] private TextMeshProUGUI _necesserrayCoinText;
    [SerializeField] private TextMeshProUGUI _coinText;

    [Header("Script Reference")]
    private PlayerController _playerScript;

    void Start()
    {
        _playerScript = PlayerController.Instance;
    }

    void Update()
    {
        _coinText.text = _playerScript._coin.ToString();
        _necesserrayCoinText.text = "/ " + _playerScript._necesserrayCoin.ToString();

        ChangeTextColor();
    }

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
}
