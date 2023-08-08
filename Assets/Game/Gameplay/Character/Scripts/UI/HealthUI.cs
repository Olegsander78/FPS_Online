using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _filledImage;

    [SerializeField]
    private float _defaultWidth;

    private void OnValidate()
    {
        _defaultWidth = _filledImage.sizeDelta.x;
    }
    public void UpdateHealth(float max, int current)
    {
        float percent = current / max;
        _filledImage.sizeDelta = new Vector2(_defaultWidth * percent, _filledImage.sizeDelta.y);
    } 
}
