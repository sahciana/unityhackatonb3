using Actors.Player;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject inventoryMenu;
        
        public ItemSlot[] itemSlots;
        
        private PlayerController _playerController;
        
        private void Start()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            inventoryMenu.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) && inventoryMenu.activeSelf) {
                Time.timeScale = 1;
                inventoryMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.I) && !inventoryMenu.activeSelf) {
                Time.timeScale = 0;
                inventoryMenu.SetActive(true);
            }
        }

        public void AddItemToInventory(string itemName, Sprite itemSprite)
        {
            foreach (var slot in itemSlots)
            {
                if (!slot.isFull)
                {
                    slot.AddItem(itemName, itemSprite);
                    
                    if (itemName == "Tree")
                    {
                        _playerController.wood++;
                    }
                    else if (itemName == "Stone")
                    {
                        _playerController.ore++;
                    }else if (itemName == "Shovel")
                    {
                        _playerController.shovel++;
                    }
                    break;
                }
            }
        }


        public void AddItemByCollision(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Stone"))
            {
                foreach (var slot in itemSlots)
                {
                    if (!slot.isFull)
                    {
                        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();

                        if (collision.CompareTag("Tree"))
                        {
                            slot.AddItem("Tree", spriteRenderer.sprite);
                            _playerController.wood++;
                        }
                        else if (collision.CompareTag("Stone"))
                        {
                            slot.AddItem("Stone", spriteRenderer.sprite);
                            _playerController.ore++;
                        }

                        collision.gameObject.SetActive(false);
                        Destroy(collision.gameObject);

                        break;
                    }
                }
            }
        }
    }
}