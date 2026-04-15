using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Collections.Generic;

public class CircleSwitcher : MonoBehaviour
{
    private SpriteResolver resolver;

    // Lista nazw twoich obrazków w kolejnoœci, w jakiej maj¹ siê zmieniaæ
    private string[] labels = { "full", "medium", "small" };
    private int currentIndex = 0;

    void Start()
    {
        resolver = GetComponent<SpriteResolver>();

        // Uruchamia funkcjê "NextSprite" za 2 sekundy i powtarza j¹ co 2 sekundy
        InvokeRepeating("NextSprite", 1.0f, 1.0f);
    }

    void NextSprite()
    {
        if (resolver != null)
        {
            // Zmieniamy index na kolejny
            currentIndex++;

            // Jeœli dojdziemy do koñca listy, wracamy do pocz¹tku (pêtla)
            if (currentIndex >= labels.Length)
            {
                currentIndex = 0;
            }

            // Wywo³ujemy zmianê obrazka
            ChangeState(labels[currentIndex]);
        }
    }

    public void ChangeState(string label)
    {
        if (resolver != null)
        {
            resolver.SetCategoryAndLabel("Alternizator", label);
            resolver.ResolveSpriteToSpriteRenderer();
            Debug.Log("Zmieniono na: " + label); // Zobaczysz to w konsoli
        }
    }
}
