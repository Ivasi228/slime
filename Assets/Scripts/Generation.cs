using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField] Grid _grid;
    [SerializeField] Tilemap _tilemap;
    [SerializeField] Tile _groundTile;
    [SerializeField] GameObject _center;
    [SerializeField] int _sideSize = 10;
    [SerializeField] int _space = 2;
    [SerializeField] List<Item> _items = new List<Item>();

    List<List<Vector2>> _settedTilesLine = new List<List<Vector2>>();
    float _lastLinePosY;
    float _lastCenterPosY;

    void Start()
    {
        GenerateFull();
    }
    int _generatedSpace = 0;
    // Update is called once per frame
    void Update()
    {
        if (_lastCenterPosY > _center.transform.position.y + _grid.cellSize.y)
        {
            if (_generatedSpace < _space)
            {
                bool onGround = false;
                if (_generatedSpace == _space - 1)
                {
                    onGround = true;
                }
                GenerateLine(_lastLinePosY - _grid.cellSize.y, false, true, onGround);
                _generatedSpace++;
            }
            else
            {
                _generatedSpace = 0;
                GenerateLine(_lastLinePosY - _grid.cellSize.y, false);
            }
        }
    }
    void GenerateFull()
    {
        float firstLine = Mathf.Round(_center.transform.position.y + _sideSize * _grid.cellSize.y / 2);
        for (float i = firstLine; i > firstLine - _sideSize * _grid.cellSize.y; i -= _grid.cellSize.y)
        {
            if (_generatedSpace < _space)
            {
                bool onGround = false;
                if(_generatedSpace == _space - 1)
                {
                    onGround = true;
                }
                GenerateLine(i, false, true, onGround);
                _generatedSpace++;
            }
            else
            {
                _generatedSpace = 0;
                GenerateLine(i, false);
            }
        }
    }
    float _skip;
    void GenerateLine(float y, bool removeUpLine = true, bool item = false, bool onGround = false)
    {
        float x = Mathf.Round(_center.transform.position.x + _sideSize / 2 * _grid.cellSize.x);
        _skip = Random.Range(x / _grid.cellSize.x, x / _grid.cellSize.x - _sideSize);
        List<Vector2> line = new List<Vector2>();
        for (float i = x - _grid.cellSize.x * 2; i > x - _sideSize * _grid.cellSize.x; i-= _grid.cellSize.x)
        {
            Item it = new Item();
            if (item)
            {
                it = GetObject(onGround);
            }
            else
            {
                it.Tile = _groundTile;
                it.Tilemap = _tilemap;
            }
            if(it != null)
            {
                if (_tilemap.WorldToCell(new Vector2(i, 0)) == _tilemap.WorldToCell(new Vector2(_skip, 0)))
                {
                    if (it.Tile == _groundTile)
                    {
                        foreach (Item it1 in _items)
                        {
                            it1.Tilemap.SetTile(it.Tilemap.WorldToCell(new Vector2(i, y + _grid.cellSize.y)), null);
                        }
                        
                    }
                    continue; //скипается и генерация предметов, что влияет на их шансы
                }
                it.Tilemap.SetTile(it.Tilemap.WorldToCell(new Vector2(i, y)), it.Tile);
                line.Add(new Vector2(i, y));
            }

            
        }
        _tilemap.SetTile(_tilemap.WorldToCell(new Vector2(x - _grid.cellSize.x, y)), _groundTile);
        line.Add(new Vector2(x - _grid.cellSize.x, y));
        _tilemap.SetTile(_tilemap.WorldToCell(new Vector2(x - _sideSize * _grid.cellSize.x, y)), _groundTile);
        line.Add(new Vector2(x - _sideSize * _grid.cellSize.x, y));
        _settedTilesLine.Add(line);
        if(removeUpLine)
        {
            foreach (Vector2 v in _settedTilesLine[0])
            {
                _tilemap.SetTile(_tilemap.WorldToCell(v), null);
            }
            _settedTilesLine.Remove(_settedTilesLine[0]);
        }
        _lastLinePosY = y;
        _lastCenterPosY =_center.transform.position.y;
    }
    Item GetObject(bool onGround)
    {
        foreach(Item i in _items)
        {
            if (Random.Range(0, (int)(100 / i.Chance)-1) == 0 && onGround == i.SpawnOnGround) return i;
        }
        return null;
           
    }
}
