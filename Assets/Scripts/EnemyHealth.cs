using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyHealth: MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    
    
    [Tooltip("Adds this amount to maxHitPoints when enemy dies. ")]
    
    [SerializeField] int difficultyRamp = 1;

    int currentHitPoints = 0;
    Enemy enemy;

    void Start()
    {   
        enemy = GetComponent<Enemy>();
    }
    
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void ProcessHit()
    {
        currentHitPoints --;
        
        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
        
    }
}
