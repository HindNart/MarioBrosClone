using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource songSource;
    public AudioSource sfxSource;

    [SerializeField] private AudioClip groundTheme;
    [SerializeField] private AudioClip undergroundTheme;
    [SerializeField] private AudioClip castleTheme;

    [SerializeField] private AudioClip marioDie;
    [SerializeField] private AudioClip stageClear;
    [SerializeField] private AudioClip castleClear;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip _1up;
    [SerializeField] private AudioClip bowserFall;
    [SerializeField] private AudioClip bowserFire;
    [SerializeField] private AudioClip coin;
    [SerializeField] private AudioClip shootFireBall;
    [SerializeField] private AudioClip flagPole;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip pauseGame;
    [SerializeField] private AudioClip pipe;
    [SerializeField] private AudioClip powerUp;
    [SerializeField] private AudioClip powerUpAppear;
    [SerializeField] private AudioClip stomp;
    [SerializeField] private AudioClip vine;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayGroundSong()
    {
        songSource.clip = groundTheme;
        songSource.Play();
    }

    public void PlayUndergroundSong()
    {
        songSource.clip = undergroundTheme;
        songSource.Play();
    }

    public void PlayCastleSong()
    {
        songSource.clip = castleTheme;
        songSource.Play();
    }

    public void PlayMarioDieSound()
    {
        sfxSource.PlayOneShot(marioDie);
    }

    public void PlayStageClearSound()
    {
        sfxSource.PlayOneShot(stageClear);
    }

    public void PlayCastleClearSound()
    {
        sfxSource.PlayOneShot(castleClear);
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(win);
    }

    public void PlayGameOverSound()
    {
        sfxSource.PlayOneShot(gameOver);
    }

    public void Play1upSound()
    {
        sfxSource.PlayOneShot(_1up);
    }

    public void PlayBowserFallSound()
    {
        sfxSource.PlayOneShot(bowserFall);
    }

    public void PlayBowserFireSound()
    {
        sfxSource.PlayOneShot(bowserFire);
    }

    public void PlayCoinSound()
    {
        sfxSource.PlayOneShot(coin);
    }

    public void PlayShootFireBallSound()
    {
        sfxSource.PlayOneShot(shootFireBall);
    }

    public void PlayFlagPoleSound()
    {
        sfxSource.PlayOneShot(flagPole);
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jump);
    }

    public void PlayPauseSound()
    {
        sfxSource.PlayOneShot(pauseGame);
    }

    public void PlayPipeSound()
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(pipe);
        }
    }

    public void PlayPowerUpSound()
    {
        sfxSource.PlayOneShot(powerUp);
    }

    public void PlayPowerUpAppearSound()
    {
        sfxSource.PlayOneShot(powerUpAppear);
    }

    public void PlayStompSound()
    {
        sfxSource.PlayOneShot(stomp);
    }

    public void PlayVineSound()
    {
        sfxSource.PlayOneShot(vine);
    }

    // public void PauseMusic()
    // {
    //     isMusicOn = !isMusicOn;

    //     if (isMusicOn)
    //     {
    //         musicSource.UnPause();
    //     }
    //     else
    //     {
    //         musicSource.Pause();
    //     }
    // }

    // public void PauseSFX()
    // {
    //     isSfxOn = !isSfxOn;

    //     if (isSfxOn)
    //     {
    //         sfxSource.UnPause();
    //     }
    //     else
    //     {
    //         sfxSource.Pause();
    //     }
    // }
}
