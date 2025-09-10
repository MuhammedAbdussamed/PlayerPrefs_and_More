using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : CollectableObject_BaseClass
{
    public override void EnterFunction()
    {
        playerScript._coin++;                     // Coini bir arttir
        audioSource.PlayOneShot(EffectSound);     // Coin sesi çikart.
        Destroy(gameObject);                      // Objeyi yok et.
    }
}
