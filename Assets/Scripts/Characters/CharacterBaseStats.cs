using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character Base Stats")]
public class CharacterBaseStats : ScriptableObject
{
	[SerializeField] private string _characterName = "";

	[SerializeField] private Material _characterImage;
	[SerializeField] private RuntimeAnimatorController _animatorController;

	[SerializeField] private uint _maxHealth = 0;
	[SerializeField] private uint _maxHeat = 0;

	[SerializeField] private List<Ability> _abilities = new List<Ability>();

	[SerializeField] private uint _attack = 0;
	[SerializeField] private float _accuracy = 0f;
	[SerializeField] private float _evade = 0f;
	[SerializeField] private uint _speed = 0;
	[SerializeField] private uint _defense = 0;

	[SerializeField] private Image _queueImage;

	[SerializeField] private MonoBehaviour _externalAI = null;

	public string characterName
	{
		get {return _characterName; }
	}

	public Material characterImage
	{
		get { return _characterImage;}
	}

	public RuntimeAnimatorController animatorController
	{
		get { return _animatorController; }
	}

	public uint maxHealth
	{
		get { return _maxHealth; }
	}
	public uint maxHeat
	{
		get { return _maxHeat; }
	}

	public List<Ability> abilities
	{
		get { return _abilities; }
	}

	public uint attack
	{
		get { return _attack; }
	}
	public float accuracy
	{
		get { return _accuracy; }
	}
	public float evade
	{
		get { return _evade; }
	}
	public uint speed
	{
		get { return _speed; }
	}
	public uint defense
	{
		get { return _defense; }
	}

	public Image queueImage
	{
		get { return _queueImage; }
	}

	public MonoBehaviour externalAI
	{
		get { return _externalAI; }
	}
}
