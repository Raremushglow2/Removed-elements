using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AlternizeRecipe
{
    public string recipeName;
    public List<string> requiredElements; // np. "Ziemia", "Zimno"
    public GameObject resultPrefab;      // Co powstanie
    public float creationTime;           // Czas w sekundach
}

public class AlternizatorData : MonoBehaviour
{
    [Header("Ustawienia Fuzji")]
    public GameObject defaultFailPrefab; // Prefab, gdy kombinacja nie istnieje

    [Header("Lista Receptur")]
    public List<AlternizeRecipe> recipes = new List<AlternizeRecipe>();
}
