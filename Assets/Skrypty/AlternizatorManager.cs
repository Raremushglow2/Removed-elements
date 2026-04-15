using UnityEngine;
using UnityEngine.EventSystems;

public class AlternizatorManager : MonoBehaviour
{
    public GameObject alternizatorMenu; // Przeci¹gnij tutaj: Alternizator menu
    public GameObject fusionMenu;        // Przeci¹gnij tutaj: AlternizePanel
    public LayerMask alternizatorLayer;  // Warstwa Twojego budynku

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 1. Ignoruj, jeœli klikasz w przyciski UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // 2. SprawdŸ klikniêcie w œwiat gry
            CheckClick();
        }
    }
    void CheckClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Collider2D hit = Physics2D.OverlapPoint(mousePos2D, alternizatorLayer);

        if (hit != null)
        {
            // Klikniêto w budynek -> poka¿ menu g³ówne, ukryj fuzjê
            if (fusionMenu != null) fusionMenu.SetActive(false);
            if (alternizatorMenu != null) alternizatorMenu.SetActive(true);
        }
        else
        {
            // Klikniêto w puste miejsce -> schowaj WSZYSTKO
            if (alternizatorMenu != null) alternizatorMenu.SetActive(false);
            if (fusionMenu != null) fusionMenu.SetActive(false);
        }
    }
}
