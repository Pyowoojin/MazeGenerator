using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	new Rigidbody2D rigidbody;
    public Vector2 inputVec;
    private float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMoving();
    }

    void InputMoving()
	{
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        Vector2 nextVec = speed * Time.deltaTime * inputVec.normalized;
        rigidbody.MovePosition(rigidbody.position + nextVec);
    }
}
