using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int _scoreValue = 100;

    public GameObject deathEffect;

    public int Value {
        get { return _scoreValue; }
        set { _scoreValue = value; }
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().Kill();
        }
    }

    public void Kill()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<Collider2D>().enabled = false;
        if (deathEffect)
        {
            GameObject go = Instantiate(deathEffect, transform);
            Vector3 pos = transform.position;
            pos.z += -2f;
            go.transform.position = pos;
        }
    }
}
