using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class MonsterSinger : MonoBehaviour
{
    [Header("Informacje")]
    public string monsterName = "Œpiewak";

    [Header("Muzyka")]
    public AudioSource audioSource;
    public List<float> activationTimes = new List<float>();

    [Header("Œwiat³o")]
    public Light2D monsterLight;
    public float maxIntensity = 2f;
    public float pulseSpeed = 5f;

    private HashSet<int> playedInCurrentLoop = new HashSet<int>();

    void Update()
    {
        // Sprawdzamy, czy conductor istnieje i czy obiekt nie jest "duchem" w sklepie
        if (GameConductor.instance == null || !audioSource.enabled) return;

        float currentTime = GameConductor.instance.songTimer;

        // Synchronizacja: ka¿dy stworek patrzy na ten sam zegar
        for (int i = 0; i < activationTimes.Count; i++)
        {
            if (currentTime >= activationTimes[i] && !playedInCurrentLoop.Contains(i))
            {
                float offset = currentTime - activationTimes[i];

                // Graj tylko, jeœli jesteœmy wewn¹trz d³ugoœci klipu
                if (audioSource.clip != null && offset < audioSource.clip.length)
                {
                    audioSource.time = offset; // Synchronizacja dŸwiêku
                    audioSource.Play();
                }
                playedInCurrentLoop.Add(i);
            }
        }

        // Reset pêtli przy starcie utworu
        if (currentTime < 0.1f && playedInCurrentLoop.Count > 0)
        {
            playedInCurrentLoop.Clear();
        }

        HandleGlow();
    }

    void HandleGlow()
    {
        if (audioSource.isPlaying && monsterLight != null)
        {
            float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            monsterLight.intensity = pulse * maxIntensity;
        }
        else if (monsterLight != null)
        {
            monsterLight.intensity = Mathf.MoveTowards(monsterLight.intensity, 0, Time.deltaTime * 5f);
        }
    }
}
