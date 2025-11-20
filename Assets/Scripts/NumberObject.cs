using UnityEngine;

public class NumberObject : MonoBehaviour
{
    public int value; // the number this object represents
    public bool hasBeenSubmitted = false;


    private void OnEnable()
    {
        // Reset the flag when the object is enabled
        hasBeenSubmitted = false;
    }
}