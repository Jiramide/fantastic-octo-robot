using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    
    public enum NodeType
    {
        Empty,
        Tower0,
        Tower1,
        Tower2
    }

    public NodeType Type;
    public GameObject NodeObject;

    public Node()
    {
        Type = NodeType.Empty;
    }

    void SetNodeType(NodeType newType)
    {
        Type = newType;
    }

    NodeType GetNodeType()
    {
        return Type;
    }

    public void SetNodeObject(GameObject newNodeObject)
    {
        NodeObject nodeObjectInfo = newNodeObject.GetComponent<NodeObject>();

        SetNodeType(nodeObjectInfo.Type);
        NodeObject = newNodeObject;
    }

    public void DestroyNodeObject()
    {
        if (NodeObject == null)
        {
            return;
        }

        Destroy(NodeObject);
        NodeObject = null;
        SetNodeType(NodeType.Empty);
    }

}
