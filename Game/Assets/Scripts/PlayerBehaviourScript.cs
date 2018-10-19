using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviourScript : MonoBehaviour {

	private Rigidbody2D rb;
	private Transform tr;
	private Animator an;
	public Transform verificaChao;
	public Transform verificaParede;

	private bool estaAndando;
	private bool estaNoChao;
	private bool estaNaParede;
	private bool estaVivo;
	private bool viradoParaDireita;
	private bool estaAtacando;
	private int life;

	private float axis;
	public float velocidade;
	public float forcaPulo;
	public float raioValidaChao;
	public float raioValidaParede;
	public int damage;

	public Collider2D atacando;


	public LayerMask solido;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();

		life = 10;
		estaVivo = true;
		viradoParaDireita = true;
		estaAtacando = false;
		damage = 1;
	}

	// Update is called once per frame
	void Update () {
		estaNoChao = Physics2D.OverlapCircle (verificaChao.position, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.position, raioValidaParede, solido);


		if (estaVivo) {
			//axis1 = Input.GetAxisRaw("Horizontal");
			axis = CrossPlatformInputManager.GetAxis("Horizontal");

			estaAndando = Mathf.Abs (axis) > 0f;
			//estaAndando = Mathf.Abs (axis1) > 0f;


			if (axis > 0f && !viradoParaDireita)
				Flip ();
			else if (axis < 0f && viradoParaDireita)
				Flip ();


			if ((CrossPlatformInputManager.GetButtonDown ("Jump") || Input.GetButtonDown ("Jump")) && estaNoChao)
				DoJump ();
			
			Animations ();
		}
			


	}

	public void DoJump(){
		//rb.velocity = Vector2.up * forcaPulo;

		//rb.AddForce (new Vector2 (0, forcaPulo), ForceMode2D.Force);
		rb.AddForce ((tr.up * forcaPulo));
	}
		

	public void Atk1(){
		an.SetTrigger ("ATK1");
		atacando.gameObject.SetActive (true);
		//yield return new WaitForSeconds (0.1f);
	}
	public void Atk2(){
		an.SetTrigger ("ATK2");
		atacando.gameObject.SetActive (true);
	}
	public void Atk3(){
		an.SetTrigger ("ATK3");
		atacando.gameObject.SetActive (true);
	}
		
	void FixedUpdate()
	{

		if (estaAndando && !estaNaParede) {
				
			if (viradoParaDireita)
				rb.velocity = new Vector2 (velocidade, rb.velocity.y);
			else
				rb.velocity = new Vector2 (-velocidade, rb.velocity.y);
		}
			
	}

	void Flip()
	{
		viradoParaDireita = !viradoParaDireita;

		tr.localScale = new Vector2 (-tr.localScale.x, tr.localScale.y);
	}


	void Animations()
	{
		an.SetBool ("Andando", (estaNoChao && estaAndando));
		an.SetBool ("Pulando", (!estaNoChao));
		an.SetFloat ("VelVertical", rb.velocity.y);
	
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere (verificaChao.position, raioValidaChao);
		Gizmos.DrawWireSphere (verificaParede.position, raioValidaParede);
	}

	void OnTriggerEnter2d(Collider2D other){
		if (atacando.CompareTag ("Enemy")) {
			SoldierBehaviour soldier = atacando.GetComponent<SoldierBehaviour> ();

			if (soldier != null) {
				soldier.DamageEnemy (damage);
			}
		}
	}
}
