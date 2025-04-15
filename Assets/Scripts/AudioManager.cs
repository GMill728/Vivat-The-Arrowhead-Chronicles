using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //the outputs for the audio
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    //all sound files for the main scene
    public AudioClip background;
    public AudioClip footsteps;
    public AudioClip death;
    public AudioClip ambient1;
    public AudioClip ambient2;
    public AudioClip ambient3;

    //timers to handle ambience
    int timer1 = 60;
    int timer2 = 15;
    int timer3 = 30;
    int tick = 60;

    public void Start() {
        //plays the background music at the start
        music.clip = background;
        music.Play();
    }

    public void PlaySFX(AudioClip clip, float vol = 1f) {
        //plays a selected audio clip (can be accessed by any script)
        SFX.PlayOneShot(clip, vol);
    }

    private void Update() {
        //plays ambient1 (horror) every 90-180 ticks
        if (timer1 == 0) {
            SFX.PlayOneShot(ambient1);
            timer1 = UnityEngine.Random.Range(90, 180);
        }

        //plays ambient2 (bats) every 45-90 ticks
        if (timer2 == 0) {
            SFX.PlayOneShot(ambient2, 0.25f);
            timer2 = UnityEngine.Random.Range(45, 90);
        }

        //plays ambient3 (bugs) every 45-90 ticks
        if (timer3 == 0) {
            SFX.PlayOneShot(ambient3, 0.25f);
            timer3 = UnityEngine.Random.Range(45, 90);
        }

        //decrements the timers every tick
        if (tick == 0) {
            timer1--;
            timer2--;
            timer3--;
            tick = 60;
        }

        tick--;
    }
}
