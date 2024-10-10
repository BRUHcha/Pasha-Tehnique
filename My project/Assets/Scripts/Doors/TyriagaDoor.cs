using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Unused = 0,
    Locked = 1,
    Unlocked = 2
}

public class TyriagaDoor : MonoBehaviour, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Other shit")]
    [SerializeField] private CodeStuff code;
    [SerializeField] private Transform _anotherTelepot;
    [SerializeField] private RandomAnimation _randomAnim;

    [SerializeField] private bool _resetAnim;

    private DoorState _doorState;
    private Transform player;

    private void Start()
    {
        if(_resetAnim)
            _doorState = DoorState.Unlocked;
    }

    private void Update()
    {
        if (code.Unlocked && _doorState != DoorState.Unlocked)
        {
            _doorState = DoorState.Unlocked;
            InteractMessage = "Похуй заходи";
        }
    }

    public void DoSomething(GameObject sender)
    {
        
        if(_doorState == DoorState.Unused)
        {
            InteractMessage = "Хуй то там ты зайдешь";
            _doorState = DoorState.Locked;
        }


        if(_doorState == DoorState.Unlocked)
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
}
