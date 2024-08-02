using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSystem : Sounds
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particle;

    [SerializeField, Range(1,100)] private int peredozChance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Smoke") && !_audioSource.isPlaying)
        {
            anim.SetBool("Smoke",true);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)) anim.SetBool("Smoke", false);

        if (Input.GetKeyDown(KeyCode.F)) anim.SetTrigger("Droped");
    }

    private void tyaga()
    {
        PlayRandomSound(0, 3);
    }
    
    private void blow()
    {
        PlayRandomSound(3, 6);
        particle.Play();
    }

    private void peredoz()
    {
        int rand = Random.Range(1, 102 - peredozChance);

        if (rand == 1)
        {
            StartCoroutine(PeredozDelay());
        }
    }

    private IEnumerator PeredozDelay()
    {
        yield return new WaitForSeconds(1f);
        PlaySound(6);
    }
}
