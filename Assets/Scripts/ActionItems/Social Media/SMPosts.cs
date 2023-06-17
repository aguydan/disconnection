using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositivePost
{
    public ItemSprite.ItemCategory Category;
    public Sprite Sprite;
}

[System.Serializable]
public class SecretPost
{
    public Sprite Sprite;
    public CommentData[] Comments;
}

[System.Serializable]
public class CommentData
{
    public Sprite Sprite;
    public bool IsWinningComment;
}

[CreateAssetMenu(menuName = "SMPosts")]
public class SMPosts : ScriptableObject
{
    public PositivePost[] PositivePosts;
    public Sprite[] NegativePosts;
    public SecretPost[] SecretPosts;
}
