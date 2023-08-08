using UnityEngine;
using UnityEngine.UI;

public class LossCounter : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private int _enemyLoss;
    private int _playerLoss;

    public void SetEnemyLoss(int value)
    {
        _enemyLoss = value;
        UpdateText();
    }

    public void SetPlayerLoss(int value)
    {
        _playerLoss = value;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{_enemyLoss} : {_playerLoss}";
    }
}
