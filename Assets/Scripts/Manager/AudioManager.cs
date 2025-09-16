using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource songSource;
    public AudioSource sfxSource;

    public AudioClip groundTheme;
    public AudioClip undergroundTheme;
    public AudioClip castleTheme;

    public AudioClip marioDie;
    public AudioClip stageClear;
    public AudioClip castleClear;
    public AudioClip win;
    public AudioClip gameOver;
    public AudioClip _1up;
    public AudioClip bowserFall;
    public AudioClip bowserFire;
    public AudioClip coin;
    public AudioClip shootFireBall;
    public AudioClip flagPole;
    public AudioClip jump;
    public AudioClip pauseGame;
    public AudioClip pipe;
    public AudioClip powerUp;
    public AudioClip powerUpAppear;
    public AudioClip stomp;
    public AudioClip vine;

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
