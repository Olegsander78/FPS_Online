using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        _player.SetInput(h, v);
    }
}
