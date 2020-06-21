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

    public void Start() {
        _animator.SetBool("isDead", false);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _animator.SetBool("isDead", true);
            GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<PlayerController>().StompPeople(_value);
        }
    }
}
