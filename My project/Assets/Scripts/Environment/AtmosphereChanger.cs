using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereChanger : MonoBehaviour
{
    //can't just save em all at once...
    [SerializeField] float fogDensity;
    [SerializeField] FogMode fogMode;
    [SerializeField] Color fogColor;

    private float _previousFogDensity;
    private FogMode _previousFogMode;
    private bool _wasFogEnabled;
    private Color _previousFogColor;

    private void OnTriggerEnter(Collider other)
    {
        _wasFogEnabled = RenderSettings.fog; 
        _previousFogMode = RenderSettings.fogMode;
        _previousFogDensity = RenderSettings.fogDensity;
        _previousFogColor = RenderSettings.fogColor;

        RenderSettings.fog = true;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogColor = fogColor;
    }

    private void OnTriggerExit(Collider other)
    {
        RenderSettings.fog = _wasFogEnabled;
        RenderSettings.fogMode = _previousFogMode;
        RenderSettings.fogDensity = _previousFogDensity;
        RenderSettings.fogColor= _previousFogColor;
    }

}
