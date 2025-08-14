using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundExit : StateMachineBehaviour
{
    [SerializeField] private Soundtype sound;
    [SerializeField, Range(0, 1)] private float volume = 1;

    // OnStateExit is called when a transition ends and the state machine ends to evaluate this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Playsound(sound, volume);
    }
}
