using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] GameObject waypointPath;
    [Header("Parameters")]
    [SerializeField] float speed;


    private float _coveredDistance;
    private CatmullRomSplineBuilder _splineBuilder;
    private int _i = 0;

    public CatmullRomSplineBuilder SplineBuilder { get => _splineBuilder; }
    public float CoveredDistance { get => _coveredDistance; }
    public int CurrentArcIndex { get => _splineBuilder.Wrap(_i); }

    void Start()
    {
        _splineBuilder = new(waypointPath.GetComponentsInChildren<Transform>().Where(x => x != waypointPath.transform).Select(x=> x.position).ToList());
    }

    void Update()
    {
        _coveredDistance += Time.deltaTime * speed;
        if (_coveredDistance >= _splineBuilder.ArcLenght.ElementAt(_splineBuilder.Wrap(_i)))
        {
            _coveredDistance -= _splineBuilder.ArcLenght.ElementAt(_splineBuilder.Wrap(_i));
            _i++;
        }
        float t = _coveredDistance / _splineBuilder.ArcLenght.ElementAt(_splineBuilder.Wrap(_i));

        transform.position = _splineBuilder.CalculatePositionOnSpline(_i, t);

    }
}
