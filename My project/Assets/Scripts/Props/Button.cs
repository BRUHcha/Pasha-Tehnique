using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum ButtonState
{
    Static = 0,
    In = 1,
    Out = 2
}

public abstract class Button : MonoBehaviour, IClickable
{
    [SerializeField] private float pressSpeed = 10f;
    [SerializeField] private float pressDeep = 0.05f;

    private ButtonState _buttonState;
    private float _currentPos, _target;
    private Vector3 _startPos;
    private Vector3 _goalPos;

    protected virtual void Start()
    {
        _startPos = transform.position;
        _goalPos = _startPos - transform.up * pressDeep;
        Debug.Log(_goalPos);
    }
    protected virtual void Update()
    {
        Debug.DrawLine(_startPos, _goalPos, Color.blue);
        if (_buttonState == ButtonState.In)
        {
            Debug.Log(_buttonState);
            _target = 1;
            _currentPos = Mathf.MoveTowards(_currentPos, _target , pressSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(_startPos, _goalPos, _currentPos);

            if (_currentPos == 1)
                _buttonState = ButtonState.Out;
        }

        if (_buttonState == ButtonState.Out)
        {
            Debug.Log(_buttonState);
            _target = 0;
            _currentPos = Mathf.MoveTowards(_currentPos, _target, pressSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(_startPos, _goalPos, _currentPos);
            if (_currentPos == 0)
                _buttonState = ButtonState.Static;
        }
    }
    public void DoSomething(GameObject sender)
    {
        if (_buttonState == ButtonState.Static)
        {
            _buttonState = ButtonState.In;
            OnPressed();
        }
    }

    protected abstract void OnPressed();
}
