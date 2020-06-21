using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int _scoreValue = 100;

    public int Value {
        get { return _scoreValue; }
        set { _scoreValue = value; }
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().Kill();
        }
    }
}
