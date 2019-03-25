using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautAnimation : MonoBehaviour {

    private Animator m_Animator;

    public bool wave = false;
    public bool idle = true;

    public GameObject rockGO1;
    public GameObject rockGO2;
    public GameObject rockGO3;

    private Transform rock1;
    private Transform rock2;
    private Transform rock3;


    public Transform robot;
    public Transform astronaut;

    public float limit_distance = 20.0f;

    private float step;
    private float rotate_speed = 1.0f;

    public bool canGrab1 = false;
    public bool canGrab2 = false;
    public bool canGrab3 = false;

    public int score = 0;

	// Use this for initialization
	void Start () {
        //Setting up the animator
        m_Animator = gameObject.GetComponent<Animator>();

        rock1 = rockGO1.transform;
        rock2 = rockGO2.transform;
        rock3 = rockGO3.transform;
    }

    // Update is called once per frame
    void Update () {

        Vector3 targetDir = robot.position - astronaut.position;
        step = rotate_speed * Time.deltaTime; //Control the rotate speed through time
        Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
        astronaut.rotation = Quaternion.LookRotation(newDir);

        rock1 = rockGO1.transform;
        rock2 = rockGO2.transform;
        rock3 = rockGO3.transform;

        m_Animator.SetBool("IsWaving", wave);

        //Calculate the distance between the astronaut and the rock in order to trigger the right animations
        float distance1 = limit_distance + 100; //initialized higher than limit_distance
        float distance2 = limit_distance + 100;
        float distance3 = limit_distance + 100;

        if (rockGO1.activeInHierarchy)
        {
            distance1 =  Mathf.Sqrt( Vector3.SqrMagnitude(rock1.position - astronaut.position) );
        }
        if (rockGO2.activeInHierarchy)
        {
            distance2 = Mathf.Sqrt( Vector3.SqrMagnitude(rock2.position - astronaut.position) );
        }
        if (rockGO3.activeInHierarchy)
        {
            distance3 = Mathf.Sqrt( Vector3.SqrMagnitude(rock3.position - astronaut.position) );
        }

        //Debug.Log(distance1);

        bool rock1_found = (distance1 < limit_distance);
        bool rock2_found = (distance2 < limit_distance);
        bool rock3_found = (distance3 < limit_distance);
        bool rock_found = rock1_found || rock2_found || rock3_found;


        //The astronaut waves at the robot if she is closed to the rock
        if (rock_found && Input.GetKeyDown(KeyCode.C))
        {
            //The astronaut rotate in order to wave in the direction of the robot
            /*Vector3 targetDir = robot.position - astronaut.position;
            step = rotate_speed * Time.deltaTime; //Control the rotate speed through time
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
            astronaut.rotation = Quaternion.LookRotation(newDir);*/

            //Management of the differents animations : th astronaut has just to wave
            wave = true;
            //m_Animator.SetBool("IsWaving", wave);

            //Thanks to the signal of the astronaut, the robot can grab the rock
            if (rock1_found)
            {
                canGrab1 = true;
            }
            else if (rock2_found)
            {
                canGrab2 = true;
            }
            else if (rock3_found)
            {
                canGrab3 = true;
            }

        }

        if ( (!rock1_found) && canGrab1 ) // We are not close to rock 1 anymore
        {
            wave = false;
            //m_Animator.SetBool("IsWaving", wave);
            canGrab1 = false;
        }
        if ((!rock2_found) && canGrab2) // We are not close to rock 2 anymore
        {
            wave = false;
            //m_Animator.SetBool("IsWaving", wave);
            canGrab2 = false;
        }
        if ((!rock3_found) && canGrab3) // We are not close to rock 3 anymore
        {
            wave = false;
            //m_Animator.SetBool("IsWaving", wave);
            canGrab3 = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Resetting");
            wave = false;
            m_Animator.SetBool("IsWaving", wave);
        }

        //Check victory condition
        if(score >= 3)
        {
            Debug.Log("VICTORY");
        }
	}
}
