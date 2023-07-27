using Colyseus.Schema;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    private Vector3 _lastPosition;
    private Vector3 _velocity;

    [SerializeField]
    private float _interpolationTime = 0.1f;
        
    private float _interpolationTimer = 0f;

    private Vector3 _smoothVelocity;

    private void Start()
    {
        _lastPosition = transform.position;
    }


    //private void Update()
    //{
    //    transform.position = Vector3.SmoothDamp(transform.position, _lastPosition , ref _smoothVelocity, 0.1f);
    //}

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _lastPosition + _velocity, Time.deltaTime);
    }

    internal void OnChange(List<DataChange> changes)
    {
        if (changes == null)
            return;

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "x":
                    _velocity.x = (float)dataChange.Value - _lastPosition.x;
                    _lastPosition.x = (float)dataChange.Value;
                    break;
                case "y":
                    _velocity.z = (float)dataChange.Value - _lastPosition.z; 
                    _lastPosition.z = (float)dataChange.Value;
                    break;
                case "vx":
                    _velocity.x = (float)dataChange.Value;
                    break;
                case "vy":
                    _velocity.z = (float)dataChange.Value;
                    break;
                default:
                    Debug.LogWarning("Не обрабатывается изменение поля: " + dataChange.Field);
                    break;
            }
        }
       
        _interpolationTimer = _interpolationTime;
    }
}