using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 0;
    }

    void Update()
    {

    }
}
