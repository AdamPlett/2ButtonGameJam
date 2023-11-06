using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSmaller : Enemy
{
    GameObject slimeSmallest;
    public float spawnOffset = 1;
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
        //spawns the two smallest slime enemy types
        Instantiate(slimeSmallest, transform.position + transform.right * spawnOffset, transform.rotation);
        Instantiate(slimeSmallest, transform.position + transform.right * -spawnOffset, transform.rotation);
        //calls the virtual function "death" to destory the gameobject apon death
        base.Death();
    }
}
