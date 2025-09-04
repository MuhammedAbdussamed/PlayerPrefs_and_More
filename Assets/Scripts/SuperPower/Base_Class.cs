using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Class : ScriptableObject
{
    [Header("Scripts")]
    public PlayerController _playerScript;

    [Header("Properties")]
    public string _speedPowerName;          // Super Güç ismi
    public float _durationTime;             // Etki süresi
    public float _speedUpValue;             // Karakterin ne kadar hızlanacağı.
    public Rarity _rarity;                  // Nadirlik seviyesi
    
    public enum Rarity                      // Enum belirli string değerlerini index yardimi ile intager değeri olarak tutar. Böylece biz yazilara bakip kolayca atama vs yapabiliriz.
    {
        Common, Rare, Legendary
    }

    public abstract void ApplyRarityValues();

    public void RandomizeRarity()
    {
        int randomNumber = Random.Range(0, 10);

        if (randomNumber < 5)           // 5 ihtimal. %50
        {
            _rarity = Rarity.Common;
        }

        else if (randomNumber < 8)      // 3 ihtimal. %30
        {
            _rarity = Rarity.Rare;
        }

        else                            // 2 ihtimal. %20
        {
            _rarity = Rarity.Legendary;
        }

        ApplyRarityValues();
    }
}
