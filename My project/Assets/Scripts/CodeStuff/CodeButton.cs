using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeButton : Button
{
    [SerializeField] CodeStuff codeStuff;

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
        codeStuff.numPressed(transform.name);
    }


}
