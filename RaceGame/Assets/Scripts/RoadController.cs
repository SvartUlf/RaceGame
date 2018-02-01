using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

	public GameObject roadTile;

    private Camera _camera;
    private GameObject[] _tiles; 

    private float _camHeight;
    private float _camWidth;
    private int _numberOfTiles;
    private float _tileHeight;

     




	// Use this for initialization
    void Start () {

        _camera = GameObject.FindObjectOfType<Camera>();
        _camHeight = _camera.orthographicSize;
        _camWidth = _camHeight * _camera.aspect;
        _tileHeight = roadTile.GetComponent<BoxCollider2D>().size.y;

        _numberOfTiles = Mathf.RoundToInt(_camHeight * 2 / _tileHeight) + 2;
        _tiles = RoadGenerator();

	}
	
	// Update is called once per frame
	void Update () {
        TileMove();
	}



    void TileMove(){
        GameObject highestTile = _tiles[0];

        foreach (GameObject tile in _tiles)
        {
            if (tile.transform.position.y > highestTile.transform.position.y)
            {
                highestTile = tile;
            }
        }

        for (int i = 0; i < _numberOfTiles; i++){
            if (_tiles[i].transform.position.y + _tileHeight * 2  < _camera.transform.position.y - _camHeight)
            {
                _tiles[i].transform.position = new Vector3(_tiles[i].transform.position.x, highestTile.transform.position.y + _tileHeight, _tiles[i].transform.position.z);
            }
        }

	}

    private GameObject[] RoadGenerator(){

        float tileStartPositionX = _camera.transform.position.x - _camWidth;
        float tileStartPositionY = _camera.transform.position.y - _camHeight - _tileHeight;
        float nextPositionY = tileStartPositionY;

        GameObject[] resultTiles = new GameObject[_numberOfTiles];

        for (int i = 0; i < _numberOfTiles; i++){
            resultTiles[i] = Instantiate(roadTile, new Vector3(tileStartPositionX, nextPositionY, 0), Quaternion.identity);
            nextPositionY += _tileHeight;
        }

        return resultTiles;
    }

		
}
