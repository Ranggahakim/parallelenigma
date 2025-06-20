using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("----- Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clip-----")]
    public AudioClip background;
    public AudioClip sfxCollectItem;
    public AudioClip sfxGameOver;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

}
