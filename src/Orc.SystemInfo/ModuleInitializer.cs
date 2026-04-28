using System.Runtime.CompilerServices;
using Catel.IoC;
using Catel.Services;
using Orc.SystemInfo;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static partial class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    [ModuleInitializer]
    public static void Initialize()
    {
    }
}
