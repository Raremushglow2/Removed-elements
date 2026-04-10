using UnityEngine;

public class GhostVisuals : MonoBehaviour
{
    private SpriteRenderer[] renderery;
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color errorColor = new Color(1f, 0f, 0f, 0.5f);

    public void SetValid(bool isValid)
    {
        // Pobierz komponenty, jeúli jeszcze ich nie mamy
        if (renderery == null || renderery.Length == 0)
        {
            renderery = GetComponentsInChildren<SpriteRenderer>();
        }

        // Sprawdü czy cokolwiek znaleüliúmy, øeby uniknπÊ b≥ÍdÛw
        if (renderery == null) return;

        Color targetColor = isValid ? normalColor : errorColor;
        foreach (var sr in renderery)
        {
            if (sr != null) sr.color = targetColor;
        }
    }
}
