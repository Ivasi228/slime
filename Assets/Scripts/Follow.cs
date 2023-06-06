using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] GameObject _who;
        [SerializeField] float _lerp;

        private void Update()
        {
            Vector3 v = Vector3.Lerp(transform.position, _who.transform.position, _lerp * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, v.y, transform.position.z );
        }
    }
}
