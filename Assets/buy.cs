using UnityEngine;

public class buy : MonoBehaviour
{
    public GameObject monsterPrefab;

    public void BuyMonster()
    {
        UIManager ui = Object.FindFirstObjectByType<UIManager>();
        if (ui != null)
        {
            ui.shopPanel.SetActive(false);
            ui.przyciskShop.SetActive(false);
            ui.przyciskCredits.SetActive(false);
        }

        PlacementManager placement = Object.FindFirstObjectByType<PlacementManager>();
        if (placement != null)
        {
            placement.StartPlacement(monsterPrefab);
        }
    }
}
