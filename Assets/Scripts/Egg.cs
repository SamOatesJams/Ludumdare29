using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {

    public float MaxVelocity = 0.15f;

    private float m_lastVelocity = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        m_lastVelocity = this.rigidbody.velocity.sqrMagnitude;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (m_lastVelocity > this.MaxVelocity)
        {
            this.collider.enabled = false;
            this.rigidbody.isKinematic = true;

            foreach (Transform child in this.transform)
            {
                child.rigidbody.isKinematic = false;
                child.rigidbody.useGravity = true;
                child.collider.enabled = true;
                child.renderer.enabled = true;
            }
        }
    }
}
