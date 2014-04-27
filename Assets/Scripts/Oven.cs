using UnityEngine;
using System.Collections;

public class Oven : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public Transform OvenDoor = null;

    /// <summary>
    /// 
    /// </summary>
    public float OpenedZ = 0.0f;

    private float m_startOpenTime = 0.0f;

    private float m_closedZ = 0.0f;

    private bool m_hasOpened = false;

    private bool m_requiresClose = false;

    private bool m_isOpeneing = false;

	// Use this for initialization
	void Start () {
        m_closedZ = this.transform.localEulerAngles.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (m_requiresClose && m_hasOpened)
        {
            if (m_startOpenTime == 0.0f)
            {
                m_startOpenTime = Time.time;
            }

            float time = (Time.time - m_startOpenTime) * 0.5f;
            if (time >= 1.0f)
            {
                m_hasOpened = false;
                m_isOpeneing = false;
                m_requiresClose = false;
                m_closedZ = 0.0f;
            }

            this.OvenDoor.transform.localEulerAngles = Vector3.Lerp(new Vector3(0.0f, 0.0f, this.OpenedZ), new Vector3(0.0f, 0.0f, 0.0f), time);
        }

        if (!m_hasOpened && m_isOpeneing)
        {
            float time = (Time.time - m_startOpenTime) * 0.5f;
            if (time >= 1.0f)
            {
                m_hasOpened = true;
                m_isOpeneing = false;
                m_startOpenTime = 0.0f;
            }

            this.OvenDoor.transform.localEulerAngles = Vector3.Lerp(new Vector3(0.0f, 0.0f, m_closedZ), new Vector3(0.0f, 0.0f, this.OpenedZ), time);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player")
        {
            return;
        }

        if (m_requiresClose)
        {
            m_isOpeneing = false;
            m_hasOpened = false;
            m_closedZ = this.OvenDoor.transform.localEulerAngles.z;
        }

        m_requiresClose = false;
        if (!m_isOpeneing && !m_hasOpened)
        {
            m_startOpenTime = Time.time;
            m_isOpeneing = true;
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name != "Player")
        {
            return;
        }

        if (m_isOpeneing || m_hasOpened)
        {
            m_requiresClose = true;
        }
    }
}
