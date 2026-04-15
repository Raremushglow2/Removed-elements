using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    // Referencja do panelu z napisami
    public GameObject creditsPanel;

    // Metoda wywo³ywana po klikniêciu przycisku "Credits"
    public void OpenCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true); // W³¹cza panel
        }
    }

    // Metoda wywo³ywana po klikniêciu przycisku "Zamknij"
    public void CloseCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false); // Wy³¹cza panel
        }
    }
}
