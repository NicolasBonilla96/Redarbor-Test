using System.Reflection;

namespace Redarbor.Api.Extensions;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly; 
}
