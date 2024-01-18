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
    private Sound soundCurrent;
    private Coroutine CoroutineSound;
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
        ObseverConstants.OnClickButtonStart.AddListener(StopListSound);
        ObseverConstants.OnClickButtonContinue.AddListener(StopListSound);
        ObseverConstants.OnClickButtonStart.AddListener(PlayListOnRound);
    }
    private void Start()
    {
    }
    public void PlayListOnRound()
    {
        float length = 0;
        float randomSound = UnityEngine.Random.Range(0, 5);
        Play(((ListAudioOnRound)randomSound).ToString(), ref length, ref soundCurrent);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListOnRound(); }));
    }
    public void PlayListShop()
    {
        float length = 0;
        float randomSound = UnityEngine.Random.Range(0, 2);
        Play(((ListAudioShop)randomSound).ToString(), ref length, ref soundCurrent);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListShop(); }));
    }
    public void StopListSound()
    {
        if (CoroutineSound != null)
        {
            StopCoroutine(CoroutineSound);
        }
        soundCurrent.source.Stop();
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
    public void Play(string name, ref float lengthClip , ref Sound outSound)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "NotFound");
            return;
        }
        s.source.Play();
        outSound = s;
        lengthClip = s.source.clip.length;
    }

}
