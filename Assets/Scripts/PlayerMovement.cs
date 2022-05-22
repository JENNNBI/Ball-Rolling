using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private float speed;
    [SerializeField] private float lerpValue;
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private Text ballNumberText;

    private Rigidbody rb;
    private Camera cam;
    private int ballNumber = 21;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            PlayerControl(); 
        }

        if (!isGameOver)
        {
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="Obstacle")
        {
            decreaseBall(other.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            incraseBall();
            Destroy(other.gameObject);
        }
    }

    private void incraseBall()
    {
        ballNumber++;
        speed += 50;
        transform.localScale = new Vector3(transform.localScale.x + 0.05f, transform.localScale.y + 0.05f, transform.localScale.z + 0.05f);
        ballNumberText.text = ballNumber.ToString();
    }

    private void decreaseBall(GameObject gameObject)
    {
        TextMeshPro obstacleText = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        int obstacleNumber = System.Convert.ToInt32(obstacleText.text);

       

        if (ballNumber>0)
        {
            ballNumber--;
            obstacleNumber--;
            speed -= 50;
            obstacleText.text = obstacleNumber.ToString();
            ballNumberText.text = ballNumber.ToString();
        }
        else
        {
            isGameOver = true;
            
            transform.localScale = new Vector3(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f, transform.localScale.z - 0.05f);

        }
        if (obstacleNumber == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Finish()
    {

    }

    private void PlayerControl()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 1000))
        {
            transform.position =  Vector3.Lerp(rb.transform.position, new Vector3(hit.point.x, 0.466f, rb.transform.position.z), lerpValue * Time.deltaTime);
        }
    }
}
