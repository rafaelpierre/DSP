using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCarController : MonoBehaviour {

    // Use this for initialization

    Rigidbody rigidBody;
    public float speed;
    float steeringAngle;
    public float maxAngle = 0;
    int dodgeCounter1 = 0;
    int dodgeCounter2 = 0;
    int doNotCrossLine = 0;
    int update = 0;
    void Start () {

        //speed = 10;
        steeringAngle = transform.rotation.y;
        //Debug.Log(steeringAngle);
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
    }

    private void Update()
    {

        float zOffset = 0;



        GameObject firstObstacle = GameObject.FindGameObjectWithTag("obstacle1");
        GameObject secondObstacle = GameObject.FindGameObjectWithTag("obstacle2");
        Vector3 positionObstacle1 = firstObstacle.transform.position;
        Vector3 positionObstacle2 = secondObstacle.transform.position;

        Vector3 positionCar = transform.position;

        //float vectorDistance = Vector3.Distance(positionObstacle1, positionCar);
        float vectorDistancex1 =  positionCar.x - positionObstacle1.x;
        float vectorDistancez1 =  Mathf.Abs(positionCar.z - positionObstacle1.z);
        
        float vectorDistancex2 = positionCar.x - positionObstacle2.x;
        float vectorDistancez2 = Mathf.Abs(positionCar.z - positionObstacle2.z);
        Vector3 vectorDifference1 = positionObstacle1 - positionCar;


       

        if ((vectorDistancex1 < 20 && vectorDistancex1 > 15) && (vectorDistancez1 < 5 ) && dodgeCounter1 == 0) 
        {
            if ((positionObstacle1.x - positionObstacle2.x) > 30)
            {
                transform.position += new Vector3(0, 0, 8);
                dodgeCounter1 += 1;

            }
            else
            {
                doNotCrossLine = 1;
            }

        }
        

        if ((vectorDistancex1 < -10 && vectorDistancex1 > -20) && (vectorDistancez1 > 5 ) && dodgeCounter1 == 1)
        {

            transform.position -= new Vector3(0, 0, 8);
            dodgeCounter1 -= 1;

        }

        if ((vectorDistancex2 < 20 && vectorDistancex2 > 15) && (vectorDistancez2 < 5) && dodgeCounter2 == 0)
        {
            
            transform.position += new Vector3(0, 0, 8);
            dodgeCounter2 = +1;

        }

        if ((vectorDistancex2 < -10 && vectorDistancex2 > -20) && (vectorDistancez2 > 5) && dodgeCounter2 == 1)
        {
            
            transform.position -= new Vector3(0, 0, 8);
            dodgeCounter2 -= 1;

        }


        //This script only recognizes stuff in his lane. It MUST have sensors or cloudinformation to know if there are
        //any other vehicles on the other road, to know if there is enough room to dodge. This should be the part
        //where we can think about rules for crossing the line.

        //Define area that is needed for dodging the vehicle on your lane
        //Check if there are any vehicles in this area
        //Double check with sensors?
        //


        if (doNotCrossLine == 0)
        {
            Vector3 movement = new Vector3(10, 0.0f, 0);
            rigidBody.AddForce(movement * -(rigidBody.velocity.magnitude + (Time.deltaTime * 100)));
        }

        if (doNotCrossLine == 1 && update < 5)
        {
            update += 1;
            Vector3 movement = new Vector3(10, 0.0f, 0);
            rigidBody.AddForce(movement * (rigidBody.velocity.magnitude + (Time.deltaTime * 100)));
        }






        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.z >= -14)
        {

            zOffset -= Time.deltaTime * rigidBody.velocity.magnitude / 10;
        }

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.z <= -8)
        {

            zOffset += Time.deltaTime * rigidBody.velocity.magnitude / 10;

        }

        transform.position += new Vector3(0, 0, zOffset);
    }
}
