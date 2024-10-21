using UnityEngine;

public class HourglassHover : MonoBehaviour
{
    public GameObject timeDisplay;
    
    private void OnMouseEnter()
    {
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }
    }
}