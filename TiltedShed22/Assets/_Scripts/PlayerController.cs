using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _bodyCollider;
    [SerializeField] private CapsuleCollider2D _chompTrigger;
    [SerializeField] private BiteReport _biteReport;
    [SerializeField] private GameObject _onDeathEffect;


    [Header("Movement Params")]
    [SerializeField] private float _horzSpeed = 3f;
    [SerializeField] private float _maxHorzDist = 3f;
    
    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _chompHits;
    [SerializeField] private AudioClip _chompMiss;

    public bool debugTestHitSound = false;

    private bool _isRunning = false;

    private bool _isChomping = false;

    private int _chompSoundIndex = 0;

    public delegate void PlayerDeathEvent();
    public PlayerDeathEvent pDied;

    public delegate void PlayerScoredEvent(int score);
    public PlayerScoredEvent pScored;

    private void Start()
    {
        // idk
        ToggleRunning(true);
        FinishChomp();
        _biteReport = _chompTrigger.GetComponent<BiteReport>();
        _biteReport.BiteEvent += HandleBite;
        _chompTrigger.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isRunning == false)
        {
            return;
        }

        float horzInput = Input.GetAxisRaw("Horizontal");
        bool chomp = GetChompInput();
        
        HandleMovement(horzInput);
        
        // Chomp logic
        bool isChompAnimationState = _animator.GetCurrentAnimatorStateInfo(0).IsName("Chomp");
        if (_isChomping && isChompAnimationState == false)
        {
            FinishChomp();
        }
        
        if (chomp && _isChomping == false)
        {
            StartChomp();
        }
    }

    private void HandleMovement(float horzInput)
    {
        if (Mathf.Abs(horzInput) > 0f)
        {
            horzInput = (horzInput > 0f) ? 1 : -1;
            Vector3 nextPos = transform.position;
            nextPos.x = Mathf.Clamp(nextPos.x + horzInput * _horzSpeed * Time.deltaTime, -_maxHorzDist, _maxHorzDist);
            transform.position = nextPos;
        }
        
    }

    private void StartChomp()
    {
        _isChomping = true;
        _animator.SetTrigger("Chomp");
        //_chompTrigger.gameObject.SetActive(true);
        
        // ToDo: Check if hit, play hit/miss sound accordingly

        bool hit = debugTestHitSound;
        if (hit)
        {
            _chompSoundIndex = (_chompSoundIndex+1) % _chompHits.Length;
            _audioSource.PlayOneShot(_chompHits[_chompSoundIndex], _audioSource.volume);
        }
        else
        {
            _audioSource.PlayOneShot(_chompMiss, _audioSource.volume);
        }
    }

    private void FinishChomp()
    {
        _isChomping = false;
        //_chompTrigger.gameObject.SetActive(false);
    }

    /// <summary>
    /// Chomp bit something of value
    /// </summary>
    public void HandleBite(int score) {
        AddPoints(score);
    }

    public void StompPeople(int score) {
        AddPoints(score);
    }

    private void AddPoints(int score) {
        if(pScored != null) pScored(score);
    }

    public void ToggleRunning(bool argIsRunning)
    {
        _isRunning = argIsRunning;
        _animator.SetBool("IsRunning", _isRunning);
    }

    public void Kill() {
        OnDeath();
    }  

    private void OnDeath()
    {
        GameObject.Instantiate(_onDeathEffect, transform.position, Quaternion.identity);
        _isRunning = false;
        _animator.SetTrigger("Die");
        //gameObject.SetActive(false);
        if (pDied != null) pDied();
    }


    private bool GetChompInput()
    {
        // For now, use jump. Later, double tap?
        return Input.GetButtonDown("Jump");
    }

    public void OnCollisionEnter2D(Collision2D c) {
        if (c.otherCollider.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Died!");
            OnDeath();
        }
    }

}
