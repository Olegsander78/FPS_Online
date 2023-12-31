using Colyseus.Schema;
using System.Collections.Generic;
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

    [SerializeField]
    private EnemyCharacter _enemyCharacter;

    [SerializeField]
    private EnemyGun _gun;

    private List<float> _receiveTimeInterval = new() { 0, 0, 0, 0, 0 };

    private float _lastReceiveTime = 0f;
    private Player _player;

    public void Init(string key, Player player)
    {
        _enemyCharacter.Init(key);
        _player = player;
        _enemyCharacter.SetSpeed(player.speed);
        _enemyCharacter.SetMaxHP(player.maxHP);

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
        
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance.LossCounter.SetEnemyLoss((byte)dataChange.Value);
                    break;
                case "currentHP":
                    if ((sbyte)dataChange.Value > (sbyte)dataChange.PreviousValue)
                        _enemyCharacter.RestoreHP((sbyte)dataChange.Value);
                    break;
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
                    _enemyCharacter.SetRotateY((float)dataChange.Value);
                    break;
                default:
                    Debug.LogWarning("�� �������������� � Enemy ��������� ����: " + dataChange.Field);
                    break;
            }
        }

        _enemyCharacter.SetMovement(position, velocity, AverageInterval);
    }

    private void SaveReceiveTime()
    {
        var interval = Time.time - _lastReceiveTime;

        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.RemoveAt(0);
    }
}

