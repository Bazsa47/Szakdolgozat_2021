using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyedInstantly =false)
    {
        foreach (Transform child in t)
        {
            if (destroyedInstantly) MonoBehaviour.DestroyImmediate(child.gameObject);
            else MonoBehaviour.Destroy(child.gameObject);
        }
    }
}
