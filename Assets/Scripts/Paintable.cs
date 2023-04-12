using System.Collections;
using System.IO;
using UnityEngine;

public class Paintable : MonoBehaviour
{

    public GameObject Brush, startMenuUI, drawGameUI;
    public Vector3 BrushSize;
    public RenderTexture RTexture;
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //cast a ray to the plane
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                //instantiate a brush
                var go = Instantiate(Brush, hit.point + Vector3.up * 0.05f, Quaternion.identity, transform);
                go.transform.localScale = BrushSize;
            }
        }
    }

    public void Save()
    {
        StartCoroutine(CoSave());
    }

    private IEnumerator CoSave()
    {
        //wait for rendering
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath + "/savedImage.png");

        //set active texture
        RenderTexture.active = RTexture;

        //convert rendering texture to texture2D
        var texture2D = new Texture2D(RTexture.width, RTexture.height);
        texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
        texture2D.Apply();
        foreach (var cup in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            cup.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", texture2D);
            cup.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(1.18f, 1.44f));
            cup.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(2.71f, 8));
        }
        startMenuUI.SetActive(true);
        drawGameUI.SetActive(false);
    }
    public void ClearButton()
    {
        foreach (var brush in GameObject.FindGameObjectsWithTag("Brush"))
        {
            Destroy(brush);
        }
    }
}
