using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SmokeSystem : Sounds
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem smokePrefab;

    private Transform smokeSpawnPos;
    private ParticleSystem newSmoke;

    private void OnEnable()
    {
        anim.enabled = true;
        smokeSpawnPos = transform.parent.parent;
        Debug.Log(smokeSpawnPos.name);
        if(smokeSpawnPos != null && smokePrefab != null)
        {
            newSmoke = Instantiate(smokePrefab, smokeSpawnPos);
        }
    }

    private void OnDisable()
    {
        anim.Play("idle");
        _audioSource.Stop();
        anim.enabled= false;
        if(newSmoke != null)
        {
            Destroy(newSmoke.gameObject, 10f);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Smoke") && !_audioSource.isPlaying)
        {
            anim.SetBool("Smoke",true);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)) anim.SetBool("Smoke", false);
    }

    private void tyaga()
    {
        PlayRandomSound(0, 3);
    }
    
    private void blow()
    {
        PlayRandomSound(3, 6);
        newSmoke.Play();
    }

}
