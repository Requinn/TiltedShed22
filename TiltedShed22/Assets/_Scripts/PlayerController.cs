using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _bodyCollider;
    [SerializeField] private CapsuleCollider2D _chompTrigger;
    
    [Header("Movement Params")]
    [SerializeField] private float _horzSpeed = 3f;
    [SerializeField] private float _maxHorzDist = 3f;

    private bool _isRunning = false;

    private bool _isChomping = false;

    private void Start()
    {
        // idk
        ToggleRunning(true);
        FinishChomp();
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
    }

    private void FinishChomp()
    {
        _isChomping = false;
        //_chompTrigger.gameObject.SetActive(false);
    }
    
    
    public void ToggleRunning(bool argIsRunning)
    {
        _isRunning = argIsRunning;
        _animator.SetBool("IsRunning", _isRunning);
    }

    
    private void OnDeath()
    {
        
    }


    #region Input

    private bool GetChompInput()
    {
        // For now, use jump. Later, double tap?
        return Input.GetButtonDown("Jump");
    }

    #endregion
}
