using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    private Renderer ballRenderer;

    void Awake()
    {
        ballRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        if (SkinManager.Instance != null)
        {
            Material mat = new Material(ballRenderer.material);
            mat.SetColor("_BaseColor", SkinManager.Instance.GetSelectedColor());
            ballRenderer.material = mat;
        }
        else
        {
            Debug.Log("SkinManager null in PlayerAppearance");
        }
    }
}