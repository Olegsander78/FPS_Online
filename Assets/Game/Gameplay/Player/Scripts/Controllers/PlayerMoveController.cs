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

//public class MoveController : MonoBehaviour
//{
//    [SerializeField]
//    private PlayerCharacter _player;

//    private void Update()
//    {
//        var h = Input.GetAxisRaw("Horizontal");
//        var v = Input.GetAxisRaw("Vertical");

//        _player.SetInput(h, v);

//        SendMove();
//    }

//    private void SendMove()
//    {
//        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity);

//        var data = new Dictionary<string, object>()
//        {
//            {"x", position.x },
//            {"y", position.z },
//            {"vx", velocity.x },
//            {"vy", velocity.z },
//            {"px", _player.GetPredictedPosition(0.1f).x },
//            {"py", _player.GetPredictedPosition(0.1f).z }
//        };

//        MultiplayerManager.Instance.SendMessage("move", data);
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;

//public class MoveController : MonoBehaviour
//{
//    [SerializeField]
//    private PlayerCharacter _player;

//    private void Update()
//    {
//        var h = Input.GetAxisRaw("Horizontal");
//        var v = Input.GetAxisRaw("Vertical");

//        _player.SetInput(h, v);

//        SendMove();
//    }

//    private void SendMove()
//    {
//        _player.GetMoveInfo(out Vector3 position);

//        var data = new Dictionary<string, object>()
//        {
//            {"x",position.x },
//            {"y",position.z }
//        };

//        MultiplayerManager.Instance.SendMessage("move", data);
//    }
//}
