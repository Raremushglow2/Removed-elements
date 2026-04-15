using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SelectorManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public LayerMask monsterLayer;
    public GameObject nameMenu;
    public TextMeshProUGUI nameText;
    private GameObject selectedMonster;

    void Update()
    {
        // 1. Jeœli stawiamy potwora, nie pozwól na wybieranie
        if (placementManager != null && placementManager.isPlacing) return;

        // 2. NOWOŒÆ: Jeœli dopiero co puœciliœmy przycisk po postawieniu, 
        // zignoruj tê klatkê, ¿eby menu nie wyskoczy³o od razu
        if (Input.GetMouseButtonUp(0)) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            CheckClick();
        }
    }
    // Ta funkcja pozwoli innym skryptom ukryæ menu wyboru
    public void CloseSelectorMenu()
    {
        if (nameMenu != null)
        {
            nameMenu.SetActive(false);
            selectedMonster = null; // Opcjonalnie: czyœcimy zaznaczenie
        }
    }
    // Dodaj tê funkcjê w SelectorManager.cs
    public void CollectSelected()
    {
        if (selectedMonster != null)
        {
            // Zak³adamy, ¿e skrypt z zarabianiem nazywa siê "Monster"
            Monster monsterScript = selectedMonster.GetComponent<Monster>();
            if (monsterScript != null)
            {
                monsterScript.CollectFromThisMonster();
            }
        }
    }
    void CheckClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, monsterLayer);

        if (hit.collider != null)
        {
            selectedMonster = hit.collider.gameObject;
            nameMenu.SetActive(true);

            // 1. Próbujemy pobraæ skrypt MonsterSinger z potwora
            MonsterSinger singer = selectedMonster.GetComponent<MonsterSinger>();

            if (singer != null)
            {
                // 2. Jeœli skrypt istnieje, bierzemy nazwê z pola "Monster Name"
                nameText.text = singer.monsterName;
            }
            else
            {
                // Failsafe: Jeœli zapomnisz dodaæ skryptu, weŸmie nazwê obiektu
                nameText.text = selectedMonster.name.Replace("(Clone)", "").Trim();
            }

            Debug.Log("Wybrano potwora: " + nameText.text);
        }
        else
        {
            nameMenu.SetActive(false);
        }
    }

    public void DeleteMonster()
    {
        if (selectedMonster != null)
        {
            Destroy(selectedMonster);
            nameMenu.SetActive(false);
            selectedMonster = null;
        }
    }
}
