using Inventory;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerController : MonoBehaviour
    {
        // Controls
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody2D _rb;
        private Vector2 _movementDirection;
        
        // Inventory
        private InventoryManager _inventoryManager;
        public int wood;
        public int ore;
        public int shovel;
    
        //Craft
        public bool isInCraftingZone;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _inventoryManager = FindFirstObjectByType<InventoryManager>();
            isInCraftingZone = false;
        }

        void Update()
        {
            _rb.linearVelocity = _movementDirection * moveSpeed;
            _movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            _inventoryManager.AddItemByCollision(collision);
            
            if (collision.CompareTag("CraftBag"))
            {
                isInCraftingZone = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("CraftBag"))
            {
                isInCraftingZone = false;
            }
        }

    }
}
