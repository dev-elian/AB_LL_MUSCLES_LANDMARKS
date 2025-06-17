using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    AudioSource _source;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip) {
        if (_source.isPlaying)
            _source.Stop();
        _source.PlayOneShot(clip);
    }
}
