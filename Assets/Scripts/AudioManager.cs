using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;

    public AudioSource audioSource;

    private int musicIndex;

    public static AudioManager instance;

    /* Permet d'ajouter dans un mixer afin de séparer les sons */
    public AudioMixerGroup soundEffectMixer;

    public void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Déjà un AudioManager");
            return;
        }

        instance = this;
    }
     
    public void Start()
    {
        musicIndex = 0;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public void Update()
    {
        /* Si on ne joue plus de musique on change de musique */
        if (!audioSource.isPlaying)
            playNextSong();
    }

    private void playNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;

        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 position)
    {
        GameObject tempGameObject = new GameObject("TempAudio");
        tempGameObject.transform.position = position;

        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;

        audioSource.Play();
        Destroy(tempGameObject, clip.length);

        return audioSource;
    }
}
