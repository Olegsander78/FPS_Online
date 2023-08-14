using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class InputController : MonoBehaviour
{        
    [SerializeField]
    private PlayerCharacter _player;

    [SerializeField]
    private float _restartDelay = 3f;

    [SerializeField]
    private PlayerGun _gun;

    [SerializeField]
    private float _mouseSensetivity = 2f;

    private MultiplayerManager _multiplayerManager;
    private bool _hold = false;
    private bool _hideCursor;

    private void Start()
    {
        _multiplayerManager = MultiplayerManager.Instance;
        _hideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _hideCursor = !_hideCursor;
            Cursor.lockState = _hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
        
        if (_hold)
            return;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var mouseX = 0f;
        var mouseY = 0f;

        var isShoot = false;

        if (_hideCursor == true)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            isShoot = Input.GetMouseButton(0);
        }
        

        var space = Input.GetKeyDown(KeyCode.Space);

        _player.SetInput(h, v, mouseX * _mouseSensetivity);

        _player.RotateX(-mouseY * _mouseSensetivity);

        if (space)
            _player.Jump();

        if (isShoot && _gun.TryShoot(out ShootInfo shootInfo))
            SendShoot(ref shootInfo);

        SendMove();
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

    public void Restart(int spawnIndex)
    {
        Debug.Log($"Index in restart {spawnIndex}");
        _multiplayerManager.SpawnPoints.GetPoint(spawnIndex,out Vector3 position, out Vector3 rotation);

        StartCoroutine(HoldRoutine());

        _player.transform.position = position;
        rotation.x = 0f;
        rotation.z = 0f;
        _player.transform.eulerAngles = rotation;
        _player.SetInput(0f, 0f, 0f);

        var data = new Dictionary<string, object>()
        {
            {"pX",position.x },
            {"pY",position.y },
            {"pZ",position.z },
            {"vX",0f },
            {"vY",0f },
            {"vZ",0f },
            {"rX",0f },
            {"rY",rotation.y }
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

