using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatmullRomSplineBuilder
{
    private readonly List<Vector3> _points;
    private List<float> _arcLengths;

    public IEnumerable<float> ArcLenght { get => _arcLengths; }

    public CatmullRomSplineBuilder(List<Vector3> points)
    {
        _points = points;
        if (_points.Count < 4)
            throw new System.Exception("Amount of Points for spline Building was lower than 4");
        ComputeLengths();
    }

    public Vector3 CalculatePositionOnSpline(int i, float t)
    {
        return 0.5f * (
            2f * _points[Wrap(i)] +
            (-_points[Wrap(i - 1)] + _points[Wrap(i+1)]) * t +
            t * t * (2f * _points[Wrap(i - 1)] - 5f * _points[Wrap(i)] + 4f * _points[Wrap(i + 1)] - _points[Wrap(i + 2)]) +
            t * t * t * (-_points[Wrap(i - 1)] + 3f * _points[Wrap(i)] - 3f * _points[Wrap(i + 1)] + _points[Wrap(i + 2)]));
    }

    private void ComputeLengths()
    {
        _arcLengths = new();
        const int samples = 20;
        for(int i = 0; i < _points.Count; i++)
        {
            Vector3 previousPoint = _points[i];
            
            float segmentLength = 0;
            for(int t = 1; t < samples; t++)
            {
                var currentPoint = CalculatePositionOnSpline(i, (float)t/samples);
                segmentLength += Vector3.Distance(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
            _arcLengths.Add(segmentLength);
        }
    }

    //had to make it public, bruh
    public int Wrap(int i)
    {
        int range = _points.Count;
        if (i < 0)
        {
            i += range * (-i / range + 1);
        }
        return i % range;
    }


}
