using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRItem : MonoBehaviour
{
    [SerializeField] GameObject _cross;

    List<Item> _wrongItems = new List<Item>();
    List<GameObject> _spawnedCrosses = new List<GameObject>();
    
    public void IdentifyWrongItems()
    {
        List<Item> items = ItemSpawner.Instance.ColoredItems;

        if (items.Count <= 4)
        {
            Debug.Log("not enough colored items");
        }
        else
        {
            _wrongItems = new List<Item>();

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, items.Count);

                while (_wrongItems.Contains(items[randomIndex]) || items[randomIndex].hasPositivePoints)
                {
                    randomIndex = Random.Range(0, items.Count);
                }

                _wrongItems.Add(items[randomIndex]);
            }
        }
    }

    public IEnumerator SpawnCrosses()
    {
        foreach (Item item in _wrongItems)
        {
            yield return new WaitForSeconds(1);
            GameObject cross = Instantiate(_cross, item.transform.position, Quaternion.identity);
            _spawnedCrosses.Add(cross);
        }
    }

    public void ClearCrosses()
    {
        foreach (GameObject cross in _spawnedCrosses)
        {
            Destroy(cross);
        }
        _spawnedCrosses.Clear();
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
