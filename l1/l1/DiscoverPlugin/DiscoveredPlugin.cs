using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;
using PluginBase;

namespace l1.DiscoverPlugin
{
    public class DiscoveredPlugin : IPlugin
    {
        class DiscoveredType : IPluginMenuInvokableItem
        {
            public string Name { get; set; }
            public Type Invokable { get; set; }
        }

        public abstract class InvokableString : IInvokable
        {
            private readonly RichTextBox _box;

            protected InvokableString(RichTextBox box)
            {
                _box = box;
            }

            public abstract string Transform(string s);
            public void DoWork()
            {
                _box.Text = Transform(_box.Text);
            }
        }

        public DiscoveredPlugin(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var menus = assembly.GetTypes().Where(t => !t.IsNested).Select(LoadType).Where(loaded => loaded != null).ToList();
            Menus = menus;
        }
        public string Name { get; private set; }
        public IEnumerable<IPluginMenuItem> Menus { get; private set; }

        private static Type BuildInjectableType(ModuleBuilder builder, MethodInfo mi)
        {
            var typeBuilder = builder.DefineType(mi.Name, TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(typeof(IInvokable));
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, mi.GetParameters().Select(p => p.ParameterType).ToArray());
            var constructorBody = constructor.GetILGenerator();
            var parameters = mi.GetParameters();
            var doWork = typeBuilder.DefineMethod("DoWork", MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.Final);
            var doWorkBody = doWork.GetILGenerator();
            for (short index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];
                var field = typeBuilder.DefineField(parameter.Name, parameter.ParameterType, FieldAttributes.Private);
                constructorBody.Emit(OpCodes.Ldarg_0);
                doWorkBody.Emit(OpCodes.Ldarg_0);
                constructorBody.Emit(OpCodes.Ldarg, index + 1);
                constructorBody.Emit(OpCodes.Stfld, field);
                doWorkBody.Emit(OpCodes.Ldfld, field);
            }
            constructorBody.Emit(OpCodes.Ret);
            doWorkBody.EmitCall(OpCodes.Call, mi, Type.EmptyTypes);
            doWorkBody.Emit(OpCodes.Ret);
            return typeBuilder.CreateType();
        }

        private static Type BuildStringType(ModuleBuilder builder, MethodInfo mi)
        {
            var typeBuilder = builder.DefineType(mi.Name, TypeAttributes.Public, typeof(InvokableString));
            typeBuilder.AddInterfaceImplementation(typeof(IInvokable));
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[] { typeof(RichTextBox) });
            var constructorBody = constructor.GetILGenerator();
            constructorBody.Emit(OpCodes.Ldarg_0);
            constructorBody.Emit(OpCodes.Ldarg_1);
            var baseConstructor = typeof(InvokableString).GetConstructor(BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance, null, new[] { typeof(RichTextBox) }, null);

            constructorBody.Emit(OpCodes.Call, baseConstructor);
            constructorBody.Emit(OpCodes.Ret);
            var transform = typeBuilder.DefineMethod("Transform", MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.Final, CallingConventions.Standard, typeof(string), new[] { typeof(string) });
            var doWorkBody = transform.GetILGenerator();
            doWorkBody.Emit(OpCodes.Ldarg_1);
            doWorkBody.EmitCall(OpCodes.Call, mi, Type.EmptyTypes);
            doWorkBody.Emit(OpCodes.Ret);
            return typeBuilder.CreateType();
        }
        private static IPluginMenuItem ConvertMethodToItem(MethodInfo mi)
        {
            Debug.Assert(mi.DeclaringType != null, "mi.DeclaringType != null");
            var assembly = mi.DeclaringType.Assembly;
            var an = new AssemblyName("Wrapper" + assembly.GetName().Name);
            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndCollect);
            var builder = ab.DefineDynamicModule("Wrapper" + assembly.GetName().Name);
            Type t;
            if (mi.ReturnParameter != null &&
                mi.ReturnParameter.ParameterType == typeof(string) &&
                mi.GetParameters().Length == 1 &&
                mi.GetParameters().First().ParameterType == typeof(string))
                t = BuildStringType(builder, mi);
            else
                t = BuildInjectableType(builder, mi);
            return new DiscoveredType { Invokable = t, Name = mi.Name };
        }
        private static IPluginMenuItem LoadType(Type type)
        {
            var subtypes = type.GetNestedTypes();
            var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
            if (!methods.Any() && !subtypes.Any())
                return null;
            var mi = subtypes.Select(LoadType).Where(loaded => loaded != null);
            var pmi = methods.Select(ConvertMethodToItem);
            return new SubMenuItem(type.Name, mi.Union(pmi));
        }
    }
}