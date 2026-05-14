// System.Resources.Extensions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51
// System.SR
using System;
using System.Resources;
namespace System;
internal static class DCResourceSR
{
    private static readonly bool s_usingResourceKeys = GetUsingResourceKeysSwitchValue();

    private static ResourceManager s_resourceManager;

    internal static ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new ResourceManager(typeof(DCResourceSR)));

    internal static string ArgumentOutOfRange_StreamLength => GetResourceString("ArgumentOutOfRange_StreamLength");

    internal static string Argument_StreamNotReadable => GetResourceString("Argument_StreamNotReadable");

    internal static string Argument_StreamNotWritable => GetResourceString("Argument_StreamNotWritable");

    internal static string Arg_ResourceFileUnsupportedVersion => GetResourceString("Arg_ResourceFileUnsupportedVersion");

    internal static string BadImageFormat_InvalidType => GetResourceString("BadImageFormat_InvalidType");

    internal static string BadImageFormat_NegativeStringLength => GetResourceString("BadImageFormat_NegativeStringLength");

    internal static string BadImageFormat_ResourceDataLengthInvalid => GetResourceString("BadImageFormat_ResourceDataLengthInvalid");

    internal static string BadImageFormat_ResourceNameCorrupted => GetResourceString("BadImageFormat_ResourceNameCorrupted");

    internal static string BadImageFormat_ResourceNameCorrupted_NameIndex => GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex");

    internal static string BadImageFormat_ResourcesDataInvalidOffset => GetResourceString("BadImageFormat_ResourcesDataInvalidOffset");

    internal static string BadImageFormat_ResourcesHeaderCorrupted => GetResourceString("BadImageFormat_ResourcesHeaderCorrupted");

    internal static string BadImageFormat_ResourcesIndexTooLong => GetResourceString("BadImageFormat_ResourcesIndexTooLong");

    internal static string BadImageFormat_ResourcesNameInvalidOffset => GetResourceString("BadImageFormat_ResourcesNameInvalidOffset");

    internal static string BadImageFormat_ResourcesNameTooLong => GetResourceString("BadImageFormat_ResourcesNameTooLong");

    internal static string BadImageFormat_ResType_SerBlobMismatch => GetResourceString("BadImageFormat_ResType_SerBlobMismatch");

    internal static string BadImageFormat_TypeMismatch => GetResourceString("BadImageFormat_TypeMismatch");

    internal static string Format_Bad7BitInt32 => GetResourceString("Format_Bad7BitInt32");

    internal static string InvalidOperation_EnumEnded => GetResourceString("InvalidOperation_EnumEnded");

    internal static string InvalidOperation_EnumNotStarted => GetResourceString("InvalidOperation_EnumNotStarted");

    internal static string InvalidOperation_ResourceNotString_Type => GetResourceString("InvalidOperation_ResourceNotString_Type");

    internal static string InvalidOperation_ResourceWriterSaved => GetResourceString("InvalidOperation_ResourceWriterSaved");

    internal static string NotSupported_BinarySerializedResources => GetResourceString("NotSupported_BinarySerializedResources");

    internal static string NotSupported_ResourceObjectSerialization => GetResourceString("NotSupported_ResourceObjectSerialization");

    internal static string NotSupported_UnseekableStream => GetResourceString("NotSupported_UnseekableStream");

    internal static string NotSupported_WrongResourceReader_Type => GetResourceString("NotSupported_WrongResourceReader_Type");

    internal static string ObjectDisposed_ResourceSet => GetResourceString("ObjectDisposed_ResourceSet");

    internal static string ResourceManager_ReflectionNotAllowed => GetResourceString("ResourceManager_ReflectionNotAllowed");

    internal static string ResourceReaderIsClosed => GetResourceString("ResourceReaderIsClosed");

    internal static string Resources_StreamNotValid => GetResourceString("Resources_StreamNotValid");

    internal static string TypeLoadException_CannotLoadConverter => GetResourceString("TypeLoadException_CannotLoadConverter");

    internal static string Serialization_ArrayContainedNulls => GetResourceString("Serialization_ArrayContainedNulls");

    internal static string Serialization_InvalidValue => GetResourceString("Serialization_InvalidValue");

    internal static string Serialization_UnexpectedNullRecordCount => GetResourceString("Serialization_UnexpectedNullRecordCount");

    internal static string Serialization_MaxArrayLength => GetResourceString("Serialization_MaxArrayLength");

    internal static string NotSupported_RecordType => GetResourceString("NotSupported_RecordType");

    internal static string Serialization_InvalidReference => GetResourceString("Serialization_InvalidReference");

    internal static string Serialization_InvalidTypeName => GetResourceString("Serialization_InvalidTypeName");

    internal static string Serialization_TypeMismatch => GetResourceString("Serialization_TypeMismatch");

    internal static string NotSupported_NonZeroOffsets => GetResourceString("NotSupported_NonZeroOffsets");

    internal static string Serialization_Cycle => GetResourceString("Serialization_Cycle");

    internal static string Serialization_Incomplete => GetResourceString("Serialization_Incomplete");

    internal static string Serialization_IObjectReferenceOnlyPrimivite => GetResourceString("Serialization_IObjectReferenceOnlyPrimivite");

    internal static string Serialization_MissingCtor => GetResourceString("Serialization_MissingCtor");

    internal static string Serialization_MissingField => GetResourceString("Serialization_MissingField");

    internal static string Serialization_MissingType => GetResourceString("Serialization_MissingType");

    internal static string Serialization_Surrogates => GetResourceString("Serialization_Surrogates");

    internal static string Serialization_TypeNotSerializable => GetResourceString("Serialization_TypeNotSerializable");

    internal static string Serialization_InvalidTypeOrAssemblyName => GetResourceString("Serialization_InvalidTypeOrAssemblyName");

    internal static string Argument_NonSeekableStream => GetResourceString("Argument_NonSeekableStream");

    internal static string Serialization_DuplicateMemberName => GetResourceString("Serialization_DuplicateMemberName");

    internal static string Serialization_DuplicateSerializationRecordId => GetResourceString("Serialization_DuplicateSerializationRecordId");

    internal static string Serialization_MemberTypeMismatchException => GetResourceString("Serialization_MemberTypeMismatchException");

    private static bool GetUsingResourceKeysSwitchValue()
    {
        bool isEnabled;
        if (!AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out isEnabled))
        {
            return false;
        }
        return isEnabled;
    }

    internal static bool UsingResourceKeys()
    {
        return s_usingResourceKeys;
    }

    private static string GetResourceString(string resourceKey)
    {
        return resourceKey;
        //if (UsingResourceKeys())
        //{
        //    return resourceKey;
        //}
        //string result = null;
        //try
        //{
        //    result = ResourceManager.GetString(resourceKey);
        //}
        //catch (MissingManifestResourceException)
        //{
        //}
        //return result;
    }

    private static string GetResourceString(string resourceKey, string defaultString)
    {
        string resourceString = GetResourceString(resourceKey);
        if (!(resourceKey == resourceString) && resourceString != null)
        {
            return resourceString;
        }
        return defaultString;
    }

    internal static string Format(string resourceFormat, object p1)
    {
        return string.Format(resourceFormat, p1);
    }

    internal static string Format(string resourceFormat, object p1, object p2)
    {
        return string.Format(resourceFormat, p1, p2);
    }

    internal static string Format(string resourceFormat, object p1, object p2, object p3)
    {
        return string.Format(resourceFormat, p1, p2, p3);
    }

    internal static string Format(string resourceFormat, params object[] args)
    {
        if (args != null)
        {
            if (UsingResourceKeys())
            {
                return resourceFormat + ", " + string.Join(", ", args);
            }
            return string.Format(resourceFormat, args);
        }
        return resourceFormat;
    }

    internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
    {
        return string.Format(provider, resourceFormat, p1);
    }

    internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
    {
        return string.Format(provider, resourceFormat, p1, p2);
    }

    internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
    {
        return string.Format(provider, resourceFormat, p1, p2, p3);
    }

    internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
    {
        if (args != null)
        {
            if (UsingResourceKeys())
            {
                return resourceFormat + ", " + string.Join(", ", args);
            }
            return string.Format(provider, resourceFormat, args);
        }
        return resourceFormat;
    }
}
