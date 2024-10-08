using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class ZXCbox : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private new AudioSource audio;

    private int currentClip = 0;
    private new Light light;
    private bool isEnabled = false;
    private bool isPaused = false;

    private void Start()
    {
        light = transform.GetChild(6).GetComponent<Light>();
    }

    public void NextClip()
    {
        if(isEnabled)
        {
            audio.Stop();
            currentClip = (currentClip + 1) % clips.Length;
            Debug.Log(currentClip);
            audio.PlayOneShot(clips[currentClip]);
        }
    }

    public void PreviousClip()
    {
        if(isEnabled)
        {
            audio.Stop();
            if(currentClip == 0) 
                currentClip = clips.Length - 1;
            else
                currentClip--;
            Debug.Log(currentClip);
            audio.PlayOneShot(clips[currentClip]);
        }
    }

    public void Pause()
    {
        if (isPaused)
            audio.UnPause();
        else
            audio.Pause();
        isPaused = !isPaused;
    }

    public void OnOffBox()
    {
        if(isEnabled)
        {
            audio.Stop();
            isEnabled = !isEnabled;
            light.enabled = false;
        }
        else
        {
            audio.PlayOneShot(clips[currentClip]);
            isEnabled = !isEnabled;
            light.enabled = true;
        }
    }

    public void ChangeVolume(float scroll)
    {
        if (isEnabled)
            audio.volume += scroll;
    }
}
