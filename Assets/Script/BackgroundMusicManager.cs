using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    
    [Range(0,1f)]
    public float volumeTrack;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volumeTrack;
        audioSource.clip = backgroundMusic;
        audioSource.Play();

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        Destroy(gameObject);
    }
}