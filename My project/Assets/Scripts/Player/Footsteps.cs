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
    private string previousTag = null;

    private void Start()
    {
        steps = GetComponent<AudioSource>();
    }
    private void Update()
    {
        GroundDetector = new Ray(transform.position, transform.forward * rayLen);

        if (Physics.Raycast(GroundDetector, out RaycastHit hit, rayLen, ground))
        {
            if (hit.collider.CompareTag("Carpet")) steps.clip = clips[0];
            if (hit.collider.CompareTag("Wood")) steps.clip = clips[1];
            if (hit.collider.CompareTag("Stone")) steps.clip = clips[2];
            else
                Debug.Log(hit.collider.gameObject.name);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && characterController.isGrounded)
        {
            if(!hit.collider.CompareTag(previousTag))
                steps.enabled = false;

            steps.enabled = true;
        }
        else
        {
            steps.enabled = false;
        }
        previousTag = hit.collider.tag;
    }
}
