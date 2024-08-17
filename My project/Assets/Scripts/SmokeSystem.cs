using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SmokeSystem : Sounds
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private PostProcessProfile postFX;

    [SerializeField, Range(1,100)] private int peredozChance;
    [SerializeField] private float peredozEffectSpeed;

    [Header("Post Processing kefteme")]

    [SerializeField, Range(-100, 100)] private int maxLens;
    [SerializeField, Range(0, 1)] private float maxVignette;
    [SerializeField, Range(0, 1)] private float maxChromatic;

    private LensDistortion _lens;
    private Vignette _vignette;
    private ChromaticAberration _chromatic;

    private void Start()
    {
        postFX.TryGetSettings(out _lens);
        postFX.TryGetSettings(out _vignette);
        postFX.TryGetSettings(out _chromatic);
    }
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
        while (_lens.intensity > maxLens && _vignette.intensity < maxVignette && _chromatic.intensity < maxChromatic)
        {
            _lens.intensity.value -= peredozEffectSpeed * 150 * Time.deltaTime;
            _vignette.intensity.value += peredozEffectSpeed * Time.deltaTime;
            _chromatic.intensity.value += peredozEffectSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        while (_lens.intensity <= 0 || _vignette.intensity >= 0 || _chromatic.intensity >= 0)
        {
            _lens.intensity.value += peredozEffectSpeed * 150 * 0.2f * Time.deltaTime;
            _vignette.intensity.value -= peredozEffectSpeed * 0.2f * Time.deltaTime;
            _chromatic.intensity.value -= peredozEffectSpeed * 0.2f * Time.deltaTime;
            yield return null;
        }
    }
}
