using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyriagaDoor : MonoBehaviour, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Other shit")]
    [SerializeField] private Transform _anotherTelepot;
    [SerializeField] private RandomAnimation _randomAnim;

    [SerializeField] private bool _resetAnim;

    private Transform player;

    private void Start()
    {

    }
    public void DoSomething(GameObject sender)
    {
        if (_resetAnim)
            _randomAnim.ResetAnim();
        if (!_resetAnim)
            _randomAnim.RandomAnim();

        player = sender.transform.parent;

        player.transform.position = _anotherTelepot.position;
        player.transform.rotation = _anotherTelepot.rotation;
    }
}
