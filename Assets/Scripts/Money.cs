using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class Money : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] int _amount;
        public int Amount
        {
            get { return _amount; }
            set {
                _amount = value;
                _text.text = Amount.ToString(); }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Tilemap map = GetComponentInParent<Tilemap>();
                Vector3Int removePos = map.WorldToCell(collision.contacts[0].point);
                map.SetTile(removePos, null);
                _amount++;
            }
        }
    }
}
