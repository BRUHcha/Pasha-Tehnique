using System.Linq;
using UnityEngine;

public class WhaleAnimator : MonoBehaviour
{
    [Header("Animation Points")]
    [SerializeField] GameObject tailTip;
    [SerializeField] GameObject headPoint;
    [Header("Tail Swing Values")]
    [SerializeField] float tailSwingAngle;
    [SerializeField] float timeToTailSwing;
    [Header("Arc Following Values")]
    [SerializeField] float predictionDistance;
    [SerializeField] float angleCoefficient;

    private float _tailSwingTime = 0;
    private int _tailDirectionCoefficient = 1;
    private GameObject _armature;
    private WaypointNavigator _waypointNavigator;
    private float _whaleLength;

    private void Start()
    {
        //lol
        tailSwingAngle /= 2;
        _armature = gameObject.transform.GetChild(0)!.gameObject;
        _waypointNavigator = GetComponent<WaypointNavigator>();
        _whaleLength = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.x * 1.5f; 

    }

    private void Update()
    {
        SwingTail();
        Align();
    }

    //right is forward and forward is right.
    //god I love when people export their blender models randomly
    private void SwingTail()
    {
        _tailSwingTime += _tailDirectionCoefficient * Time.deltaTime;
        if (_tailSwingTime >= timeToTailSwing || _tailSwingTime <= 0)
            _tailDirectionCoefficient *= -1;
        tailTip.transform.rotation = Quaternion.Lerp(
            Quaternion.AngleAxis(tailSwingAngle, _armature.transform.forward), Quaternion.AngleAxis( -1 * tailSwingAngle, _armature.transform.forward),
            (Mathf.Cos(2*(_tailSwingTime / timeToTailSwing) * Mathf.PI) + 1)/2) * _armature.transform.rotation;

        //Debug.DrawLine(transform.position + _armature.transform.forward, transform.position, Color.red, 0.1f, false);
    }

    private void Align()
    {
        int i = _waypointNavigator.CurrentArcIndex, futureI = _waypointNavigator.CurrentArcIndex;
        float t = _waypointNavigator.CoveredDistance / _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i),
            futureT = (_waypointNavigator.CoveredDistance + predictionDistance) / _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i),
            tailT = t - _whaleLength / _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i);


        if(futureT > 1)
        {
            futureI = _waypointNavigator.SplineBuilder.Wrap(i + 1);
            var temp = _waypointNavigator.CoveredDistance + predictionDistance - _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i);
            futureT = temp / _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(futureI);
        }
        
        if (tailT < 0)
        {
            var temp = _whaleLength - _waypointNavigator.CoveredDistance;
            i = _waypointNavigator.SplineBuilder.Wrap(i-1);
            tailT =(_waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i) - temp) / _waypointNavigator.SplineBuilder.ArcLenght.ElementAt(i);
        }


        var tailPositon = _waypointNavigator.SplineBuilder.CalculatePositionOnSpline(i, tailT);
        var futureHeadPostion = _waypointNavigator.SplineBuilder.CalculatePositionOnSpline(futureI, futureT);
        
        float predictedAngle = -Vector3.Angle(futureHeadPostion - tailPositon, transform.position - tailPositon);

        Debug.DrawLine(tailPositon, transform.position, Color.magenta);
        Debug.DrawLine(futureHeadPostion, tailPositon, Color.cyan);
        Debug.DrawLine(.5f * (transform.position + futureHeadPostion), tailPositon, Color.green);

        gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, (tailPositon - .5f*(transform.position + futureHeadPostion)));

        transform.Rotate(transform.position-tailPositon, angleCoefficient * predictedAngle, Space.World);
    }
}
