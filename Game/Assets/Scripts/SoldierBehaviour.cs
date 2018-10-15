using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {

	private Transform tr;
	private Animator an;
	public Transform verificaChao;
	public Transform verificaParede;

	public Transform player;

	private bool estaNoChao;
	private bool estaNaParede;
	private bool naoEstaAtirando;
	private bool viradoParaDireita;

	protected SpriteRenderer sprite;

	public float velocidade;
	public float raioValidaChao;
	public float raioValidaParede;
	public bool estaAndando;

	private float posicaoInicial;
	private int health;

	public LayerMask solido;

	void Start(){

		naoEstaAtirando = true;
	}

	void Awake () {
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();
		player = GetComponent<Transform> ();
		sprite = GetComponent<SpriteRenderer> ();
		viradoParaDireita = false;
		health = 5;
		posicaoInicial = tr.position.x;

		velocidade = (velocidade / 10) * -1; 
	}

	void FixedUpdate () {
		EnemyMovements ();
		Animations (); 
	}

	void EnemyMovements()
	{
		estaNoChao = Physics2D.OverlapCircle (verificaChao.position, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.position, raioValidaParede, solido);

		var distancia = Vector2.Distance (tr.position, player.position);

		if (estaNoChao && naoEstaAtirando) {
			tr.position = new Vector2 (tr.position.x + velocidade, tr.position.y);
		}

		if (posicaoInicial < tr.position.x)
			Flip ();

		if (!estaNoChao || estaNaParede)
			Flip ();
		else if ((!estaNoChao || estaNaParede) && !viradoParaDireita)
			Flip ();

		if (distancia < 2f) {
			naoEstaAtirando = true;
		}

		Animations ();
	}

	void Flip()
	{
		viradoParaDireita = !viradoParaDireita;

		tr.localScale = new Vector2 (-tr.localScale.x, tr.localScale.y);

		velocidade *= -1;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (verificaChao.position, raioValidaChao);
		Gizmos.DrawWireSphere (verificaParede.position, raioValidaParede);
	}

	void Animations()
	{
		if(!estaAndando && naoEstaAtirando)
			an.SetBool ("Atirando", (!naoEstaAtirando));

	}

	public void DamageEnemy(int damage){
		health -= damage;
		StartCoroutine (Damage ());
		if (health > 1)
			Destroy (gameObject);
	}

	IEnumerator Damage(){
		sprite.color = Color.red;
		yield return new WaitForSeconds (0.1f);
		sprite.color = Color.white;
	}
}
