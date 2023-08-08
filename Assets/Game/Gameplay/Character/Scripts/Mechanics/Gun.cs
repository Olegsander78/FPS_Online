using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Action OnShooted;

    [SerializeField]
    protected Bullet _bulletPrefab;

}
