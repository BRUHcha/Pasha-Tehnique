using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using PSXShaderKit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RayP : MonoBehaviour
{
    [SerializeField] private float _clickDistance;
    [SerializeField] private GameObject _textObj;

    private Ray playerRay;
    private Text _text;

    private void Start()
    {
        _text = _textObj.GetComponent<Text>(); 
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
                _text.text = "Сука только попробуй нажать [E]";
                if(Input.GetKeyDown(KeyCode.E))
                {
                    clickable.DoSomething();
                }
            }

            else
                _text.text = "";
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
