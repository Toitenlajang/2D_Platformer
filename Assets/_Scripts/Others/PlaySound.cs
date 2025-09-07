using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlayStep()
    {
        SoundManager.Playsound(Soundtype.FOOTSTEPS,0.5f);
    }
}
