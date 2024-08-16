using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeleportBlocker : MonoBehaviour
{
    [SerializeField] GameObject blockingParent;

    private List<GameObject> _stages;
    private int _stage;
    private MeshFilter _filter;
    private float _cooldown = 0.4f;
    private float _cd = 0f;


    private void Start()
    {
        _stages = blockingParent.GetComponentsInChildren<Transform>().Where(x=> x.gameObject != blockingParent).Select(x => x.gameObject).ToList();
        _stage = _stages.Count;
        _filter = GetComponent<MeshFilter>();
    }
    private void Update()
    {
        if(_cd > 0)
            _cd -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("HeavyObject") || _cd > 0)
            return;
        _stage--;
        UpdateToStage();
        React(collision);
        _cd = _cooldown;
    }

    private void UpdateToStage()
    {
        Destroy(_stages[0]);
        _stages.RemoveAt(0);
        if( _stage != 0 )    
        return;

        transform.parent.gameObject.layer = LayerMask.NameToLayer("Clickable");
        Destroy(gameObject);

    }
    private void React(Collision body)
    {


        body.rigidbody.AddForceAtPosition(gameObject.transform.forward * 150f, body.contacts[0].point);
    }
}
