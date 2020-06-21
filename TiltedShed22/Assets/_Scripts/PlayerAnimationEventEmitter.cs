using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventEmitter : MonoBehaviour
{
    public PlayerController PlayerController;
    
    [SerializeField] private CapsuleCollider2D _chompTrigger;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footsteps; // L, R

    private int footstepIndex = 0;
    
    public void StartChomp()
    {
        _chompTrigger.gameObject.SetActive(true);
    }

    public void StopChomp()
    {
        _chompTrigger.gameObject.SetActive(false);
    }


    public void PlayFootstepSound()
    {
        footstepIndex = (footstepIndex+1) % _footsteps.Length;
        _audioSource.PlayOneShot(_footsteps[footstepIndex], _audioSource.volume);
    }

}
