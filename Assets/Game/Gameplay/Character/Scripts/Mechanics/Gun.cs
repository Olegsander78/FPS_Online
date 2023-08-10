using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Action OnShooted;

    [SerializeField]
    protected Bullet _bulletPrefab;

    [SerializeField]
    protected float _bulletSpeed;

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected float _shootDelay;

    public void InitStats(WeaponInfo weaponInfo)
    {
        _bulletSpeed = weaponInfo.BulletSpeed;
        _damage = weaponInfo.Damage;
        _shootDelay = weaponInfo.ShootDelay;
    }
}
