using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance; 

    public Sound[] sounds;

	// Use this for initialization
	void Awake () {

        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
        }


        foreach (Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    public static AudioManager GetInstance(){
        return instance;
    }
	
	
	public void Play (string name) {
       Sound s =  Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
	}

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
