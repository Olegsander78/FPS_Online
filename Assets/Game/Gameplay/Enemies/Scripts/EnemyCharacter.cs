using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField]
    private Health _health;
    
    [SerializeField]
    private Transform _head;
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;

    private float _velocityMagnitude = 0f;
    private string _sessionID;

    public void Init(string sessionID)
    {
        _sessionID = sessionID;
    }

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

    public void SetMaxHP(int value)
    {
        MaxHealth = value;
        _health.SetMax(value);
        _health.SetCurrent(value);
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

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new Vector3(0f, value, 0f);
    }

    public void ApllyDamage(int damage)
    {
        _health.ApllyDamage(damage);

        var data = new Dictionary<string, object>()
        {
            {"id", _sessionID },
            {"value", damage }
        };

        MultiplayerManager.Instance.SendMessage("damage", data);
    }
}
