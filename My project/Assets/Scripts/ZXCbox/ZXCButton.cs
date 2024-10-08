using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZXCButton : Button
{
    [SerializeField] UnityEvent startEvent;

    override protected void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void OnPressed()
    {
        startEvent?.Invoke();
    }

}
