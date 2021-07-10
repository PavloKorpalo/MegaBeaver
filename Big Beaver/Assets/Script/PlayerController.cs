using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float speed;
    [SerializeField] private float jumpForse;
    [SerializeField] private float dashSpeed;

    public GameObject Mayak;

    public VariableJoystick variableJoystick;
    public Rigidbody playerRb;

    public bool isCarried; //TODO
    public bool isOnGround; //TODO
    public bool dashReady = true;

    public float rotationSpeed;


    public void FixedUpdate()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) // TODO: Make it jump with button
        {
            JumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashReady) // TODO: Make it jump with button
        {
            DashPlayer();
        }

        //Debug.Log("Speed is "+ speed);
    }

    // Move oue Beaver woth joystick
    private void MovePlayer()
    {
        Vector3 direction = (Vector3.forward * variableJoystick.Vertical) + (Vector3.right * variableJoystick.Horizontal);
        playerRb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Vector3 movementDirection = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Make our beaver jump with a space button
    public void JumpPlayer()
    {
        if (!isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForse, ForceMode.Impulse);
        }
    }

    // TODO
    public void DashPlayer()
    {
        Debug.Log("Do Dash");
        dashReady = false;

        
        StartCoroutine(DashTimer());
        
        StartCoroutine(DashCooldown());
    }

    #region Timer Coroutine
    IEnumerator DashTimer()
    {
        Debug.Log("Timer go");
        //Vector3 playerDirecrion = new Vector3(eye.transform.position.x, eye.transform.position.y, eye.transform.position.z);
        Vector3 playerDirection = Mayak.transform.position - playerRb.transform.position;
        playerRb.AddForce(playerDirection * dashSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator DashCooldown()
    {
        Debug.Log("Cooldown go");
        yield return new WaitForSeconds(3.0f);
        dashReady = true;
    }
    #endregion
}