using System.Collections;
using UnityEngine;

public class Precious_Stuff : CollectableObject_BaseClass
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (playerScript._preciousStuff)
        {
            StartCoroutine(PreciousStuffFunction());
        }
    }

    public override void EnterFunction()
    {
        playerScript._preciousStuff = true;       // Değerli eşya değişkenini true çevir
        audioSource.PlayOneShot(EffectSound);     // Coin sesi çikart.
        PreciousStuffFunction();
    }

    IEnumerator PreciousStuffFunction()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(2f);      // 2 Saniye bekle.
        Destroy(gameObject);                      // Objeyi yok et.
    }
}
