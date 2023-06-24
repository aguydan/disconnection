using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSprite
{
    public enum ItemCategory 
    {
        Instruments,
        Toys,
        Sports,
        Art,
        Everyday,
        Archives,
        Food
    }
    
    public string Name;
    public Sprite Sprite;
    public Sprite GrayscaleSprite;
    public ItemCategory Category;
    [Range(0f, 100f)] public float Chance = 100f;
    [HideInInspector] public double _weight;
}

public class RandomSpriteGenerator : MonoBehaviour
{
    public ItemSprite[] itemSprites;
    double accumulatedWeights;
    System.Random rand = new System.Random();
    
    void Awake()
    {
        CalculateWeights();
    }
    
    public ItemSprite GetRandomSprite()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < itemSprites.Length; i++)
        {
            if (itemSprites[i]._weight >= r) return itemSprites[i];
        }
        
        return itemSprites[0];
    }

    void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (ItemSprite sprite in itemSprites)
        {
            accumulatedWeights += sprite.Chance;
            sprite._weight = accumulatedWeights;
        }
    }
}
