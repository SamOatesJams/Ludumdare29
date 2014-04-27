using UnityEngine;
using System.Collections;

public class OvenWin : MonoBehaviour {

    public Oven Oven = null;

	// Use this for initialization
	void Start () {
	
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
        if (other.name == "Bowl")
        {
            Oven.CanWin = true;
        }
    }
}
