using System.Reflection;
using System.Reflection.Emit;

namespace Serilog.Enrichers.StackCleaner.Helpers;
internal static class ExceptionHelper
{
    private static readonly Action<Exception, string> s_ExceptionStackTraceStringSetter;

    static ExceptionHelper()
    {
        var stackTraceField = typeof(Exception).GetField("_stackTraceString", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var dynamicMethod = new DynamicMethod("Exception.set__stackTraceString", null, new[] { typeof(Exception), typeof(string) }, true);

        var generator = dynamicMethod.GetILGenerator();
        generator.Emit(OpCodes.Ldarg_0);
        generator.Emit(OpCodes.Ldarg_1);
        generator.Emit(OpCodes.Stfld, stackTraceField);
        generator.Emit(OpCodes.Ret);

        s_ExceptionStackTraceStringSetter = (Action<Exception, string>)dynamicMethod.CreateDelegate(typeof(Action<Exception, string>));
    }

    public static void SetStackTraceString(this Exception exception, string stacktrace)
    {
        try
        {
            s_ExceptionStackTraceStringSetter(exception, stacktrace);
        }
        catch
        {
        }
    }
}
