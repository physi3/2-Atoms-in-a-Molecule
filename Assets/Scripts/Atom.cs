using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom
{
    public string name;
    public int valenceCapacity;
    public int atomicNumber;
    public int valenceElectrons;

    public Molecule molecule;
    public List<Atom> bondedAtoms;
    public int bonds;
    
    public Atom(int passedAtomic, int capacity)
    {
        molecule = new Molecule(this);
        name = ((char)Random.Range('a', 'z')).ToString();
        bondedAtoms = new List<Atom>();
        valenceCapacity = 8;
        atomicNumber = Random.Range(0, valenceCapacity);
        valenceElectrons = atomicNumber;
        bonds = 0;
    }

    public Atom(string passedName, int passedAtomic, int capacity = 8)
    {
        molecule = new Molecule(this);
        name = passedName;
        bondedAtoms = new List<Atom>();
        atomicNumber = passedAtomic;
        valenceElectrons = atomicNumber;
        valenceCapacity = capacity;
        bonds = 0;
    }

    public int RelativeCharge()
    {
        if (TotalValenceElectrons() >= valenceCapacity / 2f)
        {
            return TotalValenceElectrons() - valenceCapacity;
        }
        else
        {
            return TotalValenceElectrons();
        }
    }
    public int TotalValenceElectrons()
    {
        return valenceElectrons + bonds;
    }
    public void FormBond(Atom atom2){
        bondedAtoms.Add(atom2);
        atom2.bondedAtoms.Add(this);
        molecule.CombineMolecule(atom2.molecule);
        atom2.molecule = molecule;

        int electrons = TotalValenceElectrons() + atom2.TotalValenceElectrons();
        int target = valenceCapacity + atom2.valenceCapacity;

        int bondtoform = Mathf.Min(Mathf.FloorToInt((target - electrons) / 2f), Mathf.Min(valenceCapacity - TotalValenceElectrons(), atom2.valenceCapacity - atom2.TotalValenceElectrons()));
        bonds += bondtoform;
        atom2.bonds += bondtoform;
    }

    public bool BondPossible(Atom atom2)
    {
        return !(TotalValenceElectrons() % valenceCapacity == 0 || atom2.TotalValenceElectrons() % atom2.valenceCapacity == 0);
    }

}
