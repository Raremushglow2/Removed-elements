using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Wyœwietla informacjê w konsoli (przydatne do testów w edytorze)
        Debug.Log("Gracz wyszed³ z gry!");

        // Zamyka aplikacjê
        Application.Quit();
    }
}
