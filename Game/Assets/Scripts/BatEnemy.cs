using UnityEngine;

public class BatEnemy : MonoBehaviour {

	private Animator an;

	public Transform player;
	public Transform CasaMorcego;
	public float velocidade;

	private int life;

	bool estaParado;

	void Start(){
		an = GetComponent<Animator> ();
		estaParado = true;

		life = 5;
	}
	// Update is called once per frame
	void Update () {
		var distanciaPlayer = Vector2.Distance (transform.position, player.position);
		var distanciaCasa = Vector2.Distance (transform.position, CasaMorcego.position);

		if (distanciaPlayer < 5 && distanciaPlayer > 0.3f) {
			estaParado = false;
			transform.position = Vector2.MoveTowards (transform.position, player.position, velocidade * Time.deltaTime);
		} else {
			if (distanciaCasa == 0) {
				estaParado = true;
			} else {
				estaParado = false;
				transform.position = Vector2.MoveTowards (transform.position, CasaMorcego.position, velocidade * Time.deltaTime);
			}
		}

		Animations (); 
	}

	//Animations is called
	void Animations()
	{
		an.SetBool ("Parado", (estaParado));
	}
}
