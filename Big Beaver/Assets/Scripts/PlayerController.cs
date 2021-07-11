using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float speed;
    [SerializeField] private float jumpForse;
    [SerializeField] private float dashSpeed;
    private TreeScript _tree;

    public GameObject Mayak;
    

    public VariableJoystick variableJoystick;
    public Rigidbody playerRb;

    public bool isCarried; //TODO
    public bool isOnGround; //TODO
    public bool dashReady = true;
    public float transformTime;
    protected bool isTree = false;
    protected bool isMegaBeaver = false;
    protected bool isGround = true;

    public float rotationSpeed;

    private void Start()
    {
        _tree = FindObjectOfType<TreeScript>();
        
    }
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
        if (isGround)
        {
            playerRb.AddForce(Vector3.up * jumpForse, ForceMode.Impulse);
            isGround = false;
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

    public void LessPlayer()
    {
        isMegaBeaver = false;
        playerRb.transform.localScale = playerRb.transform.localScale / 4;
    }

    public void GrowPlayer()
    {
        Debug.Log("77");
        isMegaBeaver = true;
        playerRb.transform.localScale = playerRb.transform.localScale * 4;
        Invoke("LessPlayer", transformTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("PreFinalScene");
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Tree")
        {
            
            
            

        }

        if (other.gameObject.tag == "Plot")
        {
            Destroy(other.gameObject);
            DashPlayer();
            Debug.Log("Plot Destroyed");
            
        }
        if (other.gameObject.tag == "Buff")
        {
            Destroy(other.gameObject);
            GrowPlayer();
            Debug.Log("Plot Destroyed");
        }
        if (other.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }



    #region Timer Coroutine
    IEnumerator DashTimer()
    {
        
        //Vector3 playerDirecrion = new Vector3(eye.transform.position.x, eye.transform.position.y, eye.transform.position.z);
        Vector3 playerDirection = Mayak.transform.position - playerRb.transform.position;
        playerRb.AddForce(playerDirection * -dashSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator DashCooldown()
    {
        
        yield return new WaitForSeconds(3.0f);
        dashReady = true;
    }

   
    #endregion
}