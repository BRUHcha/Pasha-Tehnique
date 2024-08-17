using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgorButton : Button
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource igor;

    private AudioSource _buttonSound;

    override protected void Start()
    {
        base.Start();
        _buttonSound = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void OnPressed()
    {
        _buttonSound.PlayOneShot(clips[0]);
        if (!igor.isPlaying)
            igor.PlayOneShot(clips[1]);
    }
}
