using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource loopSource;
    public AudioClip introClip;
    public AudioClip loopClip;
    public float delay = -0.2f;
    
    private void Start()
    {
        loopClip.LoadAudioData();
        introSource.clip = introClip;
        introSource.loop = false;
        introSource.Play();
        //musicSource.PlayOneShot(introClip, musicSource.volume);

        Invoke("PlayLoop", introClip.length + delay);
    }

    private void PlayLoop()
    {
        //introSource.Stop();
        loopSource.clip = loopClip;
        loopSource.loop = true;
        loopSource.Play();
    }


    public void StopMusic()
    {
        introSource.Stop();
        loopSource.Stop();
    }

}
