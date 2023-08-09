using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    public Action<int> OnWeaponSelected;
    
    [SerializeField]
    private PlayerCharacter _player;

    [SerializeField]
    private float _restartDelay = 3f;

    [SerializeField]
    private GameObject[] _weapons; 

    [SerializeField]
    private PlayerGun _gun;

    [SerializeField]
    private float _mouseSensetivity = 2f;

    private MultiplayerManager _multiplayerManager;
    private bool _hold = false;
    private int _currentWeaponIndex = 0;


    private void Start()
    {
        _multiplayerManager = MultiplayerManager.Instance;


    }
    private void Update()
    {
        if (_hold)
            return;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var isShoot = Input.GetMouseButton(0);

        var space = Input.GetKeyDown(KeyCode.Space);

        _player.SetInput(h, v, mouseX * _mouseSensetivity);

        _player.RotateX(-mouseY * _mouseSensetivity);

        var scrollWeapons = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWeapons != 0)
        {
            int direction = scrollWeapons > 0 ? 1 : -1;
            SwitchWeapon(_currentWeaponIndex + direction);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon(_currentWeaponIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon(_currentWeaponIndex + 1);
        }

        if (space)
            _player.Jump();

        if (isShoot && _gun.TryShoot(out ShootInfo shootInfo))
            SendShoot(ref shootInfo);

        SendMove();
    }

    private void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= _weapons.Length)
        {
            return;
        }
        DeselectWeapon(_currentWeaponIndex); // Деактивируем текущее оружие
        _currentWeaponIndex = weaponIndex; // Обновляем индекс текущего оружия
        SelectWeapon(_currentWeaponIndex); // Активируем новое оружие
    }

    private void SelectWeapon(int weaponIndex)
    {
        _weapons[weaponIndex].SetActive(true);

        if (_weapons[weaponIndex].TryGetComponent(out WeaponInfo weaponInfo))
            _gun.Init(weaponInfo);

        //OnWeaponSelected?.Invoke(weaponIndex);
    }

    private void DeselectWeapon(int weaponIndex)
    {
        _weapons[weaponIndex].SetActive(false);
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = _multiplayerManager.GetSessionID();
        
        var json = JsonUtility.ToJson(shootInfo);

        _multiplayerManager.SendMessage("shoot", json);
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position
            ,out Vector3 velocity
            ,out float rotateX
            ,out float rotateY);

        var data = new Dictionary<string, object>()
        {
            {"pX",position.x },
            {"pY",position.y },
            {"pZ",position.z },
            {"vX",velocity.x },
            {"vY",velocity.y },
            {"vZ",velocity.z },
            {"rX",rotateX },
            {"rY",rotateY }
        };
        
        _multiplayerManager.SendMessage("move", data);
    }

    public void Restart(string jsonRestartInfo)
    {
        RestartInfo info = JsonUtility.FromJson<RestartInfo>(jsonRestartInfo);

        StartCoroutine(HoldRoutine());

        _player.transform.position = new Vector3(info.x, 0f, info.z);
        _player.SetInput(0f, 0f, 0f);

        var data = new Dictionary<string, object>()
        {
            {"pX",info.x },
            {"pY",0f },
            {"pZ",info.z },
            {"vX",0f },
            {"vY",0f },
            {"vZ",0f },
            {"rX",0f },
            {"rY",0f }
        };

        _multiplayerManager.SendMessage("move", data);
    }

    private IEnumerator HoldRoutine()
    {
        _hold = true;
        yield return new WaitForSecondsRealtime(_restartDelay);
        _hold = false;
    }
}

[Serializable]
public struct ShootInfo
{
    public string key;
    public float pX;
    public float pY;
    public float pZ;
    public float dX;
    public float dY;
    public float dZ;
}

[Serializable]
public struct RestartInfo
{
    public float x;
    public float z;
}

