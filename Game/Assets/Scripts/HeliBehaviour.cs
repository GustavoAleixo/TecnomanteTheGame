using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliBehaviour : MonoBehaviour {
	private Transform tr;
	private Animator an;
	public Transform verificaChao;
	public Transform verificaParede;

	private bool estaAndando;
	private bool estaNoChao;
	private bool estaNaParede;
	private bool estaVivo;
	private bool paraBaixo;

	public float raioValidaChao;
	public float raioValidaParede;
	public float velocidade;

	private float axis;

	private int life;
	private float alturaMaxima;
	private float alturaMinima;

	public LayerMask solido;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();

		life = 5;
		paraBaixo = false;

		alturaMaxima = tr.position.y + 0.5f;
		alturaMinima = tr.position.y - 1.0f;
	}
		
	
	// Update is called once per frame
	void FixedUpdate () {
		EnemyMovements ();
	}

	void EnemyMovements()
	{
		estaNoChao = Physics2D.OverlapCircle (verificaChao.position, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.position, raioValidaParede, solido);

		if (paraBaixo && tr.position.y >= alturaMinima) {
			tr.position = new Vector2 (tr.position.x, tr.position.y - (0.1f / 10));
			if (tr.position.y <= alturaMinima)
				paraBaixo = !paraBaixo;
		}
		if (!paraBaixo && tr.position.y <= alturaMaxima) {
			tr.position = new Vector2 (tr.position.x, tr.position.y + (0.1f / 10));
			if (tr.position.y >= alturaMaxima)
				paraBaixo = !paraBaixo;
		}

			
		

	}

	void Flip()
	{
		paraBaixo = !paraBaixo;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere (verificaChao.position, raioValidaChao);
		Gizmos.DrawWireSphere (verificaParede.position, raioValidaParede);
	}
}
