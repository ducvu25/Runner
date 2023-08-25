using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioSetting
{
    click,
    x, 
    o
}
public class AudioController : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [Header("----------BG---------\n")]
    [SerializeField] AudioClip[] bg;
    AudioSource bgAS;

    [Header("\n----------Effect--------\n")]
    [SerializeField] AudioClip[] effect;
    AudioSource[] effectAS;

    float volume;

    public static AudioController instance;
    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        effectAS = new AudioSource[effect.Length];
        PlaySound(bg[Random.Range(0, bg.Length)], ref bgAS, 1, true, false);
    }
    // Start is called before the first frame update
    public void PlaySound(int i, float volume = 1f, bool loop = false, bool repeat = true)
    {
        PlaySound(effect[i], ref effectAS[i], volume, loop, repeat);
    }

    void PlaySound(AudioClip audioClip,ref AudioSource audioSource, float volume = 1f, bool loop = false, bool repeat = true)
    {
        if(audioSource != null && audioSource.isPlaying && repeat) {
            return;
        }
        audioSource = Instantiate(instance.prefab.GetComponent<AudioSource>());
        audioSource.loop = loop;
        audioSource.volume = volume*this.volume;
        audioSource.clip = audioClip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    public void Setting(float value)
    {
        volume = value;
        bgAS.volume = volume;
    }
}
