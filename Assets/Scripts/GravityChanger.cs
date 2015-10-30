using UnityEngine;
using System.Collections;

public class GravityChanger : MonoBehaviour {

    Rigidbody rb;
    CharacterController cc;
    Vector3 currentGravity;

    bool isOnGravRoad = false;
    Vector3 lastGroundPosition;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        currentGravity = Physics.gravity;
    }
	
	// Update is called once per frame
	void Update () {
        rb.WakeUp();
        if( !cc.isGrounded && isOnGravRoad)
        {
            transform.position = lastGroundPosition;
        }
        else
        {
            lastGroundPosition = transform.position;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ( hit.controller.isGrounded ) {

            if (hit.collider.tag == "GravityRoad")
            {
                isOnGravRoad = true;
                Debug.Log(currentGravity + " " + currentGravity.magnitude);

                if (hit.normal.normalized != -currentGravity.normalized)
                {
                    currentGravity = -(hit.normal.normalized) * 9.81f;
                    Physics.gravity = currentGravity;
                }

            }
            else
            {
                isOnGravRoad = false;
            }
        }
    }

}
