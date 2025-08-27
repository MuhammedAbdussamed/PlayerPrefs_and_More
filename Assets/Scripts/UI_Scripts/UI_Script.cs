using TMPro;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    private PlayerController _playerScript;

    void Start()
    {
        _playerScript = PlayerController.Instance;
    }

    void Update()
    {
        _coinText.text = _playerScript._coin.ToString();
    }
}
