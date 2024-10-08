using UnityEngine;

public class Anvil : Sounds, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Other shit")]
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void DoSomething(GameObject sender)
    {
        var cam = sender.GetComponentInParent<FirstPersonController>().playerCamera; //had to do it
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
