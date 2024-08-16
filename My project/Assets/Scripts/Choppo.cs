using UnityEngine;

public class Choppo : Sounds, IClickable
{
    [SerializeField] private float straight;

    private Rigidbody rb;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    public void DoSomething(GameObject sender)
    {
        if (!_audioSource.isPlaying)
        {
            PlayRandomSound(0, 6);
            rb.AddForce(Vector3.up * straight);
            anim.SetTrigger("GroundJump");
        }
    }
}
