using UnityEngine;

public class GameConductor : MonoBehaviour
{
    public static GameConductor instance;

    public float songTimer = 0f;
    public float loopLength = 90f; // 1:30 minuty

    void Awake() { instance = this; }

    void Update()
    {
        songTimer += Time.deltaTime;

        if (songTimer >= loopLength)
        {
            songTimer = 0f; // Reset pêtli
        }
    }
}