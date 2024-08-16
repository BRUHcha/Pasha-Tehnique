using UnityEngine;

public class Anvil : Sounds, IClickable
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void DoSomething(GameObject sender)
    {
        rb.AddForce(transform.position - sender.transform.position + Vector3.up * 200);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayAnyRandomSound();
    }
}
