using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePrikol : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

}
