using UnityEngine;
using System.Collections;

public class ModeController : MonoBehaviour {

    public enum ContolMode
    {
        Move,
        LeftHand,
        RightHand
    }

    /// <summary>
    /// 
    /// </summary>
    ContolMode m_currentMode = ContolMode.Move;

    /// <summary>
    /// 
    /// </summary>
    public CameraController m_moveController = null;

    /// <summary>
    /// 
    /// </summary>
    public HandContoller m_leftHandContoller = null;

    /// <summary>
    /// 
    /// </summary>
    public HandContoller m_rightHandContoller = null;

	// Use this for initialization
	void Start () {

        m_moveController.IsModeEnabled = true;

	}
	
	// Update is called once per frame
	void Update () {

        bool isDpadUp = Input.GetAxis("DPadVertical") > 0;
        bool isDpadLeft = Input.GetAxis("DPadHorizontal") < 0;
        bool isDpadRight = Input.GetAxis("DPadHorizontal") > 0;

        if (!(isDpadUp || isDpadLeft || isDpadRight))
        {
            return;
        }
        
        m_moveController.IsModeEnabled = false;
        m_leftHandContoller.IsModeEnabled = false;
        m_rightHandContoller.IsModeEnabled = false;

        if (isDpadUp)
        {
            m_currentMode = ContolMode.Move;
            m_moveController.IsModeEnabled = true;
        }
        else if (isDpadLeft)
        {
            m_currentMode = ContolMode.LeftHand;
            m_leftHandContoller.IsModeEnabled = true;
        }
        else if (isDpadRight)
        {
            m_currentMode = ContolMode.RightHand;
            m_rightHandContoller.IsModeEnabled = true;
        }

	}
}
