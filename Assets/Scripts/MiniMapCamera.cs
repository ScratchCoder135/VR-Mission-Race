using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
public GameObject player;
public float yCoordinate;

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(player.transform.position.x,yCoordinate,player.transform.position.z);
    }
}
