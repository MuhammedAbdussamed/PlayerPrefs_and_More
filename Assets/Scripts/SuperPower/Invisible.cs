using UnityEngine;

[CreateAssetMenu(fileName = "Invisible", menuName = "Super Powers/Invisible", order = 1)]
public class Invisible : Base_Class
{
    public override void ApplyRarityValues()
    {         
        switch (_rarity)
        {
            case Rarity.Common:               
                _durationTime = 5f;             // Rarity Common ise değerleri buna göre değiştir.
                break;

            case Rarity.Rare:                               
                _durationTime = 7.5f;             
                break;

            case Rarity.Legendary:                  
                _durationTime = 10f;             
                break;
        }
    }
}
