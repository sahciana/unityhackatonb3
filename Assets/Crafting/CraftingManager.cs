using System.Collections.Generic;
using Actors.Player;
using Inventory;
using UnityEngine;

namespace Crafting
{
    public class CraftingManager : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private GameObject _shovelPrefab;
        
        public GameObject craftingMenu;
        
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            craftingMenu.SetActive(false);
        }

        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.C) && !craftingMenu.activeSelf) {
                craftingMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if ( Input.GetKeyDown(KeyCode.C) && craftingMenu.activeSelf) {
                craftingMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        
        // Recettes
        private Dictionary<string, (int tree, int stone)> _craftingRecipes = new Dictionary<string, (int, int)>
        {
            { "Shovel", (2, 1) }
        };

        public void TryCraftItem(string itemName)
        {
            if (_craftingRecipes.ContainsKey(itemName))
            {
                var recipe = _craftingRecipes[itemName];
                int requiredWood = recipe.tree;
                int requiredOre = recipe.stone;

                int woodCount = 0;
                int oreCount = 0;

                foreach (var slot in _inventoryManager.itemSlots)
                {
                    if (slot.isFull)
                    {
                        if (slot.itemName == "Tree") woodCount++;
                        if (slot.itemName == "Stone") oreCount++;
                    }
                }

                if (woodCount >= requiredWood && oreCount >= requiredOre)
                {
                    CreateItem(itemName);
                    
                    RemoveResources(requiredWood, requiredOre);
                }
                else
                {
                    Debug.Log("Pas assez de ressources pour créer " + itemName);
                }
            }
        }

        private void CreateItem(string itemName)
        {
            if (itemName == "Shovel")
            {
                GameObject shovel = Instantiate(_shovelPrefab);
                _inventoryManager.AddItemToInventory(shovel.gameObject.name, shovel.gameObject.GetComponent<SpriteRenderer>().sprite);
                Debug.Log("Pelle créée !");
            }
        }

        private void RemoveResources(int woodCount, int oreCount)
        {
            int woodRemoved = 0;
            int oreRemoved = 0;

            foreach (var slot in _inventoryManager.itemSlots)
            {
                if (slot.isFull)
                {
                    if (woodRemoved < woodCount && slot.itemName == "Tree")
                    {
                        slot.isFull = false;
                        woodRemoved++;
                    }
                    if (oreRemoved < oreCount && slot.itemName == "Stone")
                    {
                        slot.isFull = false;
                        oreRemoved++;
                    }

                    if (woodRemoved >= woodCount && oreRemoved >= oreCount)
                        break;
                }
            }
        }
    }
}
