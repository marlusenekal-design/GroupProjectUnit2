using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // This allows other scripts to easily find the SoundManager instantly
    public static SoundManager Instance;

    [Header("Audio Speakers")]
    public AudioSource musicSource; // Used for background tracks
    public AudioSource sfxSource;   // Used for quick sound effects

    [Header("Background Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;
    public AudioClip creditsMusic;

    [Header("Sound Effect Clips")]
    public AudioClip playerShootSFX;
    public AudioClip playerDamageSFX;
    public AudioClip playerDeathSFX;
    public AudioClip powerUpSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip exploderBoomSFX;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 

            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        
        if (scene.name == "Gameplay Scene")
        {
            PlayGameplayMusic();
        }
        else if (scene.name == "MainMenu")
        {
            PlayMainMenuMusic();
        }
    }


    
    //  MUSIC FUNCTIONS 
    

    public void PlayMainMenuMusic() { PlayMusicTrack(mainMenuMusic); }
    public void PlayGameplayMusic() { PlayMusicTrack(gameplayMusic); }
    public void PlayGameOverMusic() { PlayMusicTrack(gameOverMusic); }
    public void PlayCreditsMusic() { PlayMusicTrack(creditsMusic); }

    private void PlayMusicTrack(AudioClip clip)
    {
        if (clip == null) return;

        
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    

    public void PlayPlayerShoot() { PlaySFX(playerShootSFX); }
    public void PlayPlayerDamage() { PlaySFX(playerDamageSFX); }
    public void PlayPlayerDeath() { PlaySFX(playerDeathSFX); }
    public void PlayPowerUp() { PlaySFX(powerUpSFX); }
    public void PlayEnemyDeath() { PlaySFX(enemyDeathSFX); }
    public void PlayExploderBoom() { PlaySFX(exploderBoomSFX); }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            
            Debug.Log("Sound effect requested, but no audio file is assigned yet.");
        }
    }
}
