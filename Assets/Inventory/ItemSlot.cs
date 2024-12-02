using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemSlot : MonoBehaviour
    {
        // ITEM
        public string itemName;
        public Sprite itemIcon;
        
        // SLOT
        [SerializeField]
        private Image itemImage;
        public bool isFull;

        void Start()
        {
            isFull = false;
        }
        
        private void Awake()
        {
            itemImage = GetComponent<Image>();
        }

        public void AddItem(string itemName, Sprite itemIcon)
        {
            this.itemIcon = itemIcon;
            this.itemName = itemName;
            
            itemImage.sprite = itemIcon;
            isFull = true;
        }
    }
}