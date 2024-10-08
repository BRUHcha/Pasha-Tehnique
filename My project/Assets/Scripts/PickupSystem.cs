using UnityEngine;

public class PickupSystem : MonoBehaviour, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Other shit")]
    [SerializeField] private MonoBehaviour itemScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private Transform ItemContainer, player, plCam;
    [SerializeField] private float dropForwardForce, dropUpwardForce;
    [SerializeField] private bool equipped;
    public static bool slotFull;

    public void DoSomething(GameObject sender)
    {
        player = sender.transform.parent;
        plCam = sender.transform.GetChild(0);
        ItemContainer = sender.transform.GetChild(0).GetChild(0).transform;
        if (!equipped && !slotFull) PickUp();
    }

    private void Update()
    {
        if (equipped && Input.GetKeyDown(KeyCode.F)) Drop();
    }
    private void Start()
    {
        if(!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
            itemScript.enabled = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            itemScript.enabled = true;
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(ItemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        itemScript.enabled = true;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        itemScript.enabled = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(plCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(plCam.up * dropUpwardForce, ForceMode.Impulse);

        float rand = Random.Range(-2f, 2f);
        rb.AddTorque(new Vector3(rand, rand, rand));

    }
}
