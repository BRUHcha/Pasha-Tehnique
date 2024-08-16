using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportSystem : MonoBehaviour, IClickable
{
    [SerializeField] private Transform _anotherTelepot;

    public void DoSomething(GameObject sender)
    {
        StartCoroutine(Teleport(sender));
    }

    private IEnumerator Teleport(GameObject player)
    {
        var mv = player.GetComponent<PlayerMovement>();
        mv.enabled = false;
        yield return new WaitForSeconds(0.05f);
        player.transform.position = _anotherTelepot.position;
        player.transform.rotation = _anotherTelepot.rotation;
        yield return new WaitForSeconds(0.05f);
        mv.enabled = true;

    }
}
