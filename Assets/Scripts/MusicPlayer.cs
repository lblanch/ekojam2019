using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource goodmusic;
    public AudioSource badmusic;
    public AudioSource normalmusic;

    

    void Start()
    {
        
        normalmusic.Play(0);
        goodmusic.Pause();
        badmusic.Pause();
        
    }

    public void playnormalmusic()
    {
        normalmusic.Play(0);
        goodmusic.Pause();
        badmusic.Pause();
    }
    public void playgoodmusic()
    {
        normalmusic.Pause();
        goodmusic.Play(0);
        badmusic.Pause();
    }
    public void playbadmusic()
    {
        normalmusic.Pause();
        goodmusic.Pause();
        badmusic.Play(0);
    }
  
}
