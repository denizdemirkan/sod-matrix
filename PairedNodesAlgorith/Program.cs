using System.Data;
List<Constraint> constraints = new List<Constraint>();
List<Constraint> constraints2 = new List<Constraint>();
List<Constraint> resultConstraints = new List<Constraint>();
List<Constraint> finalConstraints = new List<Constraint>();

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




constraints.Add(n1);
constraints.Add(n2);
constraints.Add(n3);
constraints.Add(n4);
constraints.Add(n5);
constraints.Add(n6);
constraints.Add(n7);
constraints.Add(n8);
constraints.Add(n9);
constraints.Add(n10);
constraints.Add(n11);
constraints.Add(n12);
constraints.Add(n13);
constraints.Add(n14);
constraints.Add(n15);
constraints.Add(n16);
constraints.Add(n17);
constraints.Add(n18);
constraints.Add(n19);
constraints.Add(n20);
constraints.Add(n21);

string r1 = "x";
string r2 = "y";
string r3 = "z";
string r4 = "k";
string r5 = "q";
string r6 = "t";
string r7 = "l";
string r8 = "m";
string r9 = "n";
string r10 ="i";

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

// her constraint'i gez
//      constraint'lerin her resource'unu gez
//          her resource'u gez
//              kendileri hariç olan resource'lar ile tüm resource'ların oluşturduğu kombinasyonları yeni listeye ekle bunu da bir listeler listesine ekle
// Bu adımı yeni liste oluşmayıncaya kadar yenile
// listeler listesinin son elemanından (en büyük eşleşmelerden) başla 

foreach (Constraint c in constraints) // her constraint'i gez
{
    Constraint tempC = new Constraint();

    int constraintLevel = c.resources.Count;

    //bool isAllConstraint = false;  // her cr'nin rs ile constraint oluşturduğundan emin olmak için.
    string tempConstraintName = "";

    List<string> tempConstraintResources = new List<string>(); // bu listede c.resources.Count kadar olanlar bu constraint'e eklenecek.

    foreach (string cr in c.resources) // constraint'lerin her resource'unu gez
    {
        tempConstraintResources.Add(cr);
        foreach (string rs in allResources) // her resource'u gez
        {
            if (c.resources.Contains(rs)) // kendisi hariç olanları seç
            {
                continue;
            }

            tempConstraintName = rs;

            if(isConstraint(rs, cr))
            {
                tempConstraintResources.Add(rs);
            }
            else
            {
                continue;
            }
        }

    }
    foreach (string rs in tempConstraintResources)
    {
        int counter = 0;
        foreach (string index in tempConstraintResources)
        {
            if (rs.Equals(index))
            {
                counter++;
            }
        }
        if (counter == constraintLevel)
        {
            tempC.resources.Add(rs);
        }
    }
    
    foreach(string contain in c.resources)
    {
        tempC.resources.Add(contain);
    }
    //tempConstraintResources = tempConstraintResources.Distinct().ToList();
    tempC.resources = tempC.resources.Distinct().ToList();

    constraints2.Add(tempC);

    makeConstraintsUnique();

    
}

foreach(Constraint constraint in constraints)
{
    resultConstraints.Add(constraint);
}

foreach(Constraint constraint in resultConstraints)
{
    Console.WriteLine(constraint);
}


removeLittleCombinations();


void removeLittleCombinations()
{

}

void makeConstraintsUnique()
{
    foreach(Constraint constraint in constraints2)  // yeni oluşan çoklu constraintleri gez
    {
        constraint.resources.Sort();
        if (resultConstraints.Count == 0)
        {
            resultConstraints.Add(constraint);
        }
        else
        {
            bool areEqual = false;
            foreach (Constraint c in resultConstraints)     // sonuç listesindeki elemanları gez, aynı mı diye bak..
            {
                areEqual = constraint.resources.SequenceEqual(c.resources);
                if(areEqual)        // aynı bulursa döngüden çık
                {
                    break;
                }
            }
            if (!areEqual)          // aynı eleman yoksa constraint olarak ekle
            {
                resultConstraints.Add(constraint);
            }
        }
    }
}

bool isConstraint(string resource1, string resource2)
{
    bool result = false;

    foreach(Constraint c in constraints) 
    { 
        if(c.resources.Contains(resource1) && c.resources.Contains(resource2) )
        {
            result = true;
        }
    }

    return result;
}
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
        foreach(string r in  resources)
        {
            result += r + " # ";
        }
        return result;
    }
}