using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Image imageFill;

    float hp;
    float maxHp;

    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);        
    }

    private void LateUpdate()
    {
        if (transform.right.x < 0)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void OnInit(float maxHp)
    {
        this.maxHp = maxHp;
        this.hp = maxHp;
        imageFill.fillAmount = 1;
    }

    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
}
