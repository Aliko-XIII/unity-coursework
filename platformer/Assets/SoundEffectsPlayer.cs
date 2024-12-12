using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{

    public AudioSource src;
    public AudioClip jumpSfx, damageSfx, coinSfx;

    public void JumpSound()
    {
        src.clip = jumpSfx;
        src.Play();
    }

    public void DamageSound()
    {
        src.clip = damageSfx;
        src.Play();
    }

    public void CoinSound()
    {
        src.clip = coinSfx;
        src.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
