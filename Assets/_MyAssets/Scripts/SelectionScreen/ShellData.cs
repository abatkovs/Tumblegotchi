using UnityEngine;

[CreateAssetMenu(fileName = "ShellData", menuName = "GameData/TamagotchiShell", order = 0)]
public class ShellData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Color ScreenColor { get; private set; }
    [field: SerializeField] public Sprite ShellSprite { get; private set; }

}
