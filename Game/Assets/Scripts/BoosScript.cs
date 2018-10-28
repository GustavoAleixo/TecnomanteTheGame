using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosScript : MonoBehaviour {

    private float cronometro;
	private Animator an;

	public Transform player;

	bool viradoParaDireita;

	// Use this for initialization
	void Start () {

		an = GetComponent<Animator> ();

		viradoParaDireita = true;
	}

	// Update is called once per frame
	void Update () {
        cronometro += Time.deltaTime;
		var distanciaPlayer = transform.position.x - player.position.x;

		if (distanciaPlayer > 0 && viradoParaDireita) {
			viradoParaDireita = true;
			Flip ();
			an.SetTrigger("Vira");
		}

		if (distanciaPlayer < 0 && !viradoParaDireita) {
			an.SetTrigger("Vira");
			viradoParaDireita = false;
			Flip ();
		}

	}

	void Flip()
	{
		viradoParaDireita = !viradoParaDireita;
		transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
	}

	void Animations()
	{
	}
}

