using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // <summary>
    /// Changes speed and pitch. A value of 0 is 0 semitones (no change) and
    /// a value of 1 is 6 semitones.
    /// </summary>
    private const string SEMITONES = "Semitones";

    /// <summary>
    /// Used to shift the pitch down (without changing the speed) to counteract the pitch shift
    /// that coems with the semitone shift. A value of 0 is 1.0x (no change) and a value of 1 is 0.707x 
    /// (1/sqrt(2), returns 6 semitones to normal pitch).
    /// </summary>
    private const string FFT_MULT = "FFT Mult";

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the AudioManager singleton!");
            Destroy(this);
        }
    }

    void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(SEMITONES, 0f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FFT_MULT, 0f);
    }

    public void SetSpeed(float speed)
    {
        if(speed < 0 || speed > 1)
        {
            Debug.LogWarning("Could not set speed, was outside the range [0, 1]");
            return;
        }

        float speedSemitones = Mathf.Lerp(0f, 6f, speed);
        float fftMult = 1f / Mathf.Pow(2, speedSemitones/12f);
        float fftMultInvLerp = 1f - Mathf.InverseLerp(0.707107f, 1f, fftMult);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(SEMITONES, speed);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FFT_MULT, fftMultInvLerp);
    }
}
