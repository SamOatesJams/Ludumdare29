using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

    private Transform m_originalParent = null;

	// Use this for initialization
	void Start () {
        m_originalParent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag != "PickupCollider")
        {
            return;
        }

        bool isThumb = other.collider.name == "thumb-trigger";
        HandContoller controller = other.transform.parent.GetComponent<HandContoller>();

        if (isThumb)
        {
            if (controller.IsThumbPressed)
            {
                this.rigidbody.isKinematic = true;
                this.transform.parent = other.transform;
            }
            else
            {
                this.rigidbody.isKinematic = false;
                this.transform.parent = m_originalParent;
            }
        }
        else
        {
            if (controller.AllFingersPressed)
            {
                this.rigidbody.isKinematic = true;
                this.transform.parent = other.transform;
            }
            else
            {
                this.rigidbody.isKinematic = false;
                this.transform.parent = m_originalParent;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "PickupCollider")
        {
            return;
        }

        this.rigidbody.isKinematic = false;
        this.transform.parent = m_originalParent;
    }
}
