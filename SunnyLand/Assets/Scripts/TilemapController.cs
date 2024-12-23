using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    CompositeCollider2D comp;
    PlayerController player;

    private void Start()
    {
        comp=GetComponent<CompositeCollider2D>();
        player=FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (player.ani.GetInteger("status") == 2)
        {
            comp.isTrigger= true;
        }
        else if(player.ani.GetInteger("status") == 3)
        {
            comp.isTrigger= false;
        }
    }
}
