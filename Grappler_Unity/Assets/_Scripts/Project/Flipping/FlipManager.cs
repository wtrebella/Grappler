using UnityEngine;
using System.Collections;

public class FlipManager : MonoBehaviour {
	[SerializeField] private FlipCounter counter;
	[SerializeField] private Rigidbody2DForcer forcer;
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private Vector2 force = new Vector2(100, 30);

	private void Awake() {
		counter.SignalBackflip += OnBackflip;
		counter.SignalFrontFlip += OnFrontFlip;
	}

	private void OnBackflip() {
		ApplyForce();
		PlayParticles();
		ScoreManager.instance.ReportFlip();
	}

	private void OnFrontFlip() {
		ApplyForce();
		PlayParticles();
		ScoreManager.instance.ReportFlip();
	}

	private void ApplyForce() {
		forcer.AddImpulseForce(force);
	}

	private void PlayParticles() {
		ResetParticles();
		particles.Play();
	}
			
	private void ResetParticles() {
		particles.Clear();
	}
}
