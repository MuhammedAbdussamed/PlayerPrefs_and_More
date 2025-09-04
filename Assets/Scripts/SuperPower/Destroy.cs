using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy", menuName = "Super Powers/Destroy", order = 1)]
public class Destroy : Base_Class
{
    public override void ApplyRarityValues()
    {
        switch (_rarity)
        {
            case Rarity.Common:
                _durationTime = 5f;             // Rarity Common ise değerleri buna göre değiştir.
                break;

            case Rarity.Rare:
                _durationTime = 8f;
                break;

            case Rarity.Legendary:
                _durationTime = 11f;
                break;
        }
    }
}
