using UnityEngine;

public class KeyScript : CollectableObject_BaseClass
{
    public override void EnterFunction()
    {
        playerScript.iskey = true;
        audioSource.PlayOneShot(EffectSound);
        Destroy(gameObject);
    }
}
