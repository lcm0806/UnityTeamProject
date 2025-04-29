using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public Rigidbody player;
    public int moveSpeed;

    private Vector3 inputVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        Move();
    }
    
    private void PlayerInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        inputVec = new Vector3(x, 0, z).normalized;
    }

    private void Move()
    {
        player.velocity = inputVec * moveSpeed;
    }
}
