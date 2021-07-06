using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    public static readonly int TIME = 1, MOVES = 2, MOLECULE = 3, STABLE = 4;

    public int type;

    public string molecule;
    public float timeObjective;
    public int movesObjective;

    public Objective(string moleculeName)
    {
        molecule = moleculeName;
        Debug.Log(molecule);
        type = MOLECULE;
    }
    public Objective(float time)
    {
        timeObjective = time;
        type = TIME;
    }
    public Objective(int moves)
    {
        movesObjective = moves;
        type = MOVES;
    }
    public Objective()
    {
        type = STABLE;
    }
    
    public bool ObjectiveComplete()
    {
        if (type == TIME)
        {
            return GameController.main.GetTimeSinceLevelStart() <= timeObjective;
        } else if (type == MOVES)
        {
            return GameController.main.moves <= movesObjective;
        } else if (type == MOLECULE)
        {
            List<Molecule> molecules = GameController.main.GetMolecules();
            foreach (Molecule iMolecule in molecules)
            {
                if (iMolecule.GetName() == molecule)
                {
                    return true;
                }
            }
            return false;
        } else if (type == STABLE)
        {
            List<Molecule> molecules = GameController.main.GetMolecules();

            foreach (Molecule molecule in molecules)
            {
                if (molecule.GetRelativeCharge() != 0)
                {
                    return false;
                }
            }
            return true;
        } else
        {
            return false;
        }
    }

    public static bool checkMoleculeExists(List<Molecule> molecules, string moleculeName)
    {
        foreach (Molecule iMolecule in molecules)
        {
            if (iMolecule.GetName() == moleculeName)
            {
                return true;
            }
        }
        return false;
    }

    public string readalise()
    {
        if (type == TIME)
        {
            return "Finish in under " + Mathf.FloorToInt(timeObjective).ToString() + " seconds.";
        } else if (type == MOVES)
        {
            return  "Finish in under " + (movesObjective + 1).ToString() + " moves.";
        } else if (type == MOLECULE)
        {
            Debug.Log(molecule);
            return "Create a " + Molecule.FormatMoleculeNameForTMP(molecule) + " molecule.";
        }
        else if (type == STABLE)
        {
            return "Stabilise all the atoms";
        }
        else
        {
            return "Failed";
        }
    }
}
