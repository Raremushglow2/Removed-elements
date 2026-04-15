using UnityEngine;

public class XManager : MonoBehaviour
{
    [Header("Elementy do ukrycia")]
    public GameObject mainUIPanel; // Tutaj przeci¹gniesz swój MainUIPanel

    private bool isVisible = true;

    public void ToggleUI()
    {
        // Odwracamy stan zmiennej (jeœli true to false, jeœli false to true)
        isVisible = !isVisible;

        // Ustawiamy aktywnoœæ panelu zgodnie ze zmienn¹
        if (mainUIPanel != null)
        {
            mainUIPanel.SetActive(isVisible);
        }
    }
}
