using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] public AudioSource _musicSource, _effectsSource;

    [Header("Music")]
    [SerializeField] AudioClip WelcomePageMusic;
    [SerializeField] AudioClip LevelSelectionPageMusic;
    [SerializeField] AudioClip TutorialLevelMusic;
    [SerializeField] AudioClip MainLevelMusic;
    [SerializeField] AudioClip UpgradeCubePageMusic;

    [Header("SoundEffects")]
    [SerializeField] AudioClip UIClickSound;
    [SerializeField] AudioClip UpgradeCubeSound;
    [SerializeField] AudioClip SpendMoneySound;
    [SerializeField] AudioClip BloodSound;
    [SerializeField] AudioClip PickUpCoinSound;
    [SerializeField] AudioClip EnemyDieSound;
    [SerializeField] AudioClip GameOverSound;

    [Header("Cube Shooting")]
    [SerializeField] AudioClip Cube2ShootingSound;
    [SerializeField] AudioClip Cube4ShootingSound;
    [SerializeField] AudioClip Cube8ShootingSound;
    [SerializeField] AudioClip Cube16ShootingSound;
    [SerializeField] AudioClip Cube16FrozenEffectSound;
    [SerializeField] AudioClip Cube32ShootingSound;
    [SerializeField] AudioClip Cube32FireEffectSound;
    [SerializeField] AudioClip Cube64ShootingSound;
    [SerializeField] AudioClip Cube64HitEnemySound;

    [Header("Enemy Cube")]
    [SerializeField] AudioClip EnemyCube2HitSound;
    [SerializeField] AudioClip EnemyCube4HitSound;
    [SerializeField] AudioClip EnemyCube8HitSound;
    [SerializeField] AudioClip EnemyCube16HitSound;
    [SerializeField] AudioClip EnemyCube16TimedBombSound;
    [SerializeField] AudioClip EnemyCube16ExplosionSound;
    [SerializeField] AudioClip EnemyCube32HitSound;
    [SerializeField] AudioClip EnemyCube32ShootingBubbleSound;
    [SerializeField] AudioClip EnemyCube32BubbleExplosionSound;
    [SerializeField] AudioClip EnemyCube32PoisonSound;
    [SerializeField] AudioClip EnemyCube64HitSound;
    [SerializeField] AudioClip EnemyCube64SpiderNetSound;

    public enum Music 
    {
        WelcomePageMusic,
        LevelSelectionPageMusic,
        TutorialLevelMusic,
        MainLevelMusic,
        UpgradeCubePageMusic,
    }

    public enum SoundEffects 
    {
        UIClickSound,
        UpgradeCubeSound,
        SpendMoneySound,
        Cube2ShootingSound,
        Cube4ShootingSound,
        Cube8ShootingSound,
        Cube16ShootingSound,
        Cube16FrozenEffectSound,
        Cube32ShootingSound,
        Cube32FireEffectSound,
        Cube64ShootingSound,
        Cube64HitEnemySound,
        EnemyCube2HitSound,
        EnemyCube4HitSound,
        EnemyCube8HitSound,
        EnemyCube16HitSound,
        EnemyCube16TimedBombSound,
        EnemyCube16ExplosionSound,
        EnemyCube32HitSound,
        EnemyCube32ShootingBubbleSound,
        EnemyCube32BubbleExplosionSound,
        EnemyCube32PoisonSound,
        EnemyCube64HitSound,
        EnemyCube64SpiderNetSound,
        BloodSound,
        PickUpCoinSound,
        EnemyDieSound,
        GameOverSound,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(Music.WelcomePageMusic);
    }
    private void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                PlayMusic(Music.WelcomePageMusic);
                break;
            case 1:
                PlayMusic(Music.LevelSelectionPageMusic);
                break;
            case 2:
                PlayMusic(Music.TutorialLevelMusic);
                break;
            case 3:
                PlayMusic(Music.MainLevelMusic);
                break;
            case 4:
                PlayMusic(Music.UpgradeCubePageMusic);
                break;
        }
    }

    public void PlaySound(SoundEffects soundEffectName)
    {
        AudioClip clip = FindSoundEffect(soundEffectName);
        _effectsSource.PlayOneShot(clip);
    }

    public void PlayMusic(Music musicName)
    {
        AudioClip clip = FindMusic(musicName);
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    private AudioClip FindMusic(Music musicName)
    {
        switch (musicName)
        {
            case Music.WelcomePageMusic:
                return WelcomePageMusic;
            case Music.LevelSelectionPageMusic:
                return LevelSelectionPageMusic;
            case Music.TutorialLevelMusic:
                return TutorialLevelMusic;
            case Music.MainLevelMusic:
                return MainLevelMusic;
            case Music.UpgradeCubePageMusic:
                return UpgradeCubePageMusic;
            default:
                return null;
        }
    }

    private AudioClip FindSoundEffect(SoundEffects soundEffectName)
    {
        switch (soundEffectName)
        {
            case SoundEffects.UIClickSound:
                return UIClickSound;
            case SoundEffects.UpgradeCubeSound:
                return UpgradeCubeSound;
            case SoundEffects.SpendMoneySound:
                return SpendMoneySound;
            case SoundEffects.Cube2ShootingSound:
                return Cube2ShootingSound;
            case SoundEffects.Cube4ShootingSound:
                return Cube4ShootingSound;
            case SoundEffects.Cube8ShootingSound:
                return Cube8ShootingSound;
            case SoundEffects.Cube16ShootingSound:
                return Cube16ShootingSound;
            case SoundEffects.Cube32ShootingSound:
                return Cube32ShootingSound;
            case SoundEffects.Cube64ShootingSound:
                return Cube64ShootingSound;
            case SoundEffects.Cube16FrozenEffectSound:
                return Cube16FrozenEffectSound;
            case SoundEffects.Cube32FireEffectSound:
                return Cube32FireEffectSound;
            case SoundEffects.Cube64HitEnemySound:
                return Cube64HitEnemySound;
            case SoundEffects.EnemyCube2HitSound:
                return EnemyCube2HitSound;
            case SoundEffects.EnemyCube4HitSound:
                return EnemyCube4HitSound;
            case SoundEffects.EnemyCube8HitSound:
                return EnemyCube8HitSound;
            case SoundEffects.EnemyCube16HitSound:
                return EnemyCube16HitSound;
            case SoundEffects.EnemyCube16TimedBombSound:
                return EnemyCube16TimedBombSound;
            case SoundEffects.EnemyCube16ExplosionSound:
                return EnemyCube16ExplosionSound;
            case SoundEffects.EnemyCube32HitSound:
                return EnemyCube32HitSound;
            case SoundEffects.EnemyCube32ShootingBubbleSound:
                return EnemyCube32ShootingBubbleSound;
            case SoundEffects.EnemyCube32BubbleExplosionSound:
                return EnemyCube32BubbleExplosionSound;
            case SoundEffects.EnemyCube32PoisonSound:
                return EnemyCube32PoisonSound;
            case SoundEffects.EnemyCube64HitSound:
                return EnemyCube64HitSound;
            case SoundEffects.EnemyCube64SpiderNetSound:
                return EnemyCube64SpiderNetSound;
            case SoundEffects.BloodSound:
                return BloodSound;
            case SoundEffects.PickUpCoinSound:
                return PickUpCoinSound;
            case SoundEffects.EnemyDieSound:
                return EnemyDieSound;
            case SoundEffects.GameOverSound:
                return GameOverSound;
            default:
                return null;
        }
    }

}
