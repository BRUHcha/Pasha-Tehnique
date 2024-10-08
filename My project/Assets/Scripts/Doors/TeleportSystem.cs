using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportSystem : MonoBehaviour, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Other shit")]
    [SerializeField] private Transform _anotherTelepot;

    private Transform player;

    public void DoSomething(GameObject sender)
    {
        player = sender.transform.parent;
        player.position = _anotherTelepot.position;
        player.rotation = _anotherTelepot.rotation;
    }
}
