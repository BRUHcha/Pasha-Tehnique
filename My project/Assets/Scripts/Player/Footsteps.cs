using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private float rayLen;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float stepInterval = 0.7f;
    [SerializeField] private List<AudioClip> _footstepSounds;

    private AudioSource _footstepsAudioSource;
    private Ray GroundDetector;
    private TerrainChecker _checker;
    private string _currentlayer;
    private float _stepCounter;

    public FootStepCollection[] FootstepCollections;

    private void Start()
    {
        _footstepsAudioSource = GetComponent<AudioSource>();
        _checker = new TerrainChecker();
    }
    private void Update()
    {
        _stepCounter += Time.deltaTime;
        GroundDetector = new Ray(transform.position, transform.forward * rayLen);
        if (Physics.Raycast(GroundDetector, out RaycastHit hit, rayLen, ground))
        {
            if (hit.transform.GetComponent<Terrain>() != null)
            {
                Terrain t = hit.transform.GetComponent<Terrain>();
                if(_currentlayer != _checker.GetLayerName(transform.position, t))
                {
                    _currentlayer = _checker.GetLayerName(transform.position, t);
                    SwapFootstepSounds();
                }
            }
            else
            {
                if(!hit.transform.CompareTag(_currentlayer))
                {
                    _currentlayer = hit.transform.tag;
                    SwapFootstepSounds();
                }
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (_stepCounter >= stepInterval)
            {
                PlayFootstepSound();
                _stepCounter = 0;
            }
        }

    }

    private void PlayFootstepSound()
    {
        if (!playerController.isGrounded) return;

        int n = UnityEngine.Random.Range(1, _footstepSounds.Count());
        _footstepsAudioSource.clip = _footstepSounds[n];
        _footstepsAudioSource.PlayOneShot(_footstepsAudioSource.clip);

        _footstepSounds[n] = _footstepSounds[0];
        _footstepSounds[0] = _footstepsAudioSource.clip;
    }

    private void SwapFootstepSounds()
    {
        foreach (FootStepCollection collection in FootstepCollections)
        {
            if (_currentlayer != collection.name) 
                continue;
            
            _footstepSounds.Clear();

            for (int i = 0; i < collection.footstepSounds.Count; i++)
            {
                _footstepSounds.Add(collection.footstepSounds[i]);
            }
        }
    }
}
