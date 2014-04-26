using UnityEngine;
using System.Collections;

public class Bowl : MonoBehaviour {

    public Mesh LowQualityBowl;
    public float MaxVelocity;

    private bool m_hasSmashed = false;
    private Smashable m_smashable = null;
    private float m_lastVelocity = 0.0f;

	// Use this for initialization
	void Start () {

        m_smashable = GetComponent<Smashable>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        m_lastVelocity = this.rigidbody.velocity.sqrMagnitude;

	}

    void OnCollisionEnter(Collision collision)
    {
        if (!m_hasSmashed && m_lastVelocity > this.MaxVelocity)
        {
            m_hasSmashed = true;
            GetComponent<MeshFilter>().mesh = this.LowQualityBowl;
            this.collider.enabled = false;
            m_smashable.SmashObject();
        }
    }
}
