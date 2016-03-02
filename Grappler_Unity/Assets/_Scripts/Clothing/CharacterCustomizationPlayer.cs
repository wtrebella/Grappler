using UnityEngine;
using System.Collections;

public class CharacterCustomizationPlayer : MonoBehaviour {
	[SerializeField] private ParticleSystem hatParticles;
	[SerializeField] private ParticleSystem shoeBackParticles;
	[SerializeField] private ParticleSystem shoeFrontParticles;

	public void OnHatEquipped() {
		PlayHatParticles();
	}

	public void OnShoeBackEquipped() {
		PlayShoeBackParticles();
	}

	public void OnShoeFrontEquipped() {
		PlayShoeFrontParticles();
	}

	public void OnHatUnequipped() {
		PlayHatParticles();
	}

	public void OnShoeBackUnequipped() {
		PlayShoeBackParticles();
	}

	public void OnShoeFrontUnequipped() {
		PlayShoeFrontParticles();
	}

	private void PlayHatParticles() {
		PlayParticles(hatParticles);
	}

	private void PlayShoeBackParticles() {
		PlayParticles(shoeBackParticles);
	}

	private void PlayShoeFrontParticles() {
		PlayParticles(shoeFrontParticles);
	}

	private void PlayParticles(ParticleSystem particles) {
		particles.Stop();
		particles.time = 0;
		particles.Play();
	}
}
