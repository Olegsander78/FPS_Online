using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private MultiplayerManager _multiplayerManager;
    
    [SerializeField]
    private PlayerCharacter _player;

    [SerializeField]
    private PlayerGun _gun;

    [SerializeField]
    private float _mouseSensetivity = 2f;

    private void Start()
    {
        _multiplayerManager = MultiplayerManager.Instance;
    }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var isShoot = Input.GetMouseButton(0);

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
