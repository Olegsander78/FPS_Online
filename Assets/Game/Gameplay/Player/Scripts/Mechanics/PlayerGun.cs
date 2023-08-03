using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{   
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
        var velocity = _bulletPoint.forward * _bulletSpeed;
        
        _lastShootTime = Time.time;

        Instantiate(_bulletPrefab, position, _bulletPoint.rotation)
            .Init(velocity);

        OnShooted?.Invoke();

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;

        info.dX = velocity.x;
        info.dY = velocity.y;
        info.dZ = velocity.z;

        return true;
    }
}
