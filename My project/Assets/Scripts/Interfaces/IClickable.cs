using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IClickable
{
    void DoSomething(GameObject sender);
    string InteractionMessage {  get; }
}

