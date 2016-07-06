using UnityEngine;
using System.Collections;
using WhitDataTypes;

[RequireComponent(typeof(ParticleSystem))]
public class GroundSnowParticles : MonoBehaviour {
	[SerializeField] private SpeedPercent speedPercentConverter;
	[SerializeField] private FloatRange particleSpeedRange = new FloatRange(0.1f, 24.0f);
	[SerializeField] private FloatRange particleSizeRange = new FloatRange(0.03f, 1.0f);
	[SerializeField] private FloatRange particleEmissionRateRange = new FloatRange(0.0f, 150.0f);

	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	private ParticleSystem snowParticles;
	private ParticleSystem.EmissionModule snowEmission;

	private void Awake() {
		snowParticles = GetComponent<ParticleSystem>();
		snowEmission = snowParticles.emission;
		snowEmission.enabled = false;
		snowParticles.Play();
	}

	private void ToggleEmissionBasedOnDistanceToGround() {
		if (player.groundDetector.IsCloseToGround()) snowEmission.enabled = true;
		else snowEmission.enabled = false;
	}

	private void SetParticleSpeedBasedOnPlayerSpeed() {
		float playerSpeedPercent = speedPercentConverter.GetPercent();
		float particleSpeed = particleSpeedRange.Lerp(playerSpeedPercent);
		float particleSize = particleSizeRange.Lerp(playerSpeedPercent);
		float particleEmissionRate = particleEmissionRateRange.Lerp(playerSpeedPercent);
		snowParticles.startSpeed = particleSpeed;
		snowParticles.startSize = particleSize;
		snowEmission.rate = new ParticleSystem.MinMaxCurve(particleEmissionRate);
	}

	private bool IsEmitting() {
		return snowEmission.enabled;
	}

	private void FixedUpdate() {
		ToggleEmissionBasedOnDistanceToGround();
		if (IsEmitting()) SetParticleSpeedBasedOnPlayerSpeed();
	}
}
