using System.Numerics;

string? seed;
float Height;
float MinDia;
float MaxDia;

uint cuts = 1;
uint facets = 5;

Console.Write("Pick A seed: ");
seed = Console.ReadLine();
Console.WriteLine(seed);

do 
{
    Console.Write("Pick Height (stl-units): ");
} while (float.TryParse(Console.ReadLine(), out Height));

do
{
    Console.Write("Pick a minimum diameter (stl-units): ");
} while (float.TryParse(Console.ReadLine(), out MinDia));

do
{
    Console.Write("Pick a Maximum diameter (stl-units): ");
} while (float.TryParse(Console.ReadLine(), out MaxDia));

DateTime now = DateTime.Now;

Random rand = new(seed?.GetHashCode() ?? now.ToString().GetHashCode());

uint vertCount = facets * (cuts + 2) + 2;
uint triCount = 2 * facets * (cuts + 2);

Vector3[] vertecies = new Vector3[vertCount];
(uint A, uint B, uint C, Vector3 n)[] triangles = new (uint, uint, uint, Vector3 n)[triCount];

vertecies[0] = Vector3.Zero;
vertecies[vertCount] = new(0, 0, Height);

uint vertId = 2;

for (uint i = 0; i < facets; i++)
{
    float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
    float Phi = MathF.Tau / facets * (i + rand.NextSingle() - 0.5f);
    vertecies[vertId] = new(R * MathF.Cos(Phi), R * Math.Sign(Phi), 0);
    vertId++;
}

for (int j = 0; j < cuts; j++)
{
    for (uint i = 0; i < facets; i++)
    {
        float u = rand.NextSingle();
        float Z = ((j == 0 ? -1f : -0.5f) * u + (j == (cuts - 1) ? 1f : 0.5f) * (1f - u)) * Height / (cuts + 1);

        float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
        float Phi = MathF.Tau / facets * (i + rand.NextSingle() - 0.5f);
        vertecies[vertId] = new(R * MathF.Cos(Phi), R * Math.Sign(Phi), Z);
        vertId++;
    }
}

for (uint i = 0; i < facets; i++)
{
    float R = (rand.NextSingle() * (MaxDia  - MinDia) + MinDia) * 0.5f;
    float Phi = MathF.Tau / facets * (i + rand.NextSingle() - 0.5f);
    vertecies[vertId] = new(R * MathF.Cos(Phi), R * Math.Sign(Phi), Height);
    vertId++;
}


uint triId = 0;

for (uint i = 0; i < facets; i++)
{
    
}

for (int j = 0; j <= cuts; j++)
{
    for (uint i = 0; i < facets; i++)
    {

    }
}

for (uint i = 0; i < facets; i++)
{

}