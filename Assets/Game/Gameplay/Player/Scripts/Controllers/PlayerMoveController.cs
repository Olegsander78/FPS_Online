using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _player;

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        _player.SetInput(h, v);

        SendMove();
    }

    private void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity);

        var data = new Dictionary<string, object>()
        {
            {"x", position.x },
            {"y", position.z },
            {"vx", velocity.x },
            {"vy", velocity.z }
        };

        MultiplayerManager.Instance.SendMessage("move", data);
        transform.position = position;
    }
}
