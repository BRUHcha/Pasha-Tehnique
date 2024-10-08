using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public enum ButtonState
{
    Static = 0,
    In = 1,
    Out = 2
}

[RequireComponent(typeof(AudioSource))]
public abstract class Button : MonoBehaviour, IClickable
{
    [Header("InteractionMessage")]
    [SerializeField] private string InteractMessage;
    public string InteractionMessage => InteractMessage;

    [Header("Animation settings")]
    [SerializeField] private float pressSpeed = 10f;
    [SerializeField] private float pressDeep = 0.05f;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip pressInSound;
    [SerializeField] private AudioClip pressOutSound;
    [SerializeField] private float volume = 0.5f;

    [Header("Delay Settings")]
    [SerializeField] private bool useDelay;
    [SerializeField] private float delayTime;

    private AudioSource _buttonAudioSource;
    private ButtonState _buttonState;
    private float _currentPos, _target;
    private Vector3 _startPos;
    private Vector3 _goalPos;
    private float _timer;

    protected virtual void Start()
    {
        _buttonAudioSource = GetComponent<AudioSource>();
        _buttonAudioSource.volume = volume;

        if (!pressInSound)
            pressInSound = Resources.Load<AudioClip>("Sounds/Button/DefaultButtonSound");

        _startPos = transform.position;
        _goalPos = _startPos - transform.up * pressDeep;

        if(!useDelay)
            delayTime = 0;
    }
    protected virtual void Update()
    {
        if(useDelay)
            _timer += Time.deltaTime;

        if (_buttonState == ButtonState.In)
        {
            _target = 1;
            _currentPos = Mathf.MoveTowards(_currentPos, _target , pressSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(_startPos, _goalPos, _currentPos);

            if (_currentPos == 1)
                _buttonState = ButtonState.Out;
        }

        if (_buttonState == ButtonState.Out)
        {
            if (_timer >= delayTime)
            {

                _target = 0;
                _currentPos = Mathf.MoveTowards(_currentPos, _target, pressSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(_startPos, _goalPos, _currentPos);

                if (_currentPos == 0)
                {
                    if (pressOutSound)
                        _buttonAudioSource.PlayOneShot(pressOutSound);
                    _buttonState = ButtonState.Static;
                }
            }
        }
    }
    public void DoSomething(GameObject sender)
    {
        if (_buttonState == ButtonState.Static)
        {
            _timer = 0;
            _buttonState = ButtonState.In;
            if(pressInSound)
            {
                _buttonAudioSource.PlayOneShot(pressInSound);
                Debug.Log("Есть звучок");
            }
            OnPressed();
        }
    }

    protected abstract void OnPressed();
}
