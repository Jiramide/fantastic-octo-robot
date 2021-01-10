using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{

    public enum NodeType
    {
        Hole,
        Empty,
        Tower0,
        Tower1,
        Tower2,
    }

    public class Node
    {
        
        public NodeType Type { get; set; }
        public GameObject NodeObject;

        public Node()
        {
            Type = NodeType.Hole;
        }

        public void SetNodeObject(GameObject newNodeObject)
        {
            NodeObject nodeObjectInfo = newNodeObject.GetComponent<NodeObject>();

            Type = nodeObjectInfo.Type;
            NodeObject = newNodeObject;
        }

        public void DestroyNodeObject()
        {
            if (NodeObject == null)
            {
                return;
            }

            GameObject.Destroy(NodeObject);
            NodeObject = null;
            Type = NodeType.Empty;
        }

    }

}