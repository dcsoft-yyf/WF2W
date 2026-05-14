// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Resources.Extensions;
using System.Windows.Forms;

namespace System.ComponentModel
{
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    public class WF2WComponentResourceManager : IDisposable
    {
        private List<string> _resourceKeys = null;
        private List<object> _resourceValues = null;
        public WF2WComponentResourceManager()
        {

        }
        public WF2WComponentResourceManager(string strBaseName, System.Reflection.Assembly asm)
        {
            var strResourceName = strBaseName + ".resources";
            var stream = asm.GetManifestResourceStream(strResourceName);
            if (stream == null)
            {
                var strNames = asm.GetManifestResourceNames();
                foreach (var strName in strNames)
                {
                    if (strName.EndsWith(strResourceName, StringComparison.OrdinalIgnoreCase))
                    {
                        stream = asm.GetManifestResourceStream(strName);
                        break;
                    }
                }
                if (stream == null)
                {
                    var index = strResourceName.LastIndexOf('.');
                    if (index > 0)
                    {
                        var index2 = strResourceName.LastIndexOf('.', index - 1);
                        if (index2 > 0)
                        {
                            var shortName2 = strResourceName.Substring(index2 + 1);
                            foreach (var strName in strNames)
                            {
                                if (strName.EndsWith(shortName2, StringComparison.OrdinalIgnoreCase))
                                {
                                    stream = asm.GetManifestResourceStream(strName);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (stream == null)
            {
                throw new ArgumentException("Resource not found for " + strResourceName);// ThrowHelper.ThrowArgumentException_ResourceNotFound(t.FullName);
            }

            var reader = new DCDeserializingResourceReader(stream);

            var enm = reader.GetEnumerator();
            this._resourceKeys = new List<string>();
            this._resourceValues = new List<object>();
            while (enm.MoveNext())
            {
                var key = enm.Key as string;
                var value = enm.Value;
                if (key != null && key.Length > 0)
                {
                    this._resourceKeys.Add(key);
                    this._resourceValues.Add(value);
                    if( value is Bitmap bmp )
                    {
                        bmp._FromAssemblyResource = true;
                    }
                }
            }
        }
        public WF2WComponentResourceManager(Type t) : this(t.FullName, t.Assembly)
        {
            //var asm = t.Assembly;
            //var strTargetName = t.FullName + ".resources";
            //var stream = asm.GetManifestResourceStream(strTargetName);
            //if (stream == null)
            //{
            //    var strNames = asm.GetManifestResourceNames();
            //    foreach (var strName in strNames)
            //    {
            //        if (strName.EndsWith(strTargetName, StringComparison.OrdinalIgnoreCase))
            //        {
            //            stream = asm.GetManifestResourceStream(strName);
            //            break;
            //        }
            //    }
            //    if (stream == null)
            //    {
            //        strTargetName = t.Name + ".resources";
            //        foreach (var strName in strNames)
            //        {
            //            if (strName.EndsWith(strTargetName, StringComparison.OrdinalIgnoreCase))
            //            {
            //                stream = asm.GetManifestResourceStream(strName);
            //                break;
            //            }
            //        }
            //    }
            //}

            //if (stream == null)
            //{
            //    throw new ArgumentException("Resource not found for " + t.FullName);// ThrowHelper.ThrowArgumentException_ResourceNotFound(t.FullName);
            //}

            //var reader = new DCDeserializingResourceReader(stream);

            //var enm = reader.GetEnumerator();
            //this._resourceSet = new SortedList<string, object>();
            //while (enm.MoveNext())
            //{
            //    var key = enm.Key as string;
            //    var value = enm.Value;
            //    if (key != null && key.Length > 0)
            //    {
            //        this._resourceSet[key] = value;
            //    }
            //}
        }
        private System.Resources.ResourceSet _Set = null;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public System.Resources.ResourceSet GetResourceSet(System.Globalization.CultureInfo info, bool v1, bool v2)
        {
            if(this._Set == null && this._resourceKeys!= null )
            {
                this._Set = new ResourceSet(new MyReader(this._resourceKeys, this._resourceValues));
            }
            return this._Set;
        }

        private struct MyReader : IResourceReader, IDictionaryEnumerator
        {
            public MyReader( List<string> keys , List<object > vValues )
            {
                this._Keys = keys;
                this._Values = vValues;
            }
            private List<string> _Keys = null;
            private List<object> _Values = null;
            private int _CurrentIndex = 0;
            DictionaryEntry IDictionaryEnumerator.Entry => new DictionaryEntry(this._Keys[this._CurrentIndex], this._Values[this._CurrentIndex]);

            object IDictionaryEnumerator.Key => this._Keys[this._CurrentIndex];

            object? IDictionaryEnumerator.Value => this._Values[this._CurrentIndex];

            object IEnumerator.Current => this._Values[this._CurrentIndex];

            void IResourceReader.Close()
            {
                this._Keys = null;
                this._Values = null;
            }

            void IDisposable.Dispose()
            {
                this._Keys = null;
                this._Values = null;
            }

            IDictionaryEnumerator IResourceReader.GetEnumerator()
            {
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }

            bool IEnumerator.MoveNext()
            {
                this._CurrentIndex++;
                return this._CurrentIndex < this._Keys.Count;
            }

            void IEnumerator.Reset()
            {
                this._CurrentIndex = 0;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void ReleaseAllResources()
        {
            if (this._resourceKeys != null)
            {
                this._resourceKeys.Clear();
                this._resourceKeys = null;
                foreach (var item in this._resourceValues)
                {
                    if (item is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
                this._resourceValues.Clear();
                this._resourceValues = null;
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual Type ResourceSetType
        {
            get
            {
                return this.GetType();
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Dispose()
        {
            this.ReleaseAllResources();
        }
        private bool _IgnoreCase = true;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual bool IgnoreCase
        {
            get { return this._IgnoreCase; }
            set { this._IgnoreCase = value; }
        }
        private string _BaseName = string.Empty;
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual string BaseName
        {
            get { return this._BaseName; }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual string GetString(string strKey)
        {
            return GetObject(strKey) as string;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual string GetString(string name, System.Globalization.CultureInfo culture)
        {
            return GetObject(name, culture) as string;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual object GetObject(string strName)
        {
            if (this._resourceKeys != null)
            {
                for (int i = this._resourceKeys.Count - 1; i >= 0; i--)
                {
                    var key = this._resourceKeys[i];
                    if (this._IgnoreCase)
                    {
                        if (string.Compare(key, strName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            return this._resourceValues[i];
                        }
                    }
                    else
                    {
                        if (string.CompareOrdinal(key, strName) == 0)
                        {
                            return this._resourceValues[i];
                        }
                    }
                }
            }
            return null;
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual object GetObject(string strName, System.Globalization.CultureInfo info)
        {
            return this.GetObject(strName);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void ApplyResources(object value, string objectName) => ApplyResources(value, objectName, null);

        /// <summary>
        /// This method examines all the resources for the provided culture.
        /// When it finds a resource with a key in the format of
        /// &quot;[objectName].[property name]&quot; or &quot;[objectName]-[property name]&quot; it will apply that resource's value
        /// to the corresponding property on the object. If there is no matching
        /// property the resource will be ignored.
        /// </summary>
        [RequiresUnreferencedCode("The Type of value cannot be statically discovered.")]
        public virtual void ApplyResources(object value, string objectName, CultureInfo? culture)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(objectName);
            ApplyResources(value, value.GetType(), objectName, culture);
        }


        internal static void ValidateRegisteredType(Type type)
        {
            TypeDescriptionProvider provider = TypeDescriptor.GetProvider(type);
            if (provider.RequireRegisteredTypes.GetValueOrDefault() && !provider.IsRegisteredType(type))
            {
                throw new InvalidOperationException(type.FullName);// ThrowHelper.ThrowInvalidOperationException_RegisterTypeRequired(type);
            }
        }


        private void ApplyResources(object value, Type typeFromValue, string objectName, CultureInfo? culture)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
            if (IgnoreCase)
            {
                flags |= BindingFlags.IgnoreCase;
            }

            bool componentReflect = false;
            if (value is IComponent)
            {
                ISite? site = ((IComponent)value).Site;
                if (site != null && site.DesignMode)
                {
                    componentReflect = true;
                }
            }

            for (var iCount = this._resourceKeys.Count - 1; iCount >= 0; iCount--)
            {
                var strKey = this._resourceKeys[iCount];
                var vValue = this._resourceValues[iCount];
                if (IgnoreCase)
                {
                    if (string.Compare(strKey, 0, objectName, 0, objectName.Length, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        continue;
                    }
                }
                else
                {
                    if (string.CompareOrdinal(strKey, 0, objectName, 0, objectName.Length) != 0)
                    {
                        continue;
                    }
                }

                // Character after objectName.Length should be a "." or a '-', or else we should continue.
                int idx = objectName.Length;
                if (strKey.Length <= idx || (strKey[idx] != '.' && strKey[idx] != '-'))
                {
                    continue;
                }

                // Bypass type descriptor if we are not in design mode. TypeDescriptor does an attribute
                // scan which is quite expensive.
                string propName = strKey.Substring(idx + 1);
                //Console.WriteLine( value.GetType().FullName + "." + propName + " <= " + (kvp.Value != null ? kvp.Value.GetType().FullName : "null"));
                try
                {
                    if (componentReflect)
                    {
                        PropertyDescriptor? prop = TypeDescriptorGetProperties(value).Find(propName, IgnoreCase);

                        if (prop != null && !prop.IsReadOnly && (vValue == null || prop.PropertyType.IsInstanceOfType(vValue)))
                        {
                            prop.SetValue(value, vValue);
                        }
                    }
                    else
                    {
                        PropertyInfo? prop;

                        try
                        {
                            prop = prop = TrimSafeReflectionHelperGetProperty(typeFromValue, propName, flags);
                        }
                        catch (AmbiguousMatchException)
                        {
                            // Looks like we ran into a conflict between a declared property and an inherited one.
                            // In such cases, we choose the most declared one.
                            Type? t = typeFromValue;
                            do
                            {
                                prop = TrimSafeReflectionHelperGetProperty(t, propName, flags | BindingFlags.DeclaredOnly);
                                t = t.BaseType;
                            } while (prop == null && t != null && t != typeof(object));
                        }

                        if (prop != null && prop.CanWrite && (vValue == null || prop.PropertyType.IsInstanceOfType(vValue)))
                        {
                            prop.SetValue(value, vValue, null);
                        }
                    }
                }
                catch (Exception ext)
                {
                    Console.WriteLine(ext.ToString());
                }
            }

            [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode",
                Justification = "Calling method either has RequiresUnreferencedCode or has registered types.")]
            static PropertyDescriptorCollection TypeDescriptorGetProperties(object value) => TypeDescriptor.GetProperties(value);
        }

        public static PropertyInfo TrimSafeReflectionHelperGetProperty(Type type, string name, BindingFlags bindingAttr)
        {
            return type.GetProperty(name, bindingAttr);
        }
    }
    /// <summary>
    /// Ä£ÄâÀàÐÍ System.CodeDom.MemberAttributes
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum MySystemCodeDomMemberAttributes
    {
        // Scope (low bits)
        Abstract = 0x0001,
        Final = 0x0002,
        Static = 0x0003,
        Override = 0x0004,
        Const = 0x0005,
        ScopeMask = 0x000F,
        New = 0x0010,

        // VTable flags
        Overloaded = 0x0100,
        VTableMask = 0x0300,

        // Access (high bits)
        AccessMask = 0xF000,
        Private = 0x1000,
        FamilyAndAssembly = 0x2000,
        Assembly = 0x3000,
        Family = 0x4000,
        FamilyOrAssembly = 0x5000,
        Public = 0x6000,
    }

}
