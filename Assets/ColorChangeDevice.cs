using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour
{
    private Color col;
    private Renderer _renderer;

    private void Start() {
        GameObject parent = transform.parent.gameObject;
        _renderer = parent.GetComponent<Renderer>();
        col = _renderer.material.color;
    }


    public void Operate() {
        col = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(3 * Time.deltaTime);
        _renderer.material.color = Color.Lerp(_renderer.material.color, col, t);
    }

    private void OnHierarchyChange() {
        GameObject parent = transform.parent.gameObject;
        _renderer = parent.GetComponent<Renderer>(); 
    }
}
