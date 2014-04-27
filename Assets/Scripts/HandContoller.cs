using UnityEngine;
using System.Collections;

public class HandContoller : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public float MoveAmount = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    public float UpDownArmAmount = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    public Vector2 MoveRange = new Vector2();

    /// <summary>
    /// 
    /// </summary>
    public Vector2 RotateRange = new Vector2();

    /// <summary>
    /// 
    /// </summary>
    public Vector2 ArmRotateRange = new Vector2();

    /// <summary>
    /// 
    /// </summary>
    public Vector2 ArmElevationRange = new Vector2();

    /// <summary>
    /// 
    /// </summary>
    public Transform ThumbRoot = null;

    private bool m_isThumbGripping = false;

    public float ThumbGripAmount = 20.0f;

    public Transform[] FingerRoot = new Transform[4];

    private bool[] m_isFingerGripping = new bool[4];

    public float FingerGripAmount = 60.0f;

    public bool AllFingersPressed { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsModeEnabled { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsThumbPressed { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    private float m_totalVerticalMovement = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private float m_totalHorizontalMovement = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private float m_totalArmRotation = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private float m_totalArmElveation = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsModeEnabled)
        {
            return;
        }

        HandleMovement();
        HandleHand();
	}

    private void HandleHand()
    {
        HandleThumb();
        HandleFingers();
    }

    private void HandleFingers()
    {
        bool[] isPressed = new bool[] {
            Input.GetButton("LBButton"),
            Input.GetAxis("LeftTrigger") > 0.0f,
            Input.GetButton("RBButton"),
            Input.GetAxis("RightTrigger") > 0.0f
        };

        bool allPressed = true;

        for (int fingerIndex = 0; fingerIndex < this.FingerRoot.Length; ++fingerIndex)
        {
            if (isPressed[fingerIndex] && !m_isFingerGripping[fingerIndex])
            {
                m_isFingerGripping[fingerIndex] = true;
                TurnFinger(this.FingerRoot[fingerIndex], new Vector3(0.0f, this.FingerGripAmount, 0.0f), true);
            }
            else if (!isPressed[fingerIndex])
            {
                allPressed = false;
                if (m_isFingerGripping[fingerIndex])
                {
                    m_isFingerGripping[fingerIndex] = false;
                    TurnFinger(this.FingerRoot[fingerIndex], -new Vector3(0.0f, this.FingerGripAmount, 0.0f), false);
                }                
            }
        }

        this.AllFingersPressed = allPressed;
    }

    private void TurnFinger(Transform bone, Vector3 amount, bool pressed)
    {
        
        bone.localEulerAngles = bone.localEulerAngles + amount;

        if (bone.collider != null)
        {
            bone.collider.enabled = !pressed;
        }
        
        foreach (Transform child in bone)
        {
            TurnFinger(child, amount, pressed);
        }
    }

    private void HandleThumb()
    {

        this.IsThumbPressed = Input.GetButton("AButton");
        if (!this.IsThumbPressed)
        {
            if (m_isThumbGripping)
            {
                this.ThumbRoot.localEulerAngles = this.ThumbRoot.localEulerAngles + new Vector3(0.0f, this.ThumbGripAmount, 0.0f);
                TurnFinger(this.ThumbRoot, Vector3.zero, false);
                m_isThumbGripping = false;
            }
            return;
        }

        if (!m_isThumbGripping)
        {
            this.ThumbRoot.localEulerAngles = this.ThumbRoot.localEulerAngles - new Vector3(0.0f, this.ThumbGripAmount, 0.0f);
            TurnFinger(this.ThumbRoot, Vector3.zero, true);
            m_isThumbGripping = true;
        }
        

    }

    private void HandleMovement()
    {
        float armRotation = Input.GetAxis("RotationHorizontal");
        float horizontalMovement = Input.GetAxis("MoveHorizontal");
        float verticalMovement = Input.GetAxis("MoveVertical") * this.MoveAmount;
        float armVertical = Input.GetButton("XButton") ? -this.UpDownArmAmount : (Input.GetButton("BButton") ? this.UpDownArmAmount : 0.0f);

        m_totalVerticalMovement += verticalMovement;
        m_totalHorizontalMovement += horizontalMovement;
        m_totalArmRotation += armRotation;
        m_totalArmElveation += armVertical;

        Vector3 direction = this.transform.parent.transform.TransformDirection(Vector3.forward);

        // Arm rotation
        Vector3 oldRotation = this.transform.localEulerAngles;
        this.transform.localEulerAngles = this.transform.localEulerAngles + new Vector3(0.0f, 0.0f, armRotation);
        if (m_totalArmRotation > this.ArmRotateRange.y)
        {
            this.transform.localEulerAngles = oldRotation;
            m_totalArmRotation = this.ArmRotateRange.y;
        }
        else if (m_totalArmRotation < this.ArmRotateRange.x)
        {
            this.transform.localEulerAngles = oldRotation;
            m_totalArmRotation = this.ArmRotateRange.x;
        }

        // Rotation
        oldRotation = this.transform.localEulerAngles;
        this.transform.localEulerAngles = this.transform.localEulerAngles + new Vector3(0.0f, horizontalMovement, 0.0f);
        if (m_totalHorizontalMovement > this.RotateRange.y)
        {
            this.transform.localEulerAngles = oldRotation;
            m_totalHorizontalMovement = this.RotateRange.y;
        }
        else if (m_totalHorizontalMovement < this.RotateRange.x)
        {
            this.transform.localEulerAngles = oldRotation;
            m_totalHorizontalMovement = this.RotateRange.x;
        }

        // Movement
        Vector3 oldPosition = this.transform.position;
        this.transform.position = this.transform.position + (direction * verticalMovement);
        if (m_totalVerticalMovement > this.MoveRange.y)
        {
            this.transform.position = oldPosition;
            m_totalVerticalMovement = this.MoveRange.y;
        }
        else if (m_totalVerticalMovement < this.MoveRange.x)
        {
            this.transform.position = oldPosition;
            m_totalVerticalMovement = this.MoveRange.x;
        }

        // Arm Movement
        oldPosition = this.transform.position;
        this.transform.position = this.transform.position + new Vector3(0.0f, armVertical, 0.0f);
        if (m_totalArmElveation > this.ArmElevationRange.y)
        {
            this.transform.position = oldPosition;
            m_totalArmElveation = this.ArmElevationRange.y;
        }
        else if (m_totalArmElveation < this.ArmElevationRange.x)
        {
            this.transform.position = oldPosition;
            m_totalArmElveation = this.ArmElevationRange.x;
        }
    }
}
