using Microsoft.VisualBasic;
using System.Data;

List<List<Constraint>> resultConstraints = new List<List<Constraint>>();
List<Constraint> firstConstraints = new List<Constraint>();
resultConstraints.Add(firstConstraints);

Constraint n1 = new Constraint();
Constraint n2 = new Constraint();
Constraint n3 = new Constraint();
Constraint n4 = new Constraint();
Constraint n5 = new Constraint();
Constraint n6 = new Constraint();
Constraint n7 = new Constraint();
Constraint n8 = new Constraint();
Constraint n9 = new Constraint();
Constraint n10 = new Constraint();
Constraint n11 = new Constraint();
Constraint n12 = new Constraint();
Constraint n13 = new Constraint();
Constraint n14 = new Constraint();
Constraint n15 = new Constraint();
Constraint n16 = new Constraint();
Constraint n17 = new Constraint();
Constraint n18 = new Constraint();
Constraint n19 = new Constraint();
Constraint n20 = new Constraint();
Constraint n21 = new Constraint();
Constraint n22 = new Constraint();
Constraint n23 = new Constraint();

firstConstraints.Add(n1);
firstConstraints.Add(n2);
firstConstraints.Add(n3);
firstConstraints.Add(n4);
firstConstraints.Add(n5);
firstConstraints.Add(n6);
firstConstraints.Add(n7);
firstConstraints.Add(n8);
firstConstraints.Add(n9);
firstConstraints.Add(n10);
firstConstraints.Add(n11);
firstConstraints.Add(n12);
firstConstraints.Add(n13);
firstConstraints.Add(n14);
firstConstraints.Add(n15);
firstConstraints.Add(n16);
firstConstraints.Add(n17);
firstConstraints.Add(n18);
firstConstraints.Add(n19);
firstConstraints.Add(n20);
firstConstraints.Add(n21);
firstConstraints.Add(n22);
firstConstraints.Add(n23);

string r1 = "x";
string r2 = "y";
string r3 = "z";
string r4 = "k";
string r5 = "q";
string r6 = "t";
string r7 = "l";
string r8 = "m";
string r9 = "n";
string r10 = "i";
string r11 = "p";

List<string> allResources = new List<string>();
allResources.Add(r1);
allResources.Add(r2);
allResources.Add(r3);
allResources.Add(r4);
allResources.Add(r5);
allResources.Add(r6);
allResources.Add(r7);
allResources.Add(r8);
allResources.Add(r9);
allResources.Add(r10);
allResources.Add(r11);

n1.resources.Add("i");
n1.resources.Add("t");

n2.resources.Add("i");
n2.resources.Add("q");

n3.resources.Add("i");
n3.resources.Add("y");

n4.resources.Add("i");
n4.resources.Add("x");

n5.resources.Add("t");
n5.resources.Add("x");

n6.resources.Add("t");
n6.resources.Add("q");

n7.resources.Add("t");
n7.resources.Add("y");

n8.resources.Add("y");
n8.resources.Add("z");

n9.resources.Add("y");
n9.resources.Add("q");

n10.resources.Add("y");
n10.resources.Add("x");

n11.resources.Add("y");
n11.resources.Add("k");

n12.resources.Add("z");
n12.resources.Add("x");

n13.resources.Add("z");
n13.resources.Add("n");

n14.resources.Add("z");
n14.resources.Add("m");

n15.resources.Add("z");
n15.resources.Add("k");

n16.resources.Add("m");
n16.resources.Add("n");

n17.resources.Add("m");
n17.resources.Add("l");

n18.resources.Add("k");
n18.resources.Add("x");

n19.resources.Add("k");
n19.resources.Add("l");

n20.resources.Add("x");
n20.resources.Add("q");

n21.resources.Add("m");
n21.resources.Add("k");

n22.resources.Add("l");
n22.resources.Add("x");

n23.resources.Add("n");
n23.resources.Add("p");


// Basic Configurations //

bool isNewConstraintFind = true;
int constraintLevel = 0; // start from 1'st list (2 pairs) and increase over loop

while (isNewConstraintFind)  // ensure there is a new constraint founded
{
    List<Constraint> newConstraintList = new List<Constraint>();

    isNewConstraintFind = false; 

    foreach (Constraint constraint in resultConstraints.ElementAt(constraintLevel)) // loop every "2 pairs constraints", constraint level is also complexity level
    {
        foreach (string resource in allResources)        // loop every exist resource
        {
            if (constraint.resources.Contains(resource))    // check if current constraint contains resource
            {
                continue;
            }
            bool noConstraint = false;
            foreach (string constraintResource in constraint.resources) // her c. resource'u gez ve karşılatır
            {
                if (noConstraint)   // eğer önceki c.resource ile resource arasında constraint yoksa geç zaman kaybetme
                {
                    continue;
                }
                if(!isConstraint(resource, constraintResource))  // eğer resource ile c.resource arasında constraint yoksa direkt geç zaman kaybetme.
                {
                    noConstraint = true;
                    continue;
                }
            }
            if (!noConstraint)
            { 
                Constraint newConstraint = new Constraint();
                newConstraint.resources.Add(resource);
                foreach (string constraintResource in constraint.resources)
                {
                    newConstraint.resources.Add(constraintResource);
                }
                newConstraintList.Add(newConstraint);
                isNewConstraintFind = true;
            }
        }
    }
    constraintLevel++;
    newConstraintList = makeConstraintsUnique(newConstraintList);
    if (newConstraintList.Count > 0)
    {
        resultConstraints.Add(newConstraintList);
    }
}
List<Constraint> allConstraints = new List<Constraint>();
allConstraints = finalConstraints();
Console.WriteLine("");


// Remove all the same constraint matchings except one, like x,y,z & z,x,y etc.
List<Constraint> makeConstraintsUnique(List<Constraint> constraintGiven)
{
    List<Constraint> tempConstraint = new List<Constraint>();

    foreach (Constraint constraint in constraintGiven)  // sort all resources of the constraint list like, x,y,z & z,x,y to x,y,z & x,y,z
    {
        constraint.resources.Sort();

        if (tempConstraint.Count == 0)
        {
            tempConstraint.Add(constraint);
        }
        else
        {
            bool areEqual = false;
            foreach (Constraint c in tempConstraint)     // check if there is a equal constraint
            {
                areEqual = constraint.resources.SequenceEqual(c.resources);
                if (areEqual)        // if same break
                {
                    break;
                }
            }
            if (!areEqual)          // if there is no same in list, add
            {
                tempConstraint.Add(constraint);
            }
        }
    }
    return tempConstraint;
}

// Check smaller constraints if they are a part of bigger combinations. 
List<Constraint> finalConstraints()
{
    List<Constraint> resultList = new List<Constraint>();
    List<Constraint> allConstraints = new List<Constraint>();
    foreach(List<Constraint> cl in resultConstraints)
    {
        foreach(Constraint c in cl)
        {
            allConstraints.Add(c);
        }
    }
    foreach(Constraint constraint in allConstraints)
    {
        bool isBiggerExist = false;
        foreach(Constraint constraintTarget in allConstraints)
        {
            int sameCounter = 0;

            if (constraintTarget.resources.Count <= constraint.resources.Count)  // equality siituation handled in make makeConstraintsUnique() function
            {
                continue;
            }
            if (constraint == constraintTarget)
            {
                continue;
            }
            foreach(string resouce in constraint.resources)
            {
                if (!constraintTarget.resources.Contains(resouce))
                {
                    break;
                }

                sameCounter++;
            }
            if (sameCounter == constraint.resources.Count) 
            {
                isBiggerExist = true;
                break;
            }
        }

        if (!isBiggerExist)
        {
            resultList.Add(constraint);
        }
    }
    return resultList;
}

bool isConstraint(string resource1, string resource2)
{
    bool result = false;

    foreach (Constraint c in firstConstraints)
    {
        if (c.resources.Contains(resource1) && c.resources.Contains(resource2))
        {
            result = true;
        }
    }

    return result;
}


// Classes
class Constraint
{
    public List<String> resources;
    public Constraint()
    {
        resources = new List<String>();
    }

    public override string ToString()
    {
        string result = "";
        foreach (string r in resources)
        {
            result += r + " # ";
        }
        return result;
    }
}





//string r1 = "x";
//string r2 = "y";
//string r3 = "z";
//string r4 = "k";

//List<string> allResources = new List<string>();
//allResources.Add(r1);
//allResources.Add(r2);
//allResources.Add(r3);
//allResources.Add(r4);

//Constraint c1 = new Constraint();
//c1.resources.Add("x");
//c1.resources.Add("y");
//Constraint c2 = new Constraint();
//c2.resources.Add("y");
//c2.resources.Add("z");
//Constraint c3 = new Constraint();
//c3.resources.Add("x");
//c3.resources.Add("z");
//Constraint c4 = new Constraint();
//c4.resources.Add("x");
//c4.resources.Add("k");
//Constraint c5 = new Constraint();
//c5.resources.Add("y");
//c5.resources.Add("k");
//Constraint c6 = new Constraint();
//c6.resources.Add("z");
//c6.resources.Add("k");


//firstConstraints.Add(c1);
//firstConstraints.Add(c2);
//firstConstraints.Add(c3);
//firstConstraints.Add(c4);
//firstConstraints.Add(c5);
//firstConstraints.Add(c6);
