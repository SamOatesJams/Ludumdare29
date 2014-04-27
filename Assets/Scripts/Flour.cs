using UnityEngine;
using System.Collections;

public class Flour : MonoBehaviour {

    public GameObject FlourEmitter = null;

    public float UpsideDownDelta = 0.7f;

    public float m_amount = 100.0f;

    private AudioSource m_audio = null;

	// Use this for initialization
	void Start () {
        m_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (m_amount <= 0.0f)
        {
            FlourEmitter.GetComponent<ParticleEmitter>().emit = false;
            m_audio.Stop();
            return;
        }

        if (Vector3.Dot(this.transform.forward, Vector3.down) > this.UpsideDownDelta)
        {
            FlourEmitter.GetComponent<ParticleEmitter>().emit = true;
            m_amount -= 0.2f;
            if (!m_audio.isPlaying)
            {
                m_audio.Play();
            }
        }
        else
        {
            FlourEmitter.GetComponent<ParticleEmitter>().emit = false;
            m_audio.Stop();
        }
	}
}
