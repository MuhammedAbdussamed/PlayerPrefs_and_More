using UnityEngine;

[CreateAssetMenu(fileName = "Invisible", menuName = "Super Powers/Invisible", order = 1)]
public class Invisible : Base_Class
{
    public override void ApplyRarityValues()
    {         
        switch (_rarity)
        {
            case Rarity.Common:               
                _durationTime = 5f;             
                break;

            case Rarity.Rare:                               
                _durationTime = 8f;             
                break;

            case Rarity.Legendary:                  
                _durationTime = 12f;             
                break;
        }
    }
}
