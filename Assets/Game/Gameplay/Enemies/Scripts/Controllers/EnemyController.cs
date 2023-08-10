using Colyseus.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private GameObject[] _weapons;

    private List<float> _receiveTimeInterval = new() { 0, 0, 0, 0, 0 };

    private float _lastReceiveTime = 0f;
    private Player _player;
    private int _currentWeaponIndex = 0;

    public void Init(string key, Player player)
    {
        _enemyCharacter.Init(key);
        _player = player;
        _enemyCharacter.SetSpeed(player.speed);
        _enemyCharacter.SetMaxHP(player.maxHP);

        _gun.InitStats(_weapons[_currentWeaponIndex]?.GetComponent<WeaponInfo>());
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i == _currentWeaponIndex)
                _weapons[i].SetActive(true);
            else
                _weapons[i].SetActive(false);
        }

        player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo info)
    {
        var position = new Vector3(info.pX, info.pY, info.pZ);
        var velocity = new Vector3(info.dX, info.dY, info.dZ);

        _gun.Shoot(position, velocity);
    }

    public void ChangeWeapon(WeaponMetadata weaponData)
    {
        WeaponInfo weaponInfo = _weapons[_currentWeaponIndex].GetComponent<WeaponInfo>();
        _weapons[_currentWeaponIndex].SetActive(true);

        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i]?.GetComponent<WeaponInfo>().WeaponType == weaponData.weaponType)
            {
                weaponInfo = _weapons[i].GetComponent<WeaponInfo>();
                _weapons[i].SetActive(true);
            }
            else
            {
                _weapons[i].SetActive(false);
            }
        } 

        _gun.InitStats(weaponInfo);
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
                    MultiplayerManager.Instance.LossCounter.SetEnemyLoss((byte)dataChange.PreviousValue);
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
                //case "wType":
                //    ChangeWeapon(GetWeaponInfo((int)dataChange.Value));
                //    break;
                default:
                    Debug.LogWarning("Не обрабатывается в Enemy изменение поля: " + dataChange.Field);
                    break;
            }
        }

        _enemyCharacter.SetMovement(position, velocity, AverageInterval);
    }

    private WeaponInfo GetWeaponInfo(int weaponIndex)
    {
        WeaponInfo weaponInfo = _weapons[0]?.GetComponent<WeaponInfo>();

        foreach (var weapon in _weapons)
        {
            if (weapon == _weapons.ElementAtOrDefault(weaponIndex))
            {               
                weapon.TryGetComponent(out weaponInfo);
            }
        }

        return weaponInfo;
    }
    private void SaveReceiveTime()
    {
        var interval = Time.time - _lastReceiveTime;

        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.RemoveAt(0);
    }
}

