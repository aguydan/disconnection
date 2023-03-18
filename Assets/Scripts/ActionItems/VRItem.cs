using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRItem : MonoBehaviour
{
    [SerializeField] Sprite[] _hintItems;

    List<Item> _wrongItems = new List<Item>();
    List<GameObject> _spawnedHintItems = new List<GameObject>();
    
    public int CalculateStartingWrongItemsAmount()
    {
        int wrongItemsAmount = Mathf.FloorToInt((float)ItemSpawner.Instance.ColoredItems.Count / 2f);

        return wrongItemsAmount;
    }

    public void IdentifyWrongItems(int wrongItemsAmount)
    {
        List<Item> items = ItemSpawner.Instance.ColoredItems;

        for (int i = 0; i < wrongItemsAmount; i++)
        {
            int randomIndex = Random.Range(0, items.Count);

            while (_wrongItems.Contains(items[randomIndex]) || items[randomIndex].hasPositivePoints)
            {
                randomIndex = Random.Range(0, items.Count);
            }

            _wrongItems.Add(items[randomIndex]);
        }
    }

    public IEnumerator TurnItemsToHintItems()
    {
        int third = Mathf.FloorToInt((float)_wrongItems.Count / 3f);
        int twoThirds = third * 2;
        
        yield return new WaitForSeconds(1);
        for (int i = 0; i < third; i++) AssignHintItemSprite(i);

        yield return new WaitForSeconds(1);
        for (int i = third; i < twoThirds; i++) AssignHintItemSprite(i);

        yield return new WaitForSeconds(1);
        for (int i = twoThirds; i < _wrongItems.Count; i++) AssignHintItemSprite(i);
    }

    void AssignHintItemSprite(int i)
    {
        int randomIndex = Random.Range(0, _hintItems.Length);

        _wrongItems[i].look.sprite = _hintItems[randomIndex];
    }


    public void TurnItemsBack()
    {
        foreach (Item item in _wrongItems)
        {
            item.look.sprite = item.OriginalSprite;
        }
    }

    public void UpdateWrongItems()
    {
        List<Item> wrongItems = new List<Item>();

        foreach (Item item in _wrongItems)
        {
            if (item) wrongItems.Add(item);
        }

        _wrongItems = wrongItems;
    }

    public void UpdateInteractableColliders(bool isEnabled)
    {
        List<Furniture> furniture = FurnitureSpawner.Instance.SpawnedFurniture;
        List<InteractableFurniture> interactables = FurnitureSpawner.Instance.SpawnedInteractables;

        foreach (Furniture item in furniture) item.Collider.enabled = !isEnabled;
        foreach (InteractableFurniture item in interactables) item.Collider.enabled = isEnabled;
    }
}
