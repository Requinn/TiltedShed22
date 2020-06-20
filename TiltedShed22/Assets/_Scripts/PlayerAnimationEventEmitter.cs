using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventEmitter : MonoBehaviour
{
    public PlayerController PlayerController;
    
    [SerializeField] private CapsuleCollider2D _chompTrigger;
    
    public void StartChomp()
    {
        _chompTrigger.gameObject.SetActive(true);
    }

    public void StopChomp()
    {
        _chompTrigger.gameObject.SetActive(false);
    }
}
