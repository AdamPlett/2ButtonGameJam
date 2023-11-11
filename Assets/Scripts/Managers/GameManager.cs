using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    // TO ACCESS THESE VARIABLES, USE "GameManager.gm." FOLLOWED BY DESIRED VARIABLE
    public AudioManager audio;
    public UIManager ui;
    public PlayerController player;

    void Start()
    {
        if(gm == null)
        {
            //DontDestroyOnLoad(gameObject);
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
