using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

    public Texture2D ControllerControls = null;
    public Texture2D KeyboardControls = null;
    public Texture2D Splash = null;

    bool m_hasController = false;
    private float m_startTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        m_startTime = Time.time;
	}

    void Update()
    {
        if (Time.time - m_startTime > 5.0f)
        {
            if (Input.GetButton("AButton"))
            {
                Application.LoadLevel("Sandbox");
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {                
        m_hasController = Input.GetJoystickNames().Length > 0;
	}

    void OnGUI()
    {
        Rect fullScreen = new Rect((Screen.width - Screen.height) * 0.5f, 0.0f, Screen.height, Screen.height);

        if (Time.time - m_startTime < 5.0f)
        {
            GUI.DrawTexture(fullScreen, this.Splash);
        }
        else
        {
            if (m_hasController)
            {
                GUI.DrawTexture(fullScreen, this.ControllerControls);
            }
            else
            {
                GUI.DrawTexture(fullScreen, this.KeyboardControls);
            }
        }        
    }
}
