using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour {

	// Use this for initialization
	ILogger logger = Debug.unityLogger;
	private float _scale = 0.008f;
	private float _speedY = 0.05f;
	private float _speedX = 0.0f;
	private float _speedXMultiplier = 3.0f;
	private float _mousePositionX = 0.0f;
	private float _maxSpeed = 30.0f;
	private float _acceleration = 0.05f;
	private Rigidbody2D _playerCar;
	private bool _isCrashed = false;
	private bool _isControlled = false;
	private Camera _camera;
	private float _camHeight;
	private float _camWidth;
	private Collider2D _playerCollider;



	private float _playerSideOffset;


	void Start () {
		_playerCollider = this.GetComponent<Collider2D> ();
		_playerSideOffset = _playerCollider.bounds.size.x / 2;


		_playerCar = this.GetComponent<Rigidbody2D> ();
		_maxSpeed *= _scale;
		_camera = GameObject.FindObjectOfType<Camera> ();
		_camHeight = _camera.orthographicSize;
		_camWidth = _camHeight * _camera.aspect;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0)) {
			logger.Log ("!Debug mouse down");
			_isControlled = true;
			_mousePositionX = Input.mousePosition.x;
		}
		if (Input.GetMouseButtonUp (0)) {
			logger.Log ("!Debug mouse up");
			_isControlled = false;
		}
		if (_isControlled) {
			if (_mousePositionX != Input.mousePosition.x) {
				_speedX = (Input.mousePosition.x - _mousePositionX) * _scale * _speedXMultiplier;
				_mousePositionX = Input.mousePosition.x;
			}
		}
		if (!_isCrashed) {
			_speedY += _acceleration * _scale;
			if (_speedY > _maxSpeed) {
				_speedY = _maxSpeed;
			}
			_playerCar.transform.position = new Vector2 (_playerCar.transform.position.x + _speedX, _playerCar.transform.position.y + _speedY);
			_speedX = 0.0f;
		}
		if ((transform.position.x - _playerSideOffset) < _camera.transform.position.x - _camWidth) {
			transform.position = new Vector2 (_camera.transform.position.x + _playerSideOffset - _camWidth, transform.position.y);
		}
		if ((transform.position.x + _playerSideOffset) > _camera.transform.position.x + _camWidth) {
			transform.position = new Vector2 (_camera.transform.position.x - _playerSideOffset + _camWidth, transform.position.y);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			transform.position = new Vector2 (transform.position.x, 0.0f);
			_speedY = 0.0f;
			_isCrashed = true;
		}

	}
}
