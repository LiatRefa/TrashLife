using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenSmallWindow : MonoBehaviour
{
    public bool test;
    private void Update()
    {
        if (test)
        {
            DoBounce();
            test = false;
        }
    }
    [SerializeField] private float fallDuration = 1f;
    [SerializeField] private int numberOfBounces = 3;
    [SerializeField] private float bounceHeight = 2f;
    [SerializeField] private float bounceDuration = 0.5f;
    [SerializeField] private float fallForce = 500f;

    private Vector3 originalPosition;
    private Rigidbody rb;

    private void Awake()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity to keep the cube stationary
    }

    public void DoBounce()
    {
        transform.DOShakePosition(2.0f, strength: new Vector3(0, 2, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
    }

    private void Bounce(int remainingBounces)
    {
        if (remainingBounces > 0)
        {
            // Calculate the new bounce height based on the remaining bounces
            float currentBounceHeight = bounceHeight * (remainingBounces / (float)numberOfBounces);

            // Make the cube bounce
            transform.DOJump(new Vector3(transform.position.x, currentBounceHeight, transform.position.z), bounceHeight, 1, bounceDuration).SetEase(Ease.OutQuad)
                .OnComplete(() => Bounce(remainingBounces - 1));

            // Add a small "bounce" effect when the cube hits other objects
            transform.DOJump(transform.position, 0.1f, 1, 0.1f).SetEase(Ease.OutQuad);
        }

    }
}
