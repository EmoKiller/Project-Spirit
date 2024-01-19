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
    [SerializeField]private Sound soundCurrent;
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
        ObseverConstants.ReloadScene.AddListener(OnLoadScene);
        ObseverConstants.OnSpawnBoss.AddListener(OnSpawnBoss);
        ObseverConstants.OnBossDeath.AddListener(OnBossDeath);
    }
    private void Start()
    {
    }
    private void OnLoadScene()
    {
        StopListSound();
        Play("OnPlayDead",true);
    }
    private void OnSpawnBoss()
    {
        StopListSound();
        float length = 0;
        Play("OnBossSpawn1", ref length);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListBoss(); }));
    }
    private void OnBossDeath()
    {
        StopListSound();
        PlayListOnRound();
    }
    public void PlayListBoss()
    {
        float length = 0;
        string randomSound = UnityEngine.Random.Range(1, 4).ToString();
        Play("OnBossSpawn" + randomSound, ref length);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListBoss(); }));
    }
    public void PlayListOnRound()
    {
        float length = 0;
        int randomSound = UnityEngine.Random.Range(0, 5);
        Play(((ListAudioOnRound)randomSound).ToString(), ref length);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListOnRound(); }));
    }
    public void PlayListShop()
    {
        float length = 0;
        int randomSound = UnityEngine.Random.Range(0, 2);
        Play(((ListAudioShop)randomSound).ToString(), ref length);
        CoroutineSound = StartCoroutine(GameUtilities.IEDelayCall(length, () => { PlayListShop(); }));
    }
    public void StopListSound()
    {
        if (CoroutineSound != null)
        {
            StopCoroutine(CoroutineSound);
            Debug.Log("StopCoroutine");
        }
        soundCurrent.source.Stop();
    }


    public void Play(string name,bool setCurrentSound = false)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "NotFound");
            return;
        }
        s.source.Play();
        if (setCurrentSound == true)
            soundCurrent = s;
    }
    public void Play(string name, ref float lengthClip)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "NotFound");
            return;
        }
        s.source.Play();
        soundCurrent = s;
        lengthClip = s.source.clip.length;
    }

}
