using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    public AudioClip background;
    public AudioClip footsteps;
    public AudioClip death;
    public AudioClip ambient1;
    public AudioClip ambient2;
    public AudioClip ambient3;

    int timer1 = 60;
    int timer2 = 15;
    int timer3 = 30;
    int tick = 60;

    public void Start() {
        music.clip = background;
        music.Play();
    }

    public void PlaySFX(AudioClip clip, float vol = 1f) {
        SFX.PlayOneShot(clip, vol);
    }

    private void Update() {
        if (timer1 == 0) {
            SFX.PlayOneShot(ambient1);
            timer1 = UnityEngine.Random.Range(90, 180);
        }

        if (timer2 == 0) {
            SFX.PlayOneShot(ambient2, 0.25f);
            timer2 = UnityEngine.Random.Range(45, 90);
        }

        if (timer3 == 0) {
            SFX.PlayOneShot(ambient3, 0.25f);
            timer3 = UnityEngine.Random.Range(45, 90);
        }

        if (tick == 0) {
            timer1--;
            timer2--;
            timer3--;
            tick = 60;
        }

        tick--;
    }
}
