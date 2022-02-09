using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseUnit : MonoBehaviour
{
	public string unitName = "Unit";
	public int unitLevel = 1;
	public Sprite defaultSprite;
	[Header("base Stats")]
	public int maxHealth = 100;
	[ReadOnlyInspector] public int currentHealth;
	public int defence = 100;
	public int damage = 10;
	public int critChance;

	[HideInInspector] public bool isBlocking = false;

	protected Animator anim;
	TMP_Text damageText;


     protected virtual void Awake()
	{
		damageText = GetComponentInChildren<TMP_Text>(true);
		currentHealth = maxHealth;
		anim = GetComponent<Animator>();
	}

	public virtual void ChooseAction()
	{
		isBlocking = false;
	}
	public virtual void Block()
	{
		isBlocking = true;
		anim.Play("Block Animation");
	}
	public virtual void TakeDamage(int damage)
	{
		int tempNum = Random.Range(0, 101);
		if(tempNum <= 25)
		{
			Debug.Log("Critical");
		}

		if (isBlocking)
		{
			damage = damage / 2;
			StartCoroutine(HurtWhileBlocking());
		}
		else
		{
			anim.Play("Hurt Animation");
		}
		damageText.text = damage.ToString();
		damageText.gameObject.SetActive(true);
		currentHealth -= damage;

		//Debug.Log(unitName + " took " + damage);
		if(currentHealth <= 0)
		{
			Die();
		}
	}

	IEnumerator HurtWhileBlocking()
	{
		anim.Play("Hurt Animation");
		yield return new WaitForSeconds(1);
		anim.Play("Block Animation");
	}
	protected virtual void Die()
	{
		gameObject.SetActive(false);
		Debug.Log(unitName + " has died");
	}

	protected virtual void OnValidate()
	{
		gameObject.name = unitName + " Unit";
		if (defaultSprite != null)
		{
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<SpriteRenderer>().sprite = defaultSprite;

		}
		else
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
