using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
 
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("player"); // find the player character
    }

    //Update is called once per frame
    void Update()
    {
       transform.position = player.transform.position + new Vector3(6, 8, 0); // change cameras position with X,Y,Z
       transform.rotation = Quaternion.Euler(new Vector3(45, -90, 0)); // change cameras rotation with X,Y,Z
    }
}


