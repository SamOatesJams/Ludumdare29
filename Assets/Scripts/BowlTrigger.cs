using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BowlTrigger : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public int RequiredEggs = 2;

    /// <summary>
    /// 
    /// </summary>
    private int m_noofEggs = 0;

    /// <summary>
    /// 
    /// </summary>
    public float RequiredFlour = 10.0f;

    /// <summary>
    /// 
    /// </summary>
    private float m_noofFlour = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    private Transform m_flourMound = null;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> m_ingrediatns = new List<GameObject>();

	// Use this for initialization
	void Start () {

        m_flourMound = this.transform.FindChild("FlourMound");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (m_ingrediatns.Contains(other.gameObject))
        {
            return;
        }

        if (other.name == "EggYolk")
        {
            m_noofEggs++;
            m_ingrediatns.Add(other.gameObject);
            other.transform.parent = this.transform;
            other.rigidbody.isKinematic = true;
            other.collider.enabled = false;
            return;
        }

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "FlourEmitter")
        {
            m_noofFlour += 0.02f;
            if (m_flourMound.localScale.x < 1.0f)
            {
                m_flourMound.localScale = m_flourMound.localScale + new Vector3(0.002f, 0.002f, 0.002f);
            }
        }
    }
}
