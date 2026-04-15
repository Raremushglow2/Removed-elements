using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Ustawienia Zarobków")]
    public float earningRate = 10f; // Ile zarabia
    public float timeInterval = 60f; // W jakim czasie (np. 60s)
    public float maxAnomalies = 100f; // Limit potwora

    [Header("Stan Aktualny")]
    public float currentAnomalies = 0f;

    void Update()
    {
        if (currentAnomalies < maxAnomalies)
        {
            // Obliczamy przyrost na klatkê: (zarobek / czas) * czas klatki
            float gain = (earningRate / timeInterval) * Time.deltaTime;
            currentAnomalies = Mathf.Min(currentAnomalies + gain, maxAnomalies);
        }
    }
    public void CollectFromThisMonster()
    {
        float amount = Mathf.Floor(currentAnomalies);
        currentAnomalies -= amount;
        CurrencyManager.Instance.totalAnomalies += amount;
    }
    public float Collect()
    {
        float toReturn = Mathf.Floor(currentAnomalies); // Zbieramy tylko pe³ne jednostki
        currentAnomalies -= toReturn;
        return toReturn;
    }
}
