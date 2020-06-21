using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// People
/// </summary>
public class People : MonoBehaviour
{
    [SerializeField]
    private int _biteCharge = 1;
    [SerializeField]
    private int _value = 10;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioClip[] _squishes;
    [SerializeField]
    private AudioSource _audioSource;

    public void Start() {
        _animator.SetBool("isDead", false);
    }

    private int stepIndex = 0;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            stepIndex = (stepIndex + 1) % _squishes.Length;
            _audioSource.PlayOneShot(_squishes[stepIndex], _audioSource.volume);
            _animator.SetBool("isDead", true);
            GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<PlayerController>().StompPeople(_value);
        }
    }
}
