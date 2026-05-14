using System.Reflection;
namespace System.Data;

/// <summary>
/// 模拟器全局配置�?
/// </summary>
[Obfuscation(Exclude = true, ApplyToMembers = false)]
public static class AdoEmulatorConfiguration
{
    private static IAdoDataForwarder _defaultForwarder = new NullAdoDataForwarder();

    /// <summary>
    /// 配置默认数据转发器�?
    /// </summary>
    public static void ConfigureDefaultForwarder(IAdoDataForwarder forwarder)
    {
        _defaultForwarder = forwarder ?? throw new ArgumentNullException(nameof(forwarder));
    }

    internal static IAdoDataForwarder GetDefaultForwarder() => _defaultForwarder;
}
