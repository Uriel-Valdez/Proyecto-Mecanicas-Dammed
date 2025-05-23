using UnityEngine;

public class ManagerHud : MonoBehaviour
{
    public GameObject WeaponInfoPrefab;
    public Transform hudParentCanvas;

    private GameObject currentHud;

    private void Start()
    {
       
        EventManagement.current.NewGunEvent.AddListener(CreateWeaponInfo);
    }

    public void CreateWeaponInfo()
    {


       
        if (currentHud != null) return;

        
        currentHud = Instantiate(WeaponInfoPrefab, hudParentCanvas);

        RectTransform rt = currentHud.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchorMin = new Vector2(0.5f, 0.5f); 
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = new Vector3(14f, 6f, 0f); 
        }

        Debug.Log("✅ HUD creado al sacar arma");
    }

    public void HideWeaponInfo()
    {
        if (currentHud != null)
        {
            Destroy(currentHud);
            currentHud = null;
            Debug.Log("❌ HUD destruido al soltar arma");
        }
    }

}
