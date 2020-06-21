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

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().StompPeople(_value);
        }
    }
}
