using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] public AudioClip[] clips;

    public AudioSource _audioSource => GetComponent<AudioSource>();

    public void PlayRandomSound(int min = 0, int max = 0, float volume = 0.5f)
    {
        int i = Random.Range(min,max);
        _audioSource.PlayOneShot(clips[i], volume);
    }
    public void PlaySound(int num, float volume = 0.5f)
    {
        _audioSource.PlayOneShot(clips[num], volume);
    }
}
