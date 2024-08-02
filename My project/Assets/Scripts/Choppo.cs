using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Choppo : Sounds, IClickable
{
    [SerializeField] private float straight;

    private Rigidbody rb;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    public void DoSomething()
    {
        if (!_audioSource.isPlaying)
        {
            PlayRandomSound(0, 6);
            rb.AddForce(Vector3.up * straight);
            anim.SetTrigger("GroundJump");
        }
    }
}
