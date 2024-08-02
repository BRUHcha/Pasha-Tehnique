using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Footsteps : Sounds
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float rayLen;
    [SerializeField] private LayerMask ground;

    private AudioSource steps;
    private Ray GroundDetector;

    private void Start()
    {
        steps = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && characterController.isGrounded)
        {
            RaycastHit hit;
            GroundDetector = new Ray(transform.position, transform.forward * rayLen);

            if (Physics.Raycast(GroundDetector, out hit, rayLen, ground))
            {
                if (hit.collider.tag == "Carpet") steps.clip = clips[0];
                if (hit.collider.tag == "Wood") steps.clip = clips[1];
            }

            steps.enabled = true;
        }
        else
        {
            steps.enabled = false;
        }
    }
}
