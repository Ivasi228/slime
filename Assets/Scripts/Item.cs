using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    [Serializable]
    public class Item
    {
        [SerializeField] Tile _tile;
        public Tile Tile
        {
            get { return _tile; }
            set { _tile = value; }
        }
        [SerializeField] Tilemap _tilemap;
        public Tilemap Tilemap
        {
            get { return _tilemap; }
            set { _tilemap = value; }
        }
        [Range(1, 100)]
        [SerializeField] int _chance;
        public int Chance
        {
            get { return _chance; }
            set { _chance = value; }
        }
        [SerializeField] bool _spawnOnGround;
        public bool SpawnOnGround
        {
            get { return _spawnOnGround; }
            set { _spawnOnGround = value; }
        }
    }
}
