using UnityEngine;

public class GameConductor : MonoBehaviour
{
    // TO JEST TA BRAKUJ¥CA LINIA:
    public static GameConductor instance;

    public static float globalTimer;
    public float cycleLength = 105f;

    void Awake()
    {
        // Przypisujemy tego konkretnego dyrygenta do wspólnej zmiennej
        instance = this;
    }

    void Update()
    {
        globalTimer += Time.deltaTime;
        if (globalTimer >= cycleLength) globalTimer = 0;
    }
}
