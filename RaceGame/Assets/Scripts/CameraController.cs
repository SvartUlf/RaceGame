using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject _playerCar;
	float _offset = 5.0f;

	// Use this for initialization
	void Start () {
		_playerCar = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
        
		if (_playerCar != null) {
			transform.position = new Vector3 (transform.position.x, _playerCar.transform.position.y + _offset, transform.position.z);
		}
	}

}
