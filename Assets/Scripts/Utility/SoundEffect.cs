using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField, Range(0, 1)]
    private float volume = 1;

    public void Play() {
        AudioSource AS = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
        if (AS) AS.PlayOneShot(clip, volume);
        else Debug.LogWarning("No audio source to play clip from");
    }
}