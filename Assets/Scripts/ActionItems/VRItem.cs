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

                items[randomIndex].look.color = Color.blue;
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
}
