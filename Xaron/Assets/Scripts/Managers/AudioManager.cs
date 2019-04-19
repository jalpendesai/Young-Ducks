using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Header("Player")]
    public AudioClip[] walking;
    public AudioClip[] crouching;
    public AudioClip jumping;
    public AudioClip hitPlayer;
    public AudioClip death;
    public AudioClip firing;
    public AudioClip fireReloading;

    [Header("Enemy")]
    public AudioClip hitEnemy;
    public AudioClip deathEnemy;

    [Header("Mixer Groups")]
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup voiceGroup;

    AudioSource playerSource;
    AudioSource voiceSource;

    private void Awake()
    {

        //   Destroy any other AudioManagers
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);

        //  Generating Audio Source "Channels" Game's Audio
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //  Assign AudioSource to its AudioMixer
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;
    }

    public static void PlayWalkingAudio()
    {
        //   If there are no current AudioManager
        //   OR playerSource is already playing then exit
        if(current == null || current.playerSource.isPlaying){
            return;
        }

        int index = Random.Range(0, current.walking.Length);
        current.playerSource.clip = current.walking[index];
        current.playerSource.Play();
    }

    public static void PlayJumpingAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.jumping;
        current.playerSource.Play();
    }

    public static void PlayCrouchingAudio(){
        if(current == null && current.playerSource.isPlaying){
            return;
        }

        int index = Random.Range(0, current.crouching.Length);
        current.playerSource.clip = current.crouching[index];
        current.playerSource.Play();
    }

    public static void PlayPlayerHitAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.hitPlayer;
        current.playerSource.Play();
    }

    public static void PlayPlayerDeathAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.death;
        current.playerSource.Play();
    }

    public static void PlayPlayerFiringAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.firing;
        current.playerSource.Play();
    }

    public static void PlayPlayerFireReloadingAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.fireReloading;
        current.playerSource.Play();
    }


    //  Enemy
    public static void PlayEnemyHitAudio(){
        if(current == null){
            return;
        }

        current.playerSource.clip = current.hitEnemy;
        current.playerSource.Play();
    }

    public static void PlayEnemyDeathAudio(){
        if(current == null){
            return;
        }

        current.voiceSource.clip = current.deathEnemy;
        current.voiceSource.Play();
    }
}
