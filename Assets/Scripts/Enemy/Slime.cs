using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject slimeSmaller;
    public float spawnOffset=1;
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Move()
    {
        throw new System.NotImplementedException();
    }
    public override void Death()
    {
        //spawns the two smaller slimes
        Instantiate(slimeSmaller, transform.position+transform.right*spawnOffset, transform.rotation);
        Instantiate(slimeSmaller, transform.position+transform.right*-spawnOffset, transform.rotation);
        //calls the base function to destory the game object
        base.Death();
    }
}
