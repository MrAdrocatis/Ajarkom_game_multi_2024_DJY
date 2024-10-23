using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        // THIS IS WHERE YOU CHECK IF YOURE HITTING ENEMY
        // DAMAGE ENEMY
    }
}
