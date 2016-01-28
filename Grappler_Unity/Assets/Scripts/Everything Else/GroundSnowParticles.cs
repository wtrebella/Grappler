using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class GroundSnowParticles : MonoBehaviour {
	[SerializeField] private SpeedPercentConverter speedPercentConverter;
	[SerializeField] private FloatRange particleSpeedRange = new FloatRange(0.1f, 24.0f);
	[SerializeField] private FloatRange particleSizeRange = new FloatRange(0.03f, 1.0f);
	[SerializeField] private FloatRange particleEmissionRateRange = new FloatRange(0.0f, 150.0f);
	[SerializeField] private float maxDistance = 1;

	private ParticleSystem snowParticles;

	private void Awake() {
		snowParticles = GetComponent<ParticleSystem>();
		snowParticles.enableEmission = false;
		snowParticles.Play();
	}

	private void ToggleEmissionBasedOnDistanceToGround() {
		if (IsCloseToGround()) snowParticles.enableEmission = true;
		else snowParticles.enableEmission = false;
	}

	private void SetParticleSpeedBasedOnPlayerSpeed() {
		float playerSpeedPercent = speedPercentConverter.GetPercent();
		float particleSpeed = particleSpeedRange.Lerp(playerSpeedPercent);
		float particleSize = particleSizeRange.Lerp(playerSpeedPercent);
		float particleEmissionRate = particleEmissionRateRange.Lerp(playerSpeedPercent);
		snowParticles.startSpeed = particleSpeed;
		snowParticles.startSize = particleSize;
		snowParticles.emissionRate = particleEmissionRate;
	}

	private bool IsEmitting() {
		return snowParticles.enableEmission;
	}

	private bool IsCloseToGround() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, -1).normalized, maxDistance, 1 << LayerMask.NameToLayer("Ground"));
		if (hit.collider) return true;
		else return false;
	}

	private void FixedUpdate() {
		ToggleEmissionBasedOnDistanceToGround();
		if (IsEmitting()) SetParticleSpeedBasedOnPlayerSpeed();
	}
}
