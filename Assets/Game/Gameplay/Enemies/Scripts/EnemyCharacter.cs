using System;
using UnityEngine;

public class EnemyCharacter : Character
{
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;

    [SerializeField]
    private Transform _head;

    [SerializeField]
    private CapsuleCollider _crouchedCollider;

    private float _velocityMagnitude = 0f;
    private readonly bool _isCrouched = true;


    private void Start()
    {
        TargetPosition = transform.position;
    }

    private void Update()
    {
        if (_velocityMagnitude > 0.1f)
        {
            var maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else
        {
            transform.position = TargetPosition;
        }
    }

    public void SetSpeed(float value) => Speed = value;

    internal void SetHeight(float value)
    {
        if (value == CROUCHED_HEIGHT)
        {
            _crouchedCollider.height = CROUCHED_HEIGHT;
            _crouchedCollider.center = new(0f, CROUCHED_CENTER_Y, 0f);

            OnCrouched?.Invoke(_isCrouched);
        }
        else if(value == STANDUP_HEIGHT)
        {
            _crouchedCollider.height = STANDUP_HEIGHT;
            _crouchedCollider.center = new(0f, STANDUP_CENTER_Y, 0f);

            OnCrouched?.Invoke(!_isCrouched);
        }
    }

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        TargetPosition = position + velocity * averageInterval;
        _velocityMagnitude = velocity.magnitude;
        
        this.Velocity = velocity;
    }

    public void SetRotateX(float value)
    {
        _head.localEulerAngles = new Vector3(value, 0f, 0f);
    }

    public void SetRotateLerpX(in float rotateXClient, in float rotateXServer, in float averageInterval)
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateXClient, 0f, 0f), Quaternion.Euler(rotateXServer, 0f, 0f), averageInterval);
    }

    public float GetRotateX()
    {
        return _head.localEulerAngles.x;
    }

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new Vector3(0f, value, 0f);
    }

    public void SetRotateLerpY(in float rotateYClient, in float rotateYServer, in float averageInterval)
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0f, rotateYClient, 0f), Quaternion.Euler(0f, rotateYServer, 0f), averageInterval);
    }

    public float GetRotateY()
    {
        return _head.localEulerAngles.y;
    }
}
