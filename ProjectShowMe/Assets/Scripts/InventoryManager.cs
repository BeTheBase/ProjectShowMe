using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : GenericSingleton<InventoryManager, IInventory>, IInventory
{
    [SerializeField] private List<Image> humanInventoryImages;
    private int index = 0;

    public void SetItem(int index)
    {
        if (humanInventoryImages[index].IsActive()) index += 1;
        humanInventoryImages[index].gameObject.SetActive(true);
    }
}
