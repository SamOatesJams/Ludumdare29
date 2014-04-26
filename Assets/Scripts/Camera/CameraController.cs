using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public float RotateSpeed = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        HandleRotation();
        HandleMovement();

	}

    /// <summary>
    /// 
    /// </summary>
    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxis("MoveHorizontal");
        float verticalMovement = Input.GetAxis("MoveVertical");

        this.rigidbody.AddForce(new Vector3(horizontalMovement, 0.0f, verticalMovement));

    }

    /// <summary>
    /// 
    /// </summary>
    private void HandleRotation()
    {
        float verticalRotation = Input.GetAxis("RotationVertical") * this.RotateSpeed;

        Vector3 cameradestination = Camera.main.transform.localEulerAngles + new Vector3(verticalRotation, 0.0f, 0.0f);

        if (cameradestination.x <= 180.0f && cameradestination.x > 30.0f)
        {
            cameradestination.x = 30.0f;
        }

        if (cameradestination.x >= 180.0f && cameradestination.x < 350.0f)
        {
            cameradestination.x = 350.0f;
        }

        Camera.main.transform.localEulerAngles = cameradestination;

        float horizontalRotation = Input.GetAxis("RotationHorizontal") * this.RotateSpeed;
        this.transform.localEulerAngles = this.transform.localEulerAngles + new Vector3(0.0f, horizontalRotation, 0.0f);

    }
}