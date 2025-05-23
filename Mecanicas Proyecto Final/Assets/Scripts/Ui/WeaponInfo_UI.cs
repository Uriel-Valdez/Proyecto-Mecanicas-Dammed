 using UnityEngine;
using TMPro;

public class WeaponInfo_UI : MonoBehaviour
{
    public TMP_Text currentBullets;
    public TMP_Text totalBullets;

    private void OnEnable()
    {
        EventManagement.current.updateBulletsEvent.AddListener(UpdateBullets);
    }

    private void OnDisable()
    {
        EventManagement.current.updateBulletsEvent.RemoveListener(UpdateBullets);
    }

    public void UpdateBullets(int newCurrentBullets, int newTotalBullets)
    {
        if (newCurrentBullets <= 0 )
        {
            currentBullets.color = Color.red;
        }
        else
        {
            currentBullets.color = Color.white; 
        }
        currentBullets.text = newCurrentBullets.ToString();
        totalBullets.text = newTotalBullets.ToString();
    }

 
}
