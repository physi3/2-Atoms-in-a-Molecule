using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule
{
    public List<Atom> atoms;

    public Molecule(List<Atom> passedAtoms)
    {
        atoms = passedAtoms;
    }
    public Molecule(Atom passedAtom)
    {
        atoms = new List<Atom>() { passedAtom };
    }

    public void CombineMolecule(Molecule molecule2)
    {
        atoms.AddRange(molecule2.atoms);
        atoms.ForEach(delegate (Atom atom)
        {
            atom.molecule = this;
        });
    }

    public string GetName()
    {
        string name = "";
        atoms.ForEach(delegate (Atom atom)
        {
            name+=atom.name;
        });
        char[] charArray = name.ToCharArray();
        Array.Sort(charArray);

        char last = charArray[0];
        string groupedName = "";
        int counter = 0;
        string addString = "";

        foreach (char c in charArray)
        {
            if (last == c)
            {
                counter += 1;
            }
            else
            {
                if (counter == 1)
                {
                    addString = "";
                }
                else
                {
                    addString = counter.ToString();
                }
                groupedName += last;
                groupedName += addString;
                counter = 1;
                last = c;
            }
        }

        if (counter == 1)
        {
            addString = "";
        }
        else
        {
            addString = counter.ToString();
        }
        groupedName += last;
        groupedName += addString;

        return groupedName;
    }

    public int GetRelativeCharge()
    {
        int charge = 0;
        atoms.ForEach(delegate (Atom atom)
        {
            charge += atom.RelativeCharge();
        });
        return charge;
    }

    static public string FormatMoleculeNameForTMP(string unformattedName)
    {
        string formattedName = "";
        foreach (char c in unformattedName)
        {
            if (char.IsNumber(c))
            {
                formattedName += "<sub>" + c + "</sub>";
            } else
            {
                formattedName += c;
            }
        }
        return formattedName;
    }
}
