using UnityEngine;
using UnityEngine.Rendering.Universal; // Wymagane dla œwiate³ 2D

public class MonsterMusicAI : MonoBehaviour
{
    public AudioSource audioSource;
    public Light2D monsterLight; // Tu przeci¹gniesz swoje œwiat³o
    public float startPlayingAt = 15f; 
    
    [Header("Ustawienia œwiecenia")]
    public float maxIntensity = 2f; // Jak mocno ma œwieciæ
    public float pulseSpeed = 5f;   // Jak szybko ma pulsowaæ

    private bool isCurrentlyPlaying = false;

    void Update()
    {
        if (GameConductor.instance == null) return;

        float currentTime = GameConductor.globalTimer;
        float songDuration = audioSource.clip.length * 2;
        float endPlayingAt = startPlayingAt + songDuration;

        if (currentTime >= startPlayingAt && currentTime < endPlayingAt)
        {
            if (!isCurrentlyPlaying) StartPlaying(currentTime - startPlayingAt);
            
            // EFEKT ŒWIECENIA: Pulsuje w rytm pêtli
            // U¿ywamy Sinusa, ¿eby œwiat³o p³ynnie ros³o i mala³o
            float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f; 
            monsterLight.intensity = pulse * maxIntensity;
        }
        else
        {
            if (isCurrentlyPlaying) StopPlaying();
        }
    }

    void StartPlaying(float offset)
    {
        isCurrentlyPlaying = true;
        audioSource.time = offset % audioSource.clip.length; 
        audioSource.Play();
    }

    void StopPlaying()
    {
        isCurrentlyPlaying = false;
        audioSource.Stop();
        monsterLight.intensity = 0; // Gasi œwiat³o po piosence
    }
}
