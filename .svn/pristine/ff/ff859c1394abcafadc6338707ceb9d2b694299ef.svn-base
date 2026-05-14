using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCSoft
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public interface IDCJSRuntime
    {
        T Invoke<T>(string identifier, params object?[]? args);
        System.Threading.Tasks.ValueTask<T> InvokeAsync<T>(string identifier, params object?[]? args);
        System.Threading.Tasks.ValueTask InvokeVoidAsync(string identifier, params object?[]? args);
    }
}
