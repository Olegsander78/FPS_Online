using System;
using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] 
    private Rigidbody _rigidbody;

    [SerializeField]
    private CheckFly _checkFly;

    [SerializeField]
    private Transform _head;

    [SerializeField]
    private Transform _cameraPoint;

    [SerializeField]
    private float _maxHeadAngle = 90f;

    [SerializeField]
    private float _minHeadAngle = -90f;

    [SerializeField]
    private float _jumpForce = 5f;

    [SerializeField]
    private float _jumpDelay = 0.2f;

    //[SerializeField]
    //private float _timeCrouching = 5f;

    [SerializeField]
    private Transform _crouchedTransform;

    private float _inputH, _inputV, _rotateY, _currentRotateX, _jumpTime;

    private Vector3 _standingScale = new(1f, 1f, 1f);
    private Vector3 _crouchingScale = new(1f,0.5f,1f);

    private void Start()
    {
        var camera = Camera.main.transform;
        camera.parent = _cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;
    }
    private void FixedUpdate()
    {
        Move();
        RotateY();
    }
    public void RotateX(float value)
    {
        _currentRotateX = Mathf.Clamp(_currentRotateX + value, _minHeadAngle, _maxHeadAngle);
        _head.localEulerAngles = new Vector3(_currentRotateX, 0f, 0f);
    }
    public void RotateY()
    {
        _rigidbody.angularVelocity = new Vector3(0f, _rotateY, 0f);
        _rotateY = 0f;
    }
    public void SetInput(float h, float v, float rotateY)
    {
        _inputH = h;
        _inputV = v;
        _rotateY += rotateY;
    }

    public void Jump()
    {
        if (_checkFly.IsFly)
            return;

        if (Time.time - _jumpTime < _jumpDelay)
        {
            //Debug.Log($"Time = {Time.time}, JumpTime = {_jumpTime}, Time-jumptime = {Time.time - _jumpTime}");
            return;
        }

        _jumpTime = Time.time;
        //Debug.Log($"Time = {Time.time}, JumpTime = {_jumpTime}, Time-jumptime = {Time.time - _jumpTime}");
        _rigidbody.AddForce(0f, _jumpForce, 0f, ForceMode.VelocityChange);
    }

    internal bool Crouche()
    {        
        //_crouchedTransform.localScale = Vector3.Lerp(_crouchedTransform.localScale, _crouchingScale, Time.deltaTime * _timeCrouching);
        _crouchedTransform.localScale = _crouchingScale;

        OnCrouched?.Invoke(true);

        return true;
    }

    internal bool StandUp()
    {
      
        //_crouchedTransform.localScale = Vector3.Lerp(_crouchedTransform.localScale, _standingScale, Time.deltaTime * _timeCrouching);
        _crouchedTransform.localScale = _standingScale;

        OnCrouched?.Invoke(false);

        return true;
    }

    private void Move()
    {
        var velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * Speed;
        velocity.y = _rigidbody.velocity.y;
        Velocity = velocity;
        _rigidbody.velocity = Velocity;
    }

    public void GetMoveInfo(out Vector3 position
        ,out Vector3 velocity
        ,out float rotateX
        ,out float rotateY
        ,out float scaleY)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;

        rotateX = _head.localEulerAngles.x;
        rotateY = transform.eulerAngles.y;

        scaleY = _crouchedTransform.localScale.y;
    }     
}
