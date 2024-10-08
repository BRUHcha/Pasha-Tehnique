using UnityEngine;
using UnityEngine.UI;

public class RayP : MonoBehaviour
{
    [SerializeField] private float _clickDistance;
    [SerializeField] private GameObject _textObj;

    private Ray playerRay;
    private Text _text;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _text = _textObj.GetComponent<Text>();
        gameObject.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    void Update()
    {
        RaycastHit hit;
        playerRay = new Ray(transform.position, transform.forward * _clickDistance);
        Debug.DrawRay(playerRay.origin, playerRay.direction * _clickDistance,Color.red);

        if (Physics.Raycast(playerRay, out hit, _clickDistance))
        {
            if (hit.collider.gameObject.TryGetComponent<IClickable>(out IClickable clickable))
            {
                _text.text = clickable.InteractionMessage;
                if(Input.GetKeyDown(KeyCode.E))
                {
                    clickable.DoSomething(gameObject.transform.parent.gameObject);
                }
            }
            else
                _text.text = "";

            if (hit.collider.gameObject.TryGetComponent<IScrollable>(out IScrollable scrollable))
            {
                //scroll power 0.1
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                    scrollable.ScrollWheel(scroll);
            }
        }
        else
            _text.text = "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        RaycastHit hit;
        if(Physics.Raycast(playerRay, out hit, _clickDistance))
        {
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
}
