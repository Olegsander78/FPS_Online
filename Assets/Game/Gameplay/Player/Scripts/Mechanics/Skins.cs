using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    public int Lenght { get { return _materials.Length; } }

    public Material GetMaterial(int index)
    {
        if (_materials.Length <= index)
            return _materials[0];

        return _materials[index];
    }
}
