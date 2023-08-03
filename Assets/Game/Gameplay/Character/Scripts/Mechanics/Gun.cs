using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Action OnShooted;

    [SerializeField]
    protected Bullet _bulletPrefab;

}
