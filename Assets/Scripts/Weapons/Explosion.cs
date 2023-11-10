using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float animationTime;
    public float explosionDamage = 5;
    void Start()
    {
        Destroy(gameObject, animationTime);
    }

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }
    public float GetDamage()
    {
        return explosionDamage;
    }
}
