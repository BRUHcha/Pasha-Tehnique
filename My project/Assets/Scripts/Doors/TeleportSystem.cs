using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportSystem : MonoBehaviour, IClickable
{
    [SerializeField] private Transform _anotherTelepot;
    [SerializeField] private GameObject _player;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }


    public void DoSomething()
    {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        _playerMovement.enabled = false;
        yield return new WaitForSeconds(0.05f);
        _player.transform.position = _anotherTelepot.position;
        _player.transform.rotation = _anotherTelepot.rotation;
        yield return new WaitForSeconds(0.05f);
        _playerMovement.enabled = true;
    }
}
