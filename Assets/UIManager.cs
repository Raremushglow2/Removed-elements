using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panele")]
    public GameObject shopPanel;
    public GameObject creditsUI;

    [Header("Przyciski G³ówne")]
    public GameObject przyciskShop;
    public GameObject przyciskCredits;

    // --- FUNKCJE DLA SKLEPU ---
    public void OtworzSklep()
    {
        shopPanel.SetActive(true);
    }

    public void ZamknijSklep()
    {
        shopPanel.SetActive(false);
    }

    // --- FUNKCJE DLA CREDITS (Tego Ci brakowa³o!) ---
    public void OtworzCredits()
    {
        creditsUI.SetActive(true);
    }

    public void ZamknijCredits()
    {
        creditsUI.SetActive(false);
    }
}
