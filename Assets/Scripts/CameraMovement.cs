using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Drag Player GameObject here")]
    [SerializeField]
    private Transform player;

    [Header("Distance the camera is from the player")]
    [SerializeField]
    private Vector3 offset;


    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0)); // change cameras rotation with X,Y,Z 
    }

    private void LateUpdate()
    {
        Vector3 target = new Vector3(player.position.x, 0, player.position.z) + offset;
        transform.position = target; // change cameras position with X,Y,Z
    }


    //GameObject player;

    //// Start is called before the first frame update
    //void Start()
    //{
    //   player = GameObject.Find("player"); // find the player character
    //}

    ////Update is called once per frame
    //void Update()
    //{
    //   transform.position = player.transform.position + new Vector3(6, 8, 0); // change cameras position with X,Y,Z
    //   transform.rotation = Quaternion.Euler(new Vector3(45, -90, 0)); // change cameras rotation with X,Y,Z
    //}
}


