using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyriagaDoor : MonoBehaviour, IClickable
{
    [SerializeField] private Transform _anotherTelepot;
    [SerializeField] private GameObject _player;
    [SerializeField] private RandomAnimation _randomAnim;

    [SerializeField] private bool _resetAnim;

    private PlayerMovement _playerMovement;
    private void Start()
    {
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }
    public void DoSomething()
    {
        if (_resetAnim)
            _randomAnim.ResetAnim();
        if (!_resetAnim)
            _randomAnim.RandomAnim();

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
