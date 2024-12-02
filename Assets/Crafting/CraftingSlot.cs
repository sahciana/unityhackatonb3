using UnityEngine;
using UnityEngine.UI;

namespace Crafting
{
    public class CraftingSlot : MonoBehaviour
    {
        [SerializeField] private Button craftButton;
        [SerializeField] private Text recipeText;
        private CraftingManager _craftingManager;

        private void Start()
        {
            _craftingManager = FindFirstObjectByType<CraftingManager>();
            craftButton.onClick.AddListener(CraftItem);
        }

        public void SetRecipe(string itemName)
        {
            recipeText.text = $"Craft {itemName}";
        }

        private void CraftItem()
        {
            _craftingManager.TryCraftItem("Shovel");
        }
    }
}