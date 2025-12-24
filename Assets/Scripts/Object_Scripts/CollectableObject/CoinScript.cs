using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : CollectableObject_BaseClass
{
    public override void EnterFunction()
    {
        playerScript._coin++;                                            // Coini bir arttir
        AudioSource.PlayClipAtPoint(EffectSound,transform.position);     // Coin sesi çikart.
        Destroy(gameObject);                                             // Objeyi yok et.
    }
}
