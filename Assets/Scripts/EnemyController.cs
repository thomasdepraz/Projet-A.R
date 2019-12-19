using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public TestController controller;
    public Vector3 playerPos;
    public bool canMove;
    public float speed;
    public Rigidbody enemyRgb;
    public GameObject firstPersonCamera;



    void Start()
    {
        controller = GameObject.FindWithTag("Controller").GetComponent<TestController>();
        enemyRgb = GetComponent<Rigidbody>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = controller.FirstPersonCamera.transform.position;

        if (canMove)
        {
            Movement(playerPos - transform.position);
        }
        else if (!canMove)
        {
            enemyRgb.velocity = Vector3.zero;
        }

        gameObject.transform.LookAt(firstPersonCamera.transform.position);
        gameObject.transform.Rotate(new Vector3(-90f, 0f, 0f));
    }

    public void Movement(Vector3 targetPos)
    {
        enemyRgb.velocity = targetPos.normalized * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        canMove = false;
        enemyRgb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        canMove = true;
    }
}
