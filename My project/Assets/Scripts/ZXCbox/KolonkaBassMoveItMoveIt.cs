using UnityEngine;

public class KolonkaBassMoveItMoveIt : MonoBehaviour
{
    [SerializeField] private ZXCbox box;
    [SerializeField, Range(1.1f,2)] private float maxSize;

    private float[] samples = new float[256];
    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
    }

    private void Update()
    {
        if (!box.audio.isPlaying)
        {
            if(transform.localScale != defaultScale)
                transform.localScale = defaultScale;
            return;
        }

        box.audio.GetOutputData(samples,0);

        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        float rmsValue = Mathf.Sqrt(sum / samples.Length);
        if (rmsValue < 0.08) rmsValue = 0;

        Debug.Log(rmsValue);

        transform.localScale = Vector3.Lerp(defaultScale, defaultScale * maxSize, rmsValue);
    }
}
