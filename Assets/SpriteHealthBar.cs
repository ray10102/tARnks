using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealthBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer left, right, top, bottom, back, front;
    [SerializeField] private ParticleSystem brickChunks;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(float damage)
    {
        
    }
}
