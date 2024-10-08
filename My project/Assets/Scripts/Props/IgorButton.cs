using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgorButton : Button
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource igor;


    override protected void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void OnPressed()
    {
        if (!igor.isPlaying)
            igor.PlayOneShot(clips[1]);
    }
}
