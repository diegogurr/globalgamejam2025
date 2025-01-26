using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public List<Sound> sounds;
    private Dictionary<string, Sound> soundDictionary;
    private AudioSource sfxSource;
    private AudioSource loopSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        soundDictionary = new Dictionary<string, Sound>();

        foreach (Sound sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary.Add(sound.name, sound);
            }
        }
    }

    public void PlaySoundSFX(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            sfxSource.pitch = Random.Range(minPitch, maxPitch);
            sfxSource.PlayOneShot(soundDictionary[soundName].clip, soundDictionary[soundName].volume);
        }
        else
        {
            Debug.LogWarning("AudioManager: Sound " + soundName + " not found!");
        }
    }

    public void PlayLoopingSound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            loopSource.clip = soundDictionary[soundName].clip;
            loopSource.volume = soundDictionary[soundName].volume;
            if (!loopSource.isPlaying)
            {
                loopSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioManager: Sound " + soundName + " not found!");
        }
    }

    public void StopLoopingSound()
    {
        if (loopSource.isPlaying)
        {
            loopSource.Stop();
        }
    }
}
