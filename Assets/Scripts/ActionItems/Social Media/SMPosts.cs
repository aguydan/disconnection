using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NonNegativePost
{
    public ItemSprite.ItemCategory Category;
    public Sprite Sprite;
}

[CreateAssetMenu(menuName = "SMPosts")]
public class SMPosts : ScriptableObject
{
    public NonNegativePost[] PositivePosts;
    public Sprite[] NegativePosts;
    public NonNegativePost[] SecretPosts;
}
