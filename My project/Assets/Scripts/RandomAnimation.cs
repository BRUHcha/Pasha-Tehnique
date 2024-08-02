using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _transform = gameObject.transform;
    }

    public void RandomAnim()
    {
        int i = Random.Range(1, 4);

        if (i == 1)
        {
            _transform.position = new Vector3(30.15f, 1.69f, -3.10f);
            _animator.SetTrigger("Leaning");
            Debug.Log("Leaning");
        }

        if (i == 2)
        {
            _transform.position = new Vector3(33.47f, 1.69f, -1.534f);
            _animator.SetTrigger("Punching");
            Debug.Log("Punching");
        }
        if (i == 3)
        {
            _transform.position = new Vector3(32.31f, 1.69f, -0.51f);
            _animator.SetTrigger("Pristup");
            Debug.Log("Pristup");
        }
    }
    public void ResetAnim()
    {
        _animator.SetTrigger("Idle");
        Debug.Log("Idle");
    }
}
