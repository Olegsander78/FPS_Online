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
    }
}
