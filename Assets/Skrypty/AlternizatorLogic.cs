using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AlternizatorLogic : MonoBehaviour
{
    [Header("Panele UI")]
    public GameObject mainAlternizatorMenu; //
    public GameObject fusionMenu;
    public Image resultPreview;

    [Header("Dane")]
    public AlternizatorData data;
    public PlacementManager placementManager;
    public Sprite emptyPreviewSprite;

    private List<string> currentAddedElements = new List<string>();
    private GameObject currentResultPrefab;

    public void OpenFusionMenu()
    {
        // Ukrywamy g³ówne menu (jeœli masz np. wybór trybów)
        if (mainAlternizatorMenu != null) mainAlternizatorMenu.SetActive(false);

        if (fusionMenu != null)
        {
            fusionMenu.SetActive(true);
            // WYWO£UJEMY RESET: Czyœcimy stare sk³adniki i kolory przycisków
            ResetFusion();
        }
    }

    public void AddElement(string name)
    {
        if (!currentAddedElements.Contains(name))
        {
            currentAddedElements.Add(name);
            Debug.Log("<color=green>Dodano: </color>" + name);
            CheckCombination();
        }
    }

    public void RemoveElement(string name)
    {
        if (currentAddedElements.Contains(name))
        {
            currentAddedElements.Remove(name);
            Debug.Log("<color=red>Usuniêto: </color>" + name);
            CheckCombination();
        }
    }
    void Start()
    {
        // Jeœli zapomnia³eœ przeci¹gn¹æ 'Data' w inspektorze, 
        // skrypt sam go poszuka na tym samym obiekcie.
        if (data == null)
        {
            data = GetComponent<AlternizatorData>();
            if (data != null)
            {
                Debug.Log("<color=green>Sukces: Automatycznie podpiêto AlternizatorData!</color>");
            }
        }
    }
  
    void CheckCombination()
    {
        currentResultPrefab = null;
        if (data == null)
        {
            Debug.LogError("B£¥D: Pole 'Data' w AlternizatorLogic jest puste!");
            return;
        }

        // Ten log MUSI siê pojawiæ, jeœli funkcja dzia³a
        Debug.Log($"<color=orange>Logika: Mam {currentAddedElements.Count} elementów na liœcie.</color>");

        foreach (var recipe in data.recipes)
        {
            // Sprawdzamy czy liczba siê zgadza
            if (recipe.requiredElements.Count == currentAddedElements.Count)
            {
                int matches = 0;
                foreach (string req in recipe.requiredElements)
                {
                    if (currentAddedElements.Exists(e => e.Trim().ToLower() == req.Trim().ToLower()))
                    {
                        matches++;
                    }
                }

                if (matches == recipe.requiredElements.Count)
                {
                    currentResultPrefab = recipe.resultPrefab;
                    Debug.Log("<color=cyan>ZNALAZ£EM: </color>" + recipe.recipeName);
                    break;
                }
                else
                {
                    Debug.Log($"Receptura {recipe.recipeName} nie pasuje (Tylko {matches} z {recipe.requiredElements.Count} sk³adników)");
                }
            }
        }
        UpdatePreviewImage();
    }

    void UpdatePreviewImage()
    {
        if (resultPreview == null) return;

        GameObject obj = (currentResultPrefab != null) ? currentResultPrefab :
                        (currentAddedElements.Count > 0 ? data.defaultFailPrefab : null);

        if (obj != null)
        {
            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                resultPreview.sprite = sr.sprite;
                resultPreview.color = Color.white;
            }
        }
        else
        {
            resultPreview.sprite = emptyPreviewSprite;
            resultPreview.color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void ResetFusion()
    {
        currentAddedElements.Clear(); // To jest kluczowe!
        currentResultPrefab = null;

        // Resetujemy fizycznie przyciski na scenie
        AlternizatorButton[] buttons = FindObjectsOfType<AlternizatorButton>();
        foreach (var btn in buttons)
        {
            btn.ResetButton(); // Przywraca kolory i stan isSelected = false
        }

        UpdatePreviewImage();
        Debug.Log("<color=yellow>System zresetowany i gotowy na now¹ fuzjê.</color>");
    }

    public void OnPlaceButtonClicked()
    {
        // 1. Zapamiêtaj co chcesz postawiæ w lokalnej zmiennej
        GameObject toPlace = (currentResultPrefab != null) ? currentResultPrefab : data.defaultFailPrefab;

        if (toPlace != null && placementManager != null)
        {
            // 2. Najpierw wyœlij do managera
            placementManager.StartPlacement(toPlace);
            Debug.Log("<color=white>Wys³ano do postawienia: </color>" + toPlace.name);

            // 3. Dopiero teraz zamknij i zresetuj
            if (fusionMenu != null) fusionMenu.SetActive(false);
            ResetFusion();
        }
    }
}
