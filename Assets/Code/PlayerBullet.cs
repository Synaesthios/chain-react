using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class PlayerBullet : MonoBehaviour {

    [SerializeField]
    private float m_fireForce = 2f;

    private Renderer m_renderer;
    private Rigidbody m_rigidBody;

	public void Init (PlayerFireScript.BeatFireRating rating)
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_renderer = GetComponent<Renderer>();
        m_rigidBody.AddForce(transform.forward, ForceMode.VelocityChange);

        switch (rating)
        {
            case PlayerFireScript.BeatFireRating.Normal:
                m_renderer.material.color = Color.white;
                break;

            case PlayerFireScript.BeatFireRating.Good:
                m_renderer.material.color = Color.cyan;
                break;
            case PlayerFireScript.BeatFireRating.Perfect:
                m_renderer.material.color = Color.green;
                break;
        }
	}
}
