using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupSystem : MonoBehaviour, IClickable
{
    [SerializeField] private Animator anim;
    [SerializeField] private SmokeSystem script;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private Transform ItemContainer, player, plCam;
    [SerializeField] private float dropForwardForce, dropUpwardForce;
    [SerializeField] private bool equipped;
    public static bool slotFull;

    public void DoSomething()
    {
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
            script.enabled = false;
            anim.enabled = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            script.enabled = true;
            anim.enabled = true;
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

        script.enabled = true;
        anim.enabled = true;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        script.enabled = false;
        anim.enabled = false;

        //rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //rb.AddForce(plCam.forward * dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(plCam.up * dropUpwardForce, ForceMode.Impulse);

        // float rand = Random.Range(-2f, 2f);
        //rb.AddTorque(new Vector3(rand, rand, rand));

    }
}
