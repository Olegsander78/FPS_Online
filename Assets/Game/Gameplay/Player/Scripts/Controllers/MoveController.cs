using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
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
        _player.GetMoveInfo(out Vector3 position);

        var data = new Dictionary<string, object>()
        {
            {"x",position.x },
            {"y",position.z }
        };
        
        MultiplayerManager.Instance.SendMessage("move", data);
    }
}
