using UnityEngine;
using UnityEngine.SceneManagement; // To pozwala nam prze³¹czaæ sceny

public class MenuSkrypt : MonoBehaviour
{
    public void PrzejdzDoGry()
    {
        // Ta linijka szuka sceny o nazwie "Gra" i j¹ w³¹cza
        SceneManager.LoadScene("Gra");
    }
}

