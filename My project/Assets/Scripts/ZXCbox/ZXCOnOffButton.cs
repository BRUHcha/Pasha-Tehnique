using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZXCOnOffButton : Button, IScrollable
{
    [SerializeField] private ZXCbox box;
    [SerializeField] UnityEvent clickEvent;

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
        clickEvent?.Invoke();
    }

    public void ScrollWheel(float scroll)
    {
        box.ChangeVolume(scroll / 10);
    }
}
