using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Particles : MonoBehaviour
{
    public ParticleSystem _confetti;
    public ParticleSystem _dust;

    private void OnEnable()
    {
        Event_Manager._Instance._onNextLevel1 += PlayConfettiParticles;
    }

    private void Start()
    {
        _confetti = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        _dust = this.gameObject.transform.GetChild(3).GetComponent<ParticleSystem>();  
    }

    public void PlayConfettiParticles(GameObject _null)
    {
        _confetti.Play();
    }

    public void StopConfettiParticles()
    {
        _confetti.Stop();
    }

    public void PlayDustParticles()
    {
        _dust.Play();
    }

    public void StopDustParticles()
    {
        _dust.Stop();
    }


    private void OnDisable()
    {
        Event_Manager._Instance._onNextLevel1 -= PlayConfettiParticles;
    }
}
