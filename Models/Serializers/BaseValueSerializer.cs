using System.Reflection;
using System.Runtime.CompilerServices;
using FASTER.core;
using NeuralFabric.Interfaces;

namespace NeuralFabric.Models.Serializers;

public class BaseValueSerializer : BinaryObjectSerializer<object>, ISingleton<BaseValueSerializer>
{
    private static BaseValueSerializer? _singleton;

    public BaseValueSerializer()
    {
        if (_singleton is not null)
        {
            throw new Exception();
        }
    }

    public static BaseValueSerializer GetInstance()
    {
        if (_singleton is not null)
        {
            return _singleton;
        }

        _singleton = new BaseValueSerializer();
        return _singleton;
    }

    private static IEnumerable<MethodInfo> GetGenericExtensionMethods(Assembly assembly, Type extendedType, string methodName)
    {
        var query = from type in assembly.GetTypes()
            where type.IsSealed && !type.IsGenericType && !type.IsNested
            from method in type.GetMethods(bindingAttr: BindingFlags.Static | BindingFlags.Public)
            where method.IsDefined(attributeType: typeof(ExtensionAttribute),
                inherit: false)
            where method.GetParameters()[0].ParameterType == extendedType
            select method;

        return query.Where(predicate: m => m.IsGenericMethod && m.Name == methodName);
    }

    public override void Serialize(ref object obj)
    {
        var typeT = obj.GetType();
        var thisAssembly = typeof(BaseValueSerializer).Assembly;
        foreach (var methodEntry in GetGenericExtensionMethods(assembly: thisAssembly,
                     extendedType: typeT,
                     methodName: "Serialize"))
        {
            var typeArgs = methodEntry.GetGenericArguments();
        }

        throw new NotImplementedException();
    }

    public override void Deserialize(out object obj)
    {
        throw new MethodAccessException();
    }

    public void Deserialize<T>(out T obj)
    {
        var typeT = typeof(T);
        var thisAssembly = typeof(BaseValueSerializer).Assembly;
        foreach (var methodEntry in GetGenericExtensionMethods(assembly: thisAssembly,
                     extendedType: typeT,
                     methodName: "Deserialize"))
        {
            var typeArgs = methodEntry.GetGenericArguments();
        }

        throw new NotImplementedException();
    }

    public void Deserialize<T>(string combinedKeyString, out T obj)
    {
        var _ = Tapestry.ValidateKey(combinedKey: combinedKeyString,
            keyName: string.Empty,
            expectedType: out var typeT);
        if (typeT is null || typeof(T) != typeT)
        {
            throw new InvalidDataException();
        }

        this.Deserialize(obj: out obj);
    }
}
