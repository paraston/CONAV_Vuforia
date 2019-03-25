using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour {

    private Animator m_Animator;
    private bool scan = false;
    private bool grab = false;
    public AstronautAnimation astronautAnimation;

    public Transform robot;

    public GameObject rockGO1;
    public GameObject rockGO2;
    public GameObject rockGO3;

    private Transform rock1;
    private Transform rock2;
    private Transform rock3;

    public Transform astronaut;

    private float rotate_speed = 1.0f;
    private bool backToBase = false; // indicates that the robot should get back to its base


    // Use this for initialization
    void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        rock1 = rockGO1.transform;
        rock2 = rockGO2.transform;
        rock3 = rockGO3.transform;
    }

    // Update is called once per frame
    void Update () {

        //float distance = Vector3.SqrMagnitude(astronaut.position - robot.position);
        rock1 = rockGO1.transform;
        rock2 = rockGO2.transform;
        rock3 = rockGO3.transform;

        if (backToBase) //return to robot base
        {
            Debug.Log("BACK TO BASEEEE");

            if (Vector3.SqrMagnitude(robot.localPosition) > 0.05f)
            {
                robot.localPosition *= (1 - Time.deltaTime);
            }
            else
            {
                backToBase = false; // we made it to the base
            }
        }








        if (astronautAnimation.canGrab1 == true) //once the astronaut has waved to the robot, he can grab the rock 1
        {
            Debug.Log("Grabbing 1");
            
            //The robot rotate in order to grab in the direction of the rock
            Vector3 targetDir = rock1.position - robot.position;
            float step = rotate_speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
            robot.rotation = Quaternion.LookRotation(newDir);
            
            if (rockGO1.activeInHierarchy)
            {

                if (Vector3.SqrMagnitude(targetDir) > 3.0f) //go towards the rock until close enough to collect it
                {
                    robot.Translate(targetDir * Time.deltaTime);
                }
                else
                {
                    rockGO1.SetActive(false);
                    astronautAnimation.score++;
                    backToBase = true;
                }
            }

        }







        else if (astronautAnimation.canGrab2 == true) //once the astronaut has waved to the robot, he can grab the rock 2
        {
            Debug.Log("Grabbing 2");

            //The robot rotate in order to grab in the direction of the rock
            Vector3 targetDir = rock2.position - robot.position;
            float step = rotate_speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
            robot.rotation = Quaternion.LookRotation(newDir);

            if (rockGO2.activeInHierarchy)
            {
                if (Vector3.SqrMagnitude(targetDir) > 3.0f) //go towards the rock until close enough to collect it
                {
                    robot.Translate(targetDir * Time.deltaTime);
                }
                else
                {
                    rockGO2.SetActive(false);
                    astronautAnimation.score++;
                    backToBase = true;
                }
            }
        }








        else if (astronautAnimation.canGrab3 == true) //once the astronaut has waved to the robot, he can grab the rock 3
        {
            Debug.Log("Grabbing 3");

            //The robot rotate in order to grab in the direction of the rock
            Vector3 targetDir = rock3.position - robot.position;
            float step = rotate_speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
            robot.rotation = Quaternion.LookRotation(newDir);

            if (rockGO3.activeInHierarchy)
            {
                if (Vector3.SqrMagnitude(targetDir) > 3.0f) //go towards the rock until close enough to collect it
                {
                    robot.Translate(targetDir * Time.deltaTime);
                }
                else
                {

                    rockGO3.SetActive(false);
                    astronautAnimation.score++;
                    backToBase = true;
                }
            }
        }






        //Reset on 'r' if the collect is finished
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (astronautAnimation.score >= 3)
            {
                rockGO1.SetActive(true); // respawn rocks
                rockGO2.SetActive(true);
                rockGO3.SetActive(true);

                robot.localRotation = Quaternion.identity; //reset robot pose
                robot.localPosition = Vector3.zero;

                astronautAnimation.score = 0; // reset score
            }
        }


    }
}