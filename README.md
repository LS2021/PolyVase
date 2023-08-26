# PolyVase
## A short command line program to procedurally create low poly vases for 3D-printing.

## Requirements
- .NET 7 runtime (earlier versions *might* work)
- for building from source: c# languge.

## "Installation"
The project is built as a self-contained executable wich can be run directly.
Optinally it is possible to set up the directory of the executable file with the ```PATH``` environment variable. This allows the Application to be run from anywhere, without having to specify the whole path.

1. Download a pre built executable, from the releases. The following versions are currently provided.
    - 64 Bit Windows
    - 32 Bit Windows
    - 64 Bit Linux
3. Build directly from source.

## Usage
1. Run the Executable with ```PolyVase``` (full or relative path necassary if ```PATH``` is not set up).
2. ou will be prompted with the following
    1. A seed value, can be anything. Since the program is deterministic, using the same seed (identical other parameters) will always produre an identical output.
    2. The dimensions of the model.
    3. The number of vertical cuts.
    4. The nuber of vertecies.
    5. the amout of random variation
3. The save location for the output *.stl -file

## Build with dotnet cli
0. navigate to the root directory of the project.
1. Restore external references with the```dotnet restore``` command.
2. Build in Release config with ```dotnet publish --sc -c Release -r <runtime> -o <output directory>```
    - ```<runtime>``` needs to be replaced with the correct runtime-identifier (see: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog)
        - win-x64 for 64 bit windows
        - win-x86 for 32 bit (x86)
        - linux-x64 for 64 bit linux
    - ```<output directory>``` needs to be replaced with the chosen path for the chosen output folder

