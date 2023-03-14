using UnityEngine;

[System.Serializable]
public class ItemObject {
    public string ItemKey;
    public Sprite ItemSprite;
    public string ItemText;
}

[CreateAssetMenu(menuName = "ItemObjects")]
public class ItemObjects : ScriptableObject
{
    public ItemObject[] PositiveItemObjects;
    public ItemObject[] NegativeItemObjects;
}
