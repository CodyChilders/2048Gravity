using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class ExtensionMethods
{
    public static UnityEngine.GameObject[] GetChildObjects(this UnityEngine.GameObject parent)
    {
        List<UnityEngine.GameObject> childSpawnLocations = new List<UnityEngine.GameObject>();
        foreach (UnityEngine.Component childTransform in parent.GetComponentsInChildren(typeof(UnityEngine.Transform)))
        {
            if(childTransform.gameObject != parent)
                childSpawnLocations.Add(childTransform.gameObject);
        }
        return childSpawnLocations.ToArray();
    }
}
