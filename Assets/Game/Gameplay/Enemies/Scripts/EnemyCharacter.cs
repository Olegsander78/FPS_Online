using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyCharacter : Character
{
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    public Quaternion TargetRotationY { get; private set; }

    [SerializeField]
    private Transform _head;

    [SerializeField]
    private CapsuleCollider _crouchedCollider;

    private float _velocityMagnitude = 0f;
    private float _angularVelocityYMagnitude = 0f;
    private readonly bool _isCrouched = true;


    private void Start()
    {
        TargetPosition = transform.position;
        TargetRotationY = transform.rotation;
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

        if (_angularVelocityYMagnitude > 0.1f)
        {
            var maxAngle = _angularVelocityYMagnitude * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotationY, maxAngle);

            TargetRotationY = transform.rotation;
        }
        else
        {
            transform.rotation = TargetRotationY;
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

    public void SetSmoothRotationY(in float eulerRotation, in float angularVelocityY, in float averageInterval)
    {
        if (eulerRotation != 0f && angularVelocityY != 0f)
        {
            TargetRotationY = Quaternion.Euler(0f, eulerRotation, 0f) * Quaternion.Euler(0f, angularVelocityY * averageInterval, 0f);

            _angularVelocityYMagnitude = Mathf.Abs(new Vector3(0f, angularVelocityY * averageInterval, 0f).magnitude);

            this.AngularVelocity = new Vector3(0f, angularVelocityY * averageInterval, 0f);
        }
    }

    public void SetRotateX(float value)
    {
        _head.localEulerAngles = new Vector3(value, 0f, 0f);
    }


    public float GetRotateX()
    {
        return _head.localEulerAngles.x;
    }

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new Vector3(0f, value, 0f);
    }

    public void SetRotateLerpY(in float rotateYClient, in float angularVelocutyY, in float averageInterval)
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0f, rotateYClient, 0f), Quaternion.Euler(0f, angularVelocutyY, 0f), averageInterval);
        _angularVelocityYMagnitude = new Vector3(0f, angularVelocutyY, 0f).magnitude;
        this.AngularVelocity = new(0f, angularVelocutyY, 0f);        
    }

    public float GetRotateY()
    {
        return transform.localEulerAngles.y;
    }
}
