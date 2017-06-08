using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Game
{

    public class redBoxFactory : IBox
    {
        public GameObject GetBox()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.name = "red";
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            gameObject.transform.parent = GameObject.Find("game").transform;
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            return gameObject;
        }
    }

    public class blueBoxFactory : IBox
    {
        public GameObject GetBox()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.name = "blue";
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.transform.parent = GameObject.Find("game").transform;
            gameObject.transform.localPosition = new Vector3(1, 0, 0);
            return gameObject;
        }
    }
}
