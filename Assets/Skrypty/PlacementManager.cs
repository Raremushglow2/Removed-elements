using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public LayerMask maskaWyspy;
    public bool isPlacing = false;
    public GameObject currentGhost;
    public GameObject shopPanel;

    public void StartPlacement(GameObject prefab)
    {
        if (shopPanel != null) shopPanel.SetActive(false); //

        // Zamiast tagów, po prostu niszczymy poprzedniego ducha, jeœli istnieje
        if (isPlacing && currentGhost != null)
        {
            Destroy(currentGhost);
        }

        currentGhost = Instantiate(prefab);
        isPlacing = true;

        // WA¯NE: Wy³¹czamy MonsterSinger, ¿eby nie gra³ w rêce!
        currentGhost.GetComponent<MonsterSinger>().enabled = false;

        Collider2D col = currentGhost.GetComponent<Collider2D>(); //
        if (col != null) col.enabled = false; //
    }

    void Update()
    {
        if (!isPlacing || currentGhost == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        currentGhost.transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            // Klikniêcie lewym przyciskiem stawia potwora
            PlaceMonster();
        }

        // OPCJONALNIE: Klikniêcie prawym przyciskiem anuluje zakup
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentGhost);
            isPlacing = false;
        }
    }

    void PlaceMonster()
    {
        // W£¥CZAMY SKRYPTY dopiero po postawieniu
        MonsterSinger singer = currentGhost.GetComponent<MonsterSinger>();
        if (singer != null) singer.enabled = true;

        Collider2D col = currentGhost.GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        isPlacing = false;
        currentGhost = null; // Czyœcimy referencjê, ¿eby nastêpny zakup by³ czysty
        Debug.Log("Potwór postawiony pomyœlnie!");
    }
}
