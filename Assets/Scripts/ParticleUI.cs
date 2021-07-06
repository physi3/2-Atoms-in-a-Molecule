using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleUI
{
    Text text;
    Text ionText;
    GameObject particle;
    Atom element;

    public ParticleUI(GameObject passedAtom,GameObject textPrefab)
    {
        particle = passedAtom;
        element = particle.GetComponent<Particle>().element;
        text = GameObject.Instantiate(textPrefab, GameObject.FindGameObjectWithTag("AtomCanvas").transform).GetComponent<Text>();
        text.text = element.name;
        ionText = text.gameObject.transform.Find("Ion").GetComponent<Text>();
    }

    public void UpdateText()
    {
        
        text.gameObject.transform.position = Camera.main.WorldToScreenPoint(particle.gameObject.transform.position);
        ionText.text = FormatCharge(element.RelativeCharge());
    }

    static string FormatCharge(int charge)
    {   
        if (Mathf.Abs(charge) == 1)
        {
            if (charge > 0)
            {
                return "+";
            } else
            {
                return "-";
            }
        }
        if (charge == 0)
        {
            return "";
        }
        else if (charge > 0)
        {
            return charge.ToString() + "+";
        } else
        {
            return Mathf.Abs(charge).ToString() + "-";
        }
    }

}
