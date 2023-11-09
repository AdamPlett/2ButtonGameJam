using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float animationTime;

    void Start()
    {
        Destroy(gameObject, animationTime);
    }

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }
}
