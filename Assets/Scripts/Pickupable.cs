using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

    private Transform m_originalParent = null;

    private int m_isPickedup = 0;

    public AudioClip PickupAudio = null;

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
                if (m_isPickedup == 0)
                {
                    OnPickup(other, 1);
                }
            }
            else
            {
                if (m_isPickedup == 1)
                {
                    this.rigidbody.isKinematic = false;
                    this.transform.parent = m_originalParent;
                    m_isPickedup = 0;
                }
            }
        }
        else
        {
            if (controller.AllFingersPressed)
            {
                if (m_isPickedup == 0)
                {
                    OnPickup(other, 2);
                }                
            }
            else
            {
                if (m_isPickedup == 2)
                {
                    this.rigidbody.isKinematic = false;
                    this.transform.parent = m_originalParent;
                    m_isPickedup = 0;
                }
            }
        }
    }

    private void OnPickup(Collider other, int type)
    {
        this.rigidbody.isKinematic = true;
        this.transform.parent = other.transform;

        if (PickupAudio != null)
        {
            this.GetComponent<AudioSource>().clip = PickupAudio;
            this.GetComponent<AudioSource>().Play();
        }

        BowlTrigger bowlTrigger = this.GetComponentInChildren<BowlTrigger>();
        if (bowlTrigger != null)
        {
            bowlTrigger.OnPickUp();
        }

        m_isPickedup = type;
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
