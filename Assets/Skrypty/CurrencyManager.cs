using UnityEngine;
using TMPro; // Pamiêtaj o TextMeshPro!

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance; // Singleton dla ³atwego dostêpu

    [Header("Ekonomia")]
    public double totalAnomalies = 0;
    public TextMeshProUGUI currencyText; // Przeci¹gnij tutaj swój tekst z UI

    void Awake()
    {
        Instance = this;
    }

    // W CurrencyManager.cs w metodzie Update
    void Update()
    {
        // Tylko liczba, bez ¿adnych dodatkowych tekstów
        currencyText.text = Mathf.Floor((float)totalAnomalies).ToString();
    }

    public void CollectAllAnomalies()
    {
        // Znajduje wszystkie potwory na scenie
        Monster[] allMonsters = FindObjectsOfType<Monster>();
        float collectedThisTurn = 0;

        foreach (Monster monster in allMonsters)
        {
            collectedThisTurn += monster.Collect();
        }

        totalAnomalies += collectedThisTurn;
        Debug.Log("Zebrano: " + collectedThisTurn);
    }
}
