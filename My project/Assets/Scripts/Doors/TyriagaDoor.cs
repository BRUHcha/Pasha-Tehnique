using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyriagaDoor : MonoBehaviour, IClickable
{
    [SerializeField] private Transform _anotherTelepot;
    [SerializeField] private RandomAnimation _randomAnim;

    [SerializeField] private bool _resetAnim;

    private void Start()
    {

    }
    public void DoSomething(GameObject sender)
    {
        if (_resetAnim)
            _randomAnim.ResetAnim();
        if (!_resetAnim)
            _randomAnim.RandomAnim();

        StartCoroutine(Teleport(sender.GetComponent<PlayerMovement>()));
    }
    private IEnumerator Teleport(PlayerMovement playerMovement)
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(0.05f);
        playerMovement.gameObject.transform.position = _anotherTelepot.position;
        playerMovement.gameObject.transform.rotation = _anotherTelepot.rotation;
        yield return new WaitForSeconds(0.05f);
        playerMovement.enabled = true;
    }
}
