using System;

namespace EifelMono.NuGet
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(EifelMono.NuGet)} {EifelMono.NuGet.AssemblyInfoProperties.Version}");

            switch (args.Length)
            {
                case 2:
                    {
                        string dllFilename = args[1];
                        switch (args[0].ToLower())
                        {
                            case "nuspecversionfromdll":
                                MainActions.NuSpecFillVersionFromDll(dllFilename);
                                break;
                        }
                        break;
                    }
                
                default:
                    {
                        Console.WriteLine("Not enough parameters");
                        break;
                    }
            }
        }
    }
}
