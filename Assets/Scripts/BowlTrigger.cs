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
    private float m_noofSugar = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    public float RequiredSugar = 10.0f;

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
    public int RequiredButter = 1;

    /// <summary>
    /// 
    /// </summary>
    private int m_noofButter = 0;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> m_ingredients = new List<GameObject>();

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
    public void OnPickUp()
    {
        foreach (GameObject ingredient in m_ingredients)
        {
            ingredient.collider.enabled = false;
            ingredient.rigidbody.isKinematic = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (m_ingredients.Contains(other.gameObject))
        {
            return;
        }

        if (other.name == "EggYolk")
        {
            m_noofEggs++;
            m_ingredients.Add(other.gameObject);
            other.transform.parent = this.transform;
            return;
        }

        if (other.name == "Butter")
        {
            m_noofButter++;
            m_ingredients.Add(other.gameObject);
            other.transform.parent = this.transform;
            return;
        }

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "FlourEmitter")
        {
            m_noofFlour += 0.02f;
            if (m_noofFlour < this.RequiredFlour)
            {
                m_flourMound.localScale = m_flourMound.localScale + new Vector3(0.002f, 0.002f, 0.002f);
            }
            return;
        }

        if (other.name == "SugarEmitter")
        {
            m_noofSugar += 0.02f;
            if (m_noofSugar < this.RequiredSugar)
            {
                m_flourMound.localScale = m_flourMound.localScale + new Vector3(0.001f, 0.001f, 0.001f);
            }
            return;
        }
    }
}
