﻿using System.Numerics;
using System.Text;

//Prameters

string? seed = null;
float Height = float.NaN;
float MinDia = float.NaN;
float MaxDia = float.NaN;

uint cuts = uint.MaxValue;
uint facets = 0;

float angleRange = float.NaN;

string? fileName = null;

for(int i = 0; i < args.Length; i++)
{
    if(i + 1 < args.Length)
    {
        switch (args[i].ToLower())
        {
            case "-seed":
                seed = args[++i];
                break;
            case "-height":
                float.TryParse(args[++i], out Height);
                break;
            case "-min-diameter":
                float.TryParse(args[++i], out MinDia);
                break;
            case "-max-diameter":
                float.TryParse(args[++i], out MaxDia);
                break;
            case "-cuts":
                uint.TryParse(args[++i], out cuts);
                break;
            case "-facets":
                uint.TryParse(args[++i], out facets);
                break;
            case "-angle-range":
                float.TryParse(args[++i], out angleRange);
                break;
            case "-out":
                fileName = args[++i];
                break;                                        
        }   
    }
}

if (seed == null)
{
    Console.Write("Pick A seed: ");
    seed = Console.ReadLine();
}

//Prompt and get required Inputs

if (Height == float.NaN) do 
{
    Console.Write("Pick a height (stl-units): ");
} while (!float.TryParse(Console.ReadLine(), out Height));

if (MinDia == float.NaN) do
{
    Console.Write("Pick a minimum diameter (stl-units): ");
} while (!float.TryParse(Console.ReadLine(), out MinDia));

if (MaxDia == float.NaN) do
{
    Console.Write("Pick a maximum diameter (stl-units): ");
} while (!float.TryParse(Console.ReadLine(), out MaxDia));

if (facets == 0) do
{
    Console.Write("Pick a number of facets (3 or more): ");
} while (!uint.TryParse(Console.ReadLine(), out facets) ||facets < 3);

if (cuts == uint.MaxValue) do
{
    Console.Write("Pick a number of cuts (0 or more): ");
} while (!uint.TryParse(Console.ReadLine(), out cuts) || cuts < 0);

if (angleRange == float.NaN) do
{
    Console.Write("Pick a range of the angle (0 to 1): ");
} while (!float.TryParse(Console.ReadLine(), out angleRange) || angleRange < 0 || angleRange > 1);

//use current time in case no seed is provided;
DateTime now = DateTime.Now;

Random rand = new(seed?.GetHashCode() ?? now.ToString().GetHashCode());

uint vertCount = facets * (cuts + 2) + 2;
uint triCount = 2 * facets * (cuts + 2);

Vector3[] vertecies = new Vector3[vertCount];
(uint A, uint B, uint C, Vector3 n)[] triangles = new (uint, uint, uint, Vector3 n)[triCount];

//calculate vertex positions
vertecies[0] = Vector3.Zero;
vertecies[vertCount - 1] = new(0, 0, Height);

uint vertId = 1;

for (uint i = 0; i < facets; i++)
{
    float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
    float Phi = MathF.Tau / facets * (i + (rand.NextSingle() - 0.5f) * angleRange);
    vertecies[vertId] = new(R * MathF.Cos(Phi), R * MathF.Sin(Phi), 0);
    vertId++;
}

for (int j = 0; j < cuts; j++)
{
    for (uint i = 0; i < facets; i++)
    {
        float Z = (rand.NextSingle() + j + 0.5F) * Height / (cuts + 1);

        float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
        float Phi = MathF.Tau / facets * (i + (rand.NextSingle() - 0.5f) * angleRange);
        vertecies[vertId] = new(R * MathF.Cos(Phi), R * MathF.Sin(Phi), Z);
        vertId++;
    }
}

for (uint i = 0; i < facets; i++)
{
    float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
    float Phi = MathF.Tau / facets * (i + (rand.NextSingle() - 0.5f) * angleRange);
    vertecies[vertId] = new(R * MathF.Cos(Phi), R * MathF.Sin(Phi), Height);
    vertId++;
}


//tetermin triangle facets, including face normals.
uint triId = 0;

for (uint i = 0; i < facets; i++) 
{
    triangles[triId] = (0, (i + 1) % facets + 1, i + 1, new());
    triId++;
}

for (uint j = 0; j <= cuts; j++)
{
    for (uint i = 0; i < facets; i++)
    {
        uint offset = facets * j + 1;
        triangles[triId] = (offset + i, offset + (i + 1) % facets, offset + facets + i, new());
        triId++;
        triangles[triId] = (offset + (i + 1) % facets + facets, offset + facets + i, offset + (i + 1) % facets, new());
        triId++;
    }
}

for (uint i = 0; i < facets; i++)
{
    triangles[triId] = (vertCount - 1, vertCount - facets - 1 + i, vertCount - facets - 1 + (i + 1) % facets, new());
    triId++;
}

for(uint idx = 0; idx < triCount; idx ++)
{
    Vector3 A = vertecies[triangles[idx].A];
    Vector3 B = vertecies[triangles[idx].B];
    Vector3 C = vertecies[triangles[idx].C];

    triangles[idx].n = Vector3.Normalize(Vector3.Cross(A - C, B - C));
}

if (fileName == null)
{
    Console.Write("Pick File Save Location: ");
    fileName = Console.ReadLine() ?? "";
    
    bool fileOK = false;

    do
    {
        fileName = Console.ReadLine() ?? "";
        if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            Console.Write("Invalid file name. Try again: ");
        }

        else if (File.Exists(fileName))
        {
            Console.WriteLine("File already exists. override (y: yes / n: no):");
            bool invaildResponse = true;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        invaildResponse = false;
                        fileOK = true;
                    break;
                    case ConsoleKey.N:
                        invaildResponse = false;
                        Console.Write("Pick a different file name: ");
                    break;
                    default:
                    break;
                }
            } while(invaildResponse);
        }

        else
        {
            fileOK = true;
        }
    } while(!fileOK);
}

const int stlFileHeadderLength = 80; // stl-header must be exactly 80 bytes long;

//Write mesh data in the binary stl-file format to the output stream.

string headderText = $"seed: {seed}; Facets: {facets}; Cuts: {cuts}";
byte[] hedderEncoded = Encoding.UTF8.GetBytes(headderText.PadRight(stlFileHeadderLength, '\0') ,0 ,stlFileHeadderLength); // stl-header must be exactly 80 bytes long;

BinaryWriter OUT = new BinaryWriter(new FileStream(fileName, FileMode.Create));
OUT.Write(hedderEncoded,0 , stlFileHeadderLength);

OUT.Write(triCount);

for(uint idx = 0; idx < triCount; idx ++)
{
    Vector3 A = vertecies[triangles[idx].A];
    Vector3 B = vertecies[triangles[idx].B];
    Vector3 C = vertecies[triangles[idx].C];
    Vector3 n = triangles[idx].n;

    ushort attribute = 0;

    OUT.Write(n.X);
    OUT.Write(n.Y);
    OUT.Write(n.Z);

    OUT.Write(A.X);
    OUT.Write(A.Y);
    OUT.Write(A.Z);

    OUT.Write(B.X);
    OUT.Write(B.Y);
    OUT.Write(B.Z);

    OUT.Write(C.X);
    OUT.Write(C.Y);
    OUT.Write(C.Z);

    OUT.Write(attribute);
}

OUT.Flush();
OUT.Close();

Console.WriteLine("Done!");