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

    private List<float> _receiveTimeInterval = new() { 0, 0, 0, 0, 0 };

    private float _lastReceiveTime = 0f;

    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        var position = _enemyCharacter.TargetPosition;
        var velocity = Vector3.zero;
        
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
                default:
                    Debug.LogWarning("Не обрабатывается изменение поля: " + dataChange.Field);
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
        _receiveTimeInterval.Remove(0);
    }
}

