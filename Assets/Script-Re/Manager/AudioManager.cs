using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class AudioManager : SerializedMonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public Dictionary<TypeListAudio, Sound[]> audioSources;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach (Sound item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }
    private void Start()
    {
        //CheckScene("MainMenu", "MainMenu");
    }
    public void PlayList<T>(T listPlay) where T : Enum
    {
        float length = 0;
        Play(listPlay.ToString(),ref length);
        //Debug.Log(length);
        ListAudioShop a;
        if (Enum.TryParse(listPlay.ToString(), out a))
        {
            Debug.Log(listPlay);
        }
        //this.DelayCall(length, () =>
        //{
        //    ListAudioShop a;
        //    if (Enum.TryParse(listPlay.ToString(),out a))
        //    {
        //        Debug.Log(listPlay);
        //    }
        //});
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "NotFound");
            return;
        }
        s.source.Play();
    }
    public void Play(string name,ref float lengthClip)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "NotFound");
            return;
        }
        s.source.Play();
        lengthClip = s.source.clip.length;
    }

}
