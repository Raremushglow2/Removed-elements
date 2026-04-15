using UnityEngine;

public class buy : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject shopPanel; // Tu przypiszesz okno sklepu

    public void BuyMonster()
    {
        Debug.Log("Próba zakupu potwora...");
        PlacementManager placement = Object.FindFirstObjectByType<PlacementManager>();

        if (placement != null)
        {
            placement.StartPlacement(monsterPrefab);


            if (shopPanel != null)
            {
                shopPanel.SetActive(false);
                Debug.Log("Sklep zamkniêty!");
            }
            else
            {
                Debug.LogWarning("Nie przypisa³eœ ShopPanel w Inspektorze!");
            }
        }
        else
        {
            Debug.LogError("Nie znaleziono PlacementManager na scenie!");
        }
    }
}
