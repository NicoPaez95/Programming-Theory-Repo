using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForcey;

    [SerializeField] private float jumpforcex;

    private CharacterController myCh;

    private Animator anim;

    public  bool isJumping = false;

    [SerializeField] float movementSpeed=3;
    private int slopeForceRayLength=1;
    Rigidbody myRb;

    private void Start()
    {

        myCh = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        ControllPlayer();
    }

    private void FixedUpdate()
    {
        jumpPlayer();
    }
    private void ControllPlayer()
    {
        if (!isJumping&transform.position.x>=20f)
        {
            adjustTerrain();

            Walk();
    
        }

    }

    private void Walk()
    { 
        anim.SetInteger("Walk", 1);
    }

    private void Jump()
    {
        anim.SetInteger("Walk", 0);

        StartCoroutine(jumpwait());
    }

    private void adjustTerrain()
    {
        myCh.Move(transform.forward * movementSpeed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                if (slopeAngle > myCh.slopeLimit)
                {
                    var slopeDirection = new Vector3(hit.normal.x, -hit.normal.y, hit.normal.z);

                    myCh.Move(slopeDirection * 8 * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator jumpwait()
    {
        yield return new WaitForSeconds(2);

        anim.SetInteger("idle", 1);

        myCh.enabled = false;

        anim.SetInteger("idle", 0);

        transform.rotation = Quaternion.Euler(-25, 270, 0);

        Vector3 velocidad = new Vector3(-jumpforcex, jumpForcey, 0);

        myRb.AddForce(velocidad);

        //myRb.AddForce(Vector3.up * jumpForcey*Time.deltaTime, ForceMode.Impulse);

        //myRb.AddForce(transform.forward *jumpforcex*Time.deltaTime, ForceMode.Impulse);

        //myRb.AddForce(transform.forward*Time.fixedDeltaTime,ForceMode.Impulse);

        

        isJumping = true;




    }

    void jumpPlayer()
    {
        if (!isJumping && transform.position.x < 20)
        {
            Jump();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }
}
