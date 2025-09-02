using UnityEngine;

[CreateAssetMenu(fileName = "Speed_Up", menuName = "Super Powers/Speed_Up", order = 1)]
public class Speed_Up : Base_Class
{
    public override void ApplyRarityValues()    
    {
        switch (_rarity)
        {
            case Rarity.Common:                 //
                _speedUpValue = 1.25f;          // Rarity Common ise değerleri buna göre değiştir.
                _durationTime = 3f;             //
                break;

            case Rarity.Rare:                   //
                _speedUpValue = 2.5f;           // Rarity Rare ise değerleri buna göre değiştir.
                _durationTime = 6f;             //
                break;

            case Rarity.Legendary:              //
                _speedUpValue = 4f;             // Rarity Legendary ise değerleri buna göre değiştir.
                _durationTime = 9f;             //
                break;
        }
    }
}
