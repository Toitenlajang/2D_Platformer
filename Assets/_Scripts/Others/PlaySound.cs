using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlayStep()
    {
        SoundManager.Playsound(Soundtype.PLAYER_FOOTSTEPS, 0.1f);
    }
}
