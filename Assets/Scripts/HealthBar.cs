using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform Bar;
 
    void Start()
    {
        Bar = GetComponent<RectTransform>();
        setsize(Health.totalhealth);

    }
    public void Damage(float damage)
    {
        if((Health.totalhealth -= damage) >= 0f) 
        {
            Health.totalhealth -= damage;
        }
        else
        {
            Health.totalhealth = 0;
        }

        setsize(Health.totalhealth);

    }
    public void setsize(float size)
    {
        Bar.localScale = new Vector3(size, 1f);


    }
}
