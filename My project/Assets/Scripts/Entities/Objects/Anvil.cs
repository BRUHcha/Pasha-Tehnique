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
        var cam = sender.GetComponent<PlayerMovement>().PlayerCamera; //had to do it
        RaycastHit hit;
        var playerRay = new Ray(cam.transform.position, cam.transform.forward);
        Physics.Raycast(playerRay, out hit);
        rb.AddForceAtPosition(200f * (transform.position - sender.transform.position) + Vector3.up * 300, hit.point);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayAnyRandomSound();
    }
}
