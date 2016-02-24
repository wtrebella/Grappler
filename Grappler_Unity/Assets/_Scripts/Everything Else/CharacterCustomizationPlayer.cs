using UnityEngine;
using System.Collections;

public class CharacterCustomizationPlayer : MonoBehaviour {
	[SerializeField] private ParticleSystem hatParticles;
	[SerializeField] private ParticleSystem shoeBackParticles;
	[SerializeField] private ParticleSystem shoeFrontParticles;

	public void OnHatEquipped() {
		PlayParticles(hatParticles);
	}

	public void OnShoeBackEquipped() {
		PlayParticles(shoeBackParticles);
	}

	public void OnShoeFrontEquipped() {
		PlayParticles(shoeFrontParticles);
	}

	public void OnHatUnequipped() {

	}

	public void OnShoeBackUnequipped() {

	}

	public void OnShoeFrontUnequipped() {

	}

	private void PlayParticles(ParticleSystem particles) {
		particles.Stop();
		particles.time = 0;
		particles.Play();
	}
}
