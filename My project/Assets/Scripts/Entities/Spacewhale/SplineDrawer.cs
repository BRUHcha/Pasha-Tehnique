using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SplineDrawer : MonoBehaviour
{
    private CatmullRomSplineBuilder _splineBuilder;
    private List<Vector3> _points;

    private void Update()
    {   
        _points = GetComponentsInChildren<Transform>().Where(x => x != transform).Select(x => x.position).ToList();
        _splineBuilder = new(_points);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        const int samples = 20;
        for (int i = 0; i < _points.Count; i++)
        {
            Vector3 previousPoint = _points[i];

            for (int t = 1; t < samples; t++)
            {
                var currentPoint = _splineBuilder.CalculatePositionOnSpline(i, (float)t / samples);
                Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }
        }
    }
}
