using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    private CharacterClass _myCharacter;
    private int _selection = 0;
    private bool _moving = false;
    private Vector2 _moveDirection;

    private int _health;
    private float _armor;
    private int _moveSpeed;
    private int _magic;
    private int[] _damage;
    private int _score = 0;
    private int _keys = 0;
    private int _potions = 0;
    private int[] _powerups;

    private Rigidbody _rigidbody;
    private PlayerManager _playerManager;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        for (int i = 0; i < 3; i++)
        {
            if (!_playerManager.isAvailable(_selection))
                _selection++;
        }
    }

    private void loadStats()
    {
        _health = _myCharacter.health;
        _armor = _myCharacter.armor;
        _moveSpeed = _myCharacter.moveSpeed;
        _magic = _myCharacter.magic;
        _damage = _myCharacter.damage;
        GetComponent<Renderer>().material = _myCharacter.myColor;
    }

    //this function moves the player
    public void OnMove(CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        if (context.performed)
        {
            _moving = true;
            //_rigidbody.AddForce(new Vector3(direction.x * _moveSpeed * 100, 0, direction.y * _moveSpeed * 100));
        }
        else if (context.canceled)
        {
            _moving = false;
        }
    }

    private void FixedUpdate()
    {
        if (_moving)
            transform.position += new Vector3(_moveDirection.x * _moveSpeed * 0.125f, 0, _moveDirection.y * _moveSpeed * 0.125f);
    }

    //these two functions are used to select the character 
    public void ScrollDown()
    {
        subtractAndScroll();
        if (!_playerManager.isAvailable(_selection))
        {
            subtractAndScroll();
            if (!_playerManager.isAvailable(_selection))
            {
                subtractAndScroll();
            }
        }
    }

    private void subtractAndScroll()
    {
        _selection--;
        if (_selection < 0)
            _selection = 3;
    }

    public void ScrollUp()
    {
        addAndScroll();
        if (!_playerManager.isAvailable(_selection))
        {
            addAndScroll();
            if (!_playerManager.isAvailable(_selection))
            {
                addAndScroll();
            }
        }
    }

    private void addAndScroll()
    {
        _selection++;
        if (_selection > 3)
            _selection = 0;
    }

    public void SelectCharacter()
    {
        if (_playerManager.isAvailable(_selection))
        {
            _myCharacter = _playerManager.selectCharacter(_selection);
            loadStats();
        }
    }

    //randomly returns the low damage number or the high damage number
    public int getDamage()
    {
        return Random.Range(_damage[0], _damage[1] + 1);
    }
}
