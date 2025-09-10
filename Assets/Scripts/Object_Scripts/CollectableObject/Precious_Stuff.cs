using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Precious_Stuff : CollectableObject_BaseClass
{
    [SerializeField] private float alarmDelay;
    [SerializeField] private UnityEvent alarm;
    [SerializeField] HalfDead_AI[] halfDeadBots;
    private MeshRenderer meshRenderer;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        meshRenderer = GetComponent<MeshRenderer>();
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
        yield return new WaitForSeconds(2f);            // 2 Saniye bekle.
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(alarmDelay);    // Alarm süresi kadar bekle
        alarm?.Invoke();
    }

    public void Alarm()
    {
        for (int i = 0; i < halfDeadBots.Length; i++)
        {
            halfDeadBots[i].Revive();
        }
    }
}
