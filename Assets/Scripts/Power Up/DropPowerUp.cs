using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject[] PowerUpUpdate;

    [SerializeField]
    private GameObject[] PowerUpBonus;


    public float[] DropUpdate;
    public float[] DropBonus;
    private float DropNumber, DropItem;

    private GameObject[] SelectPowerUp;
    private float[] SelectDropRate;
    private float ChonArray;


    // thuc hien 1 la khi 1 ke dich bi tieu diet de kiem tra co hoi roi ra power up, neu co thuc hien spawn power up
    public void DropPU(float DropChange)
    {
        DropNumber = Random.value;
        if (DropNumber <= DropChange)
        {
            Vector3 DropPosition = new Vector3(transform.position.x, transform.position.y, -0.05f);

            ChonArray = Random.value;
            if(ChonArray <= 0.35f)
            {
                SelectPowerUp = PowerUpUpdate;
                SelectDropRate = DropUpdate;
            }
            else if(ChonArray > 0.35f)
            {
                SelectPowerUp = PowerUpBonus;
                SelectDropRate = DropBonus;
            }

            DropItem = Random.value;
            for(int i = 0; i < SelectDropRate.Length; i++) 
            {
                if(DropItem <= SelectDropRate[i])
                {
                    Instantiate(SelectPowerUp[i], DropPosition, Quaternion.identity);
                    break;
                }
            }

        } 
    }
}