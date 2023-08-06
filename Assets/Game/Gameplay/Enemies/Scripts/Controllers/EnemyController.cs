using Colyseus.Schema;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float AverageInterval
    {
        get
        {
            float summ = 0;
            for (int i = 0; i < _receiveTimeInterval.Count; i++)
            {
                summ += _receiveTimeInterval[i];
            }
            return summ / _receiveTimeInterval.Count;
        }
    }

    private float AverageIntervalRotation
    {
        get
        {
            float summ = 0;
            for (int i = 0; i < _receiveTimeIntervalRotation.Count; i++)
            {
                summ += _receiveTimeIntervalRotation[i];
            }
            return summ / _receiveTimeIntervalRotation.Count;
        }
    }

    [SerializeField]
    private EnemyCharacter _enemyCharacter;

    [SerializeField]
    private EnemyGun _gun;

    private List<float> _receiveTimeInterval = new() { 0, 0, 0, 0, 0 };
    private List<float> _receiveTimeIntervalRotation = new() { 0, 0, 0, 0, 0 };

    private float _lastReceiveTime = 0f;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _enemyCharacter.SetSpeed(player.speed);
        _enemyCharacter.SetHeight(player.cH);
        player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo info)
    {
        var position = new Vector3(info.pX, info.pY, info.pZ);
        var velocity = new Vector3(info.dX, info.dY, info.dZ);

        _gun.Shoot(position, velocity);
    }

    public void Destroy()
    {
        _player.OnChange -= OnChange;

        Destroy(gameObject);
    }

    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        var position = _enemyCharacter.TargetPosition;
        var velocity = _enemyCharacter.Velocity;

        float rotationY = _enemyCharacter.TargetRotationY.y;
        float angularVelocityY = _enemyCharacter.AngularVelocity.y;

        //var rotationX = _enemyCharacter.GetRotateX();
        //var rotationY = _enemyCharacter.GetRotateY();
        
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                case "rX":
                    _enemyCharacter.SetRotateX((float)dataChange.Value);
                    break;
                case "rY":
                    rotationY = (float)dataChange.Value;
                    break;
                //case "rY":
                //    _enemyCharacter.SetRotateY((float)dataChange.Value);
                //    break;
                case "avY":
                    angularVelocityY = (float)dataChange.Value;
                    break;
                case "cH":
                    _enemyCharacter.SetHeight((float)dataChange.Value);
                    break;
                default:
                    Debug.LogWarning("Не обрабатывается изменение поля: " + dataChange.Field);
                    break;
            }
        }

        _enemyCharacter.SetMovement(position, velocity, AverageInterval);

        _enemyCharacter.SetSmoothRotationY(rotationY, angularVelocityY, AverageIntervalRotation);
    }

    private void SaveReceiveTime()
    {
        var interval = Time.time - _lastReceiveTime;

        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.Remove(0);
    }
}

