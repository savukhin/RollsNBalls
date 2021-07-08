using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private AudioSource audioSource;
    public Song[] songs;
    public HUDController HUD;
    public bool muted = false;
    
    public void Play()
    {
        if (!muted) {}
        ChangeVolume(1f);
        //if (!audioSource.clip)
        Song song = Next();
        audioSource.Play();
        Invoke("Next", audioSource.clip.length);
        HUD.updateSong(song.title, song.author);
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    public void Pause()
    {
        audioSource.Pause();
        print("Len = " + audioSource.clip.length);
    }

    public void Resume()
    {
        audioSource.Play();
        print("Len = " + audioSource.clip.length);
    }

    public void Stop()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public Song Next()
    {
        Song song = songs[Random.Range(0, songs.Length)];
        audioSource.clip = song.clip;
        return song;
    }

    public void Mute()
    {
        muted = true;
        Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
