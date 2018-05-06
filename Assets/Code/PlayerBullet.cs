using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class PlayerBullet : MonoBehaviour {

    [SerializeField]
    private float m_fireForce;

    private Renderer m_renderer;
    private TrailRenderer m_trailRenderer;
    private Rigidbody m_rigidBody;

	public void Init (BeatRating rating)
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_renderer = GetComponent<Renderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_rigidBody.AddForce(transform.forward * m_fireForce, ForceMode.VelocityChange);

        switch (rating)
        {
            case BeatRating.Normal:
                m_renderer.material.color = Color.white;
                m_trailRenderer.startColor = Color.white;
                m_trailRenderer.endColor = Color.white;
                break;

            case BeatRating.Good:
                m_renderer.material.color = Color.cyan;
                m_trailRenderer.startColor = Color.cyan;
                m_trailRenderer.endColor = Color.cyan;
                break;
            case BeatRating.Perfect:
                m_renderer.material.color = Color.green;
                m_trailRenderer.startColor = Color.green;
                m_trailRenderer.endColor = Color.green;
                break;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "BossBullet")
        {
            Destroy(gameObject);
        }
    }
}
