using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public Image Panel;
    public float SwitchWallsDuration;
    public float SwitchRoomsDuration;
    private float opacity;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the TransitionManager singleton!");
            Destroy(this);
        }
    }

    void Start() 
    { 
        Panel.color = Color.clear;
        opacity = 0f; 
    }

    public void SwitchRoomsTransition(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(Blink(callback, SwitchRoomsDuration));
    }

    public void SwitchWallsTransition(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(Blink(callback, SwitchWallsDuration));
    }

    private IEnumerator Blink(Action callback, float targetDuration)
    {
        yield return Transition(1f, targetDuration/2f);
        callback();
        yield return Transition(0f, targetDuration/2f);
    }

    private IEnumerator Transition(float targetOpacity, float targetDuration)
    {
        float startingOpacity = opacity;

        // Will continue where it left off if already partially through a transition
        float actualDuration = Mathf.Abs(startingOpacity - targetOpacity) * targetDuration; 

        float startTime = Time.time;
        float elapsedTime = 0f;
        while(elapsedTime < actualDuration)
        {
            elapsedTime = Time.time - startTime;
            float t = elapsedTime / actualDuration;
            opacity = Mathf.Lerp(startingOpacity, targetOpacity, t);
            Panel.color = new Color(0, 0, 0, opacity);
            yield return null;
        }

        opacity = targetOpacity;
        Panel.color = new Color(0, 0, 0, opacity);
    }
}
