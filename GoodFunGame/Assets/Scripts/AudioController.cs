using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SFX
{
    Button,
    ShopBuy,
    PlayerAttack,
    StageClear,
    GravityField,
    ReflectShield,
}
public class AudioController : MonoBehaviour
{

    #region Singleton

    public static AudioController Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<AudioController>();
            return instance;
        }
        set => instance = value;
    }
    private static AudioController instance;

    #endregion

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private int sfxCursor;

    void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);

        // Scene Initialize.
        SceneManager.sceneLoaded += InitializeOnSceneLoaded;
    }

    public void InitializeOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScene")
        {
            bgmPlayer.clip = bgmClips[0];
            bgmPlayer.Play();
        }
        else if (scene.name == "MainScene")
        {
            bgmPlayer.clip = bgmClips[1];
            bgmPlayer.Play();
        }
        else if (scene.name == "GameScene")
        {
            bgmPlayer.clip = bgmClips[1];
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }


    public void SFXPlay(SFX type)
    {
        switch (type)
        {
            case SFX.Button:
                sfxPlayer[sfxCursor].clip = sfxClips[0];
                break;
            case SFX.ShopBuy:
                sfxPlayer[sfxCursor].clip = sfxClips[1];
                break;
            case SFX.PlayerAttack:
                sfxPlayer[sfxCursor].clip = sfxClips[2];
                break;
            case SFX.StageClear:
                sfxPlayer[sfxCursor].clip = sfxClips[3];
                break;
            case SFX.GravityField:
                sfxPlayer[sfxCursor].clip = sfxClips[4];
                break;
            case SFX.ReflectShield:
                sfxPlayer[sfxCursor].clip = sfxClips[5];
                break;


        }

        sfxPlayer[sfxCursor].Play();
        sfxCursor++;
        if (sfxCursor == sfxPlayer.Length) sfxCursor = 0;
    }
}
