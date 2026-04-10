using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    private GameObject currentGhost;
    public LayerMask maskaWyspy;

    // Tutaj poprawiamy b³¹d:
    private bool isPlacing = false;

    public void StartPlacement(GameObject prefab)
    {
        monsterPrefab = prefab;
        currentGhost = Instantiate(prefab);
        isPlacing = true;

        // £¹czymy siê z UI i chowamy wszystko
        UIManager ui = Object.FindFirstObjectByType<UIManager>();
        if (ui != null)
        {
            ui.shopPanel.SetActive(false);
            ui.przyciskShop.SetActive(false);
            ui.przyciskCredits.SetActive(false);
        }
    }

    void Update()
    {
        if (!isPlacing || currentGhost == null) return;

        // Poruszanie duchem za myszk¹
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 5;
        currentGhost.transform.position = mousePos;

        // Sprawdzanie czy pod myszk¹ jest wyspa
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0.1f, maskaWyspy);

        // Pobieramy skrypt wizualny ducha (ten co go nie mog³eœ dodaæ wczeœniej)
        GhostVisuals gv = currentGhost.GetComponent<GhostVisuals>();

        if (hit.collider != null)
        {
            if (gv != null) gv.SetValid(true); // Robi siê normalny
            if (Input.GetMouseButtonDown(0)) PlaceMonster();
        }
        else
        {
            if (gv != null) gv.SetValid(false); // Robi siê czerwony
        }
    }

    void PlaceMonster()
    {
        // 1. Zabieramy skrypt GhostVisuals, ¿eby przesta³ mrugaæ na czerwono/bia³o
        GhostVisuals gv = currentGhost.GetComponent<GhostVisuals>();
        if (gv != null) Destroy(gv);

        // 2. Resetujemy kolor na pe³ny (na wszelki wypadek)
        SpriteRenderer sr = currentGhost.GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;

        // 3. Roz³¹czamy currentGhost, ¿eby skrypt zapomnia³ o tym postawionym
        isPlacing = false;
        currentGhost = null;

        // 4. Pokazujemy UI
        UIManager ui = Object.FindFirstObjectByType<UIManager>();
        if (ui != null)
        {
            ui.przyciskShop.SetActive(true);
            ui.przyciskCredits.SetActive(true);
        }
    }
}
