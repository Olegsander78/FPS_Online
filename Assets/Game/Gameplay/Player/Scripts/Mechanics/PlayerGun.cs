using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public event Action OnShooted;
    
    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private Transform _bulletPoint;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _shootDelay;

    private float _lastShootTime;

    public bool TryShoot(out ShootInfo info)
    {
        info = new();

        if (Time.time - _lastShootTime < _shootDelay)
            return false;

        var position = _bulletPoint.position;
        var direction = _bulletPoint.forward;
        
        _lastShootTime = Time.time;

        Instantiate(_bulletPrefab, position, _bulletPoint.rotation)
            .Init(direction, _bulletSpeed);

        OnShooted?.Invoke();

        direction *= _bulletSpeed;

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;

        info.dX = direction.x;
        info.dY = direction.y;
        info.dZ = direction.z;

        return true;
    }
}
