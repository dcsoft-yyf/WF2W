//------------------------------------------------------------------------------
// <copyright file="COM2TypeInfoProcessor.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms.ComponentModel.Com2Interop {
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;
    using System.ComponentModel;
    using System.Diagnostics;
    using System;
    using System.Windows.Forms;
    using System.ComponentModel.Design;    
    using Microsoft.Win32;
    using System.Collections;
    using Hashtable = System.Collections.Hashtable;
    
    using System.Reflection.Emit;
    using System.Reflection;
    using System.Threading;
    using System.Globalization;
   

    /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor"]/*' />
    /// <devdoc>
    /// This is the main worker class of Com2 property interop. It takes an IDispatch Object
    /// and translates it's ITypeInfo into Com2PropertyDescriptor objects that are understandable
    /// by managed code.
    ///
    /// This class only knows how to process things that are natively in the typeinfo.  Other property
    /// information such as IPerPropertyBrowsing is handled elsewhere.
    /// </devdoc>
    internal class Com2TypeInfoProcessor {
        
        #if DEBUG
        private static TraceSwitch DbgTypeInfoProcessorSwitch = new TraceSwitch("DbgTypeInfoProcessor", "Com2TypeInfoProcessor: debug Com2 type info processing");
        #else
        private static TraceSwitch DbgTypeInfoProcessorSwitch;
        #endif
        
        private Com2TypeInfoProcessor() {
        }
        
        private static ModuleBuilder moduleBuilder = null;
        
        private static ModuleBuilder ModuleBuilder {
            get {
               //if (moduleBuilder == null) {
               //   AppDomain currentDomain =  Thread.GetDomain();
               //   AssemblyName assemblyName = new AssemblyName();
               //   assemblyName.Name = "COM2InteropEmit";
               //   AssemblyBuilder aBuilder = currentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
               //   moduleBuilder = aBuilder.DefineDynamicModule("COM2Interop.Emit");
               //}
               return moduleBuilder;
            }
        }
        
        private static Hashtable builtEnums;
        private static Hashtable processedLibraries;
        
         
        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.FindTypeInfo"]/*' />
        /// <devdoc>
        /// Given an Object, this attempts to locate its type ifo
        /// </devdoc>
        public static WinFormUnsafeNativeMethods.ITypeInfo FindTypeInfo(Object obj, bool wantCoClass) {
            WinFormUnsafeNativeMethods.ITypeInfo  pTypeInfo = null;

            // this is kind of odd.  What's going on here is that
            // if we want the CoClass (e.g. for the interface name),
            // we need to look for IProvideClassInfo first, then
            // look for the typeinfo from the IDispatch.
            // In the case of many OleAut32 operations, the CoClass
            // doesn't have the interface members on it, although
            // in the shell it usually does, so
            // we need to re-order the lookup if we _actually_ want
            // the CoClass if it's available.
            //

            for (int i = 0; pTypeInfo == null && i < 2; i++) {

                  if (wantCoClass == (i == 0)){
                        if (obj is WinFormNativeMethods.IProvideClassInfo) {
                            WinFormNativeMethods.IProvideClassInfo pProvideClassInfo = (WinFormNativeMethods.IProvideClassInfo)obj;
                            try {
                                pTypeInfo = pProvideClassInfo.GetClassInfo();
                            }
                            catch {
                            }
                        }
                  }
                  else {
                       if (obj is WinFormUnsafeNativeMethods.IDispatch) {
                            WinFormUnsafeNativeMethods.IDispatch iDispatch = (WinFormUnsafeNativeMethods.IDispatch)obj;
                            try {
                                pTypeInfo = iDispatch.GetTypeInfo(0, WinFormSafeNativeMethods.GetThreadLCID());
                            }
                            catch {
                            }
                       }
                  }

            }
            return pTypeInfo;
        }


        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.FindTypeInfos"]/*' />
        /// <devdoc>
        /// Given an Object, this attempts to locate its type info. If it implementes IProvideMultipleClassInfo
        /// all available type infos will be returned, otherwise the primary one will be alled.
        /// </devdoc>
        public static WinFormUnsafeNativeMethods.ITypeInfo[] FindTypeInfos(Object obj, bool wantCoClass){
            
            WinFormUnsafeNativeMethods.ITypeInfo[] typeInfos = null;
            int n = 0;
            WinFormUnsafeNativeMethods.ITypeInfo temp = null;

            if (obj is WinFormNativeMethods.IProvideMultipleClassInfo) {
               WinFormNativeMethods.IProvideMultipleClassInfo pCI = (WinFormNativeMethods.IProvideMultipleClassInfo)obj;
               if (!WinFormNativeMethods.Succeeded(pCI.GetMultiTypeInfoCount(ref n)) || n == 0) {
                  n = 0;
               }

               if (n > 0){
                  typeInfos = new WinFormUnsafeNativeMethods.ITypeInfo[n];
                  
                  for (int i = 0; i < n; i++){
                     if (WinFormNativeMethods.Failed(pCI.GetInfoOfIndex(i, 1 /*MULTICLASSINFO_GETTYPEINFO*/, ref temp, 0, 0, IntPtr.Zero, IntPtr.Zero))){
                        continue;
                     }
                     Debug.Assert(temp != null, "IProvideMultipleClassInfo::GetInfoOfIndex returned S_OK for ITypeInfo index " + i + ", this is a issue in the object that's being browsed, NOT the property browser.");
                     typeInfos[i] = temp;
                  }
               }
            }

            if (typeInfos == null || typeInfos.Length == 0){
               temp = FindTypeInfo(obj, wantCoClass);
               if (temp != null) {
                   typeInfos = new WinFormUnsafeNativeMethods.ITypeInfo[]{temp};
               }
            }

            return typeInfos;
        }
        
        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.GetNameDispId"]/*' />
        /// <devdoc>
        /// Retrieve the dispid of the property that we are to use as the name
        /// member.  In this case, the grid will put parens around the name.
        /// </devdoc>
        public static int GetNameDispId(WinFormUnsafeNativeMethods.IDispatch obj){
            int dispid = WinFormNativeMethods.DISPID_UNKNOWN;
            string[] names = null;
            
            ComNativeDescriptor cnd = ComNativeDescriptor.Instance;
            bool succeeded = false;

            // first try to find one with a valid value
            cnd.GetPropertyValue(obj, "__id", ref succeeded);
            
            if (succeeded) {
               names = new string[]{"__id"};
            }
            else {
               cnd.GetPropertyValue(obj, WinFormNativeMethods.ActiveX.DISPID_Name, ref succeeded);
               if (succeeded) {
                  dispid = WinFormNativeMethods.ActiveX.DISPID_Name;
               }
               else {
                  cnd.GetPropertyValue(obj, "Name", ref succeeded);
                  if (succeeded) {
                     names = new string[]{"Name"};
                  }
               }
            }
            
            // now get the dispid of the one that worked...
            if (names != null) {
               int[] pDispid = new int[]{WinFormNativeMethods.DISPID_UNKNOWN};
               Guid g = Guid.Empty;
               int hr = obj.GetIDsOfNames(ref g, names, 1, WinFormSafeNativeMethods.GetThreadLCID(), pDispid);
               if (WinFormNativeMethods.Succeeded(hr)){

                  dispid = pDispid[0];
               }
            }
            
            return dispid;
        }


        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.GetProperties"]/*' />
        /// <devdoc>
        /// Gets the properties for a given Com2 Object.  The returned Com2Properties
        /// Object contains the properties and relevant data about them.
        /// </devdoc>
        public static Com2Properties GetProperties(Object obj) {
            
            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "Com2TypeInfoProcessor.GetProperties");
            
            if (obj == null || !Marshal.IsComObject(obj)) {
                Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "Com2TypeInfoProcessor.GetProperties returning null: Object is not a com Object");
                return null;
            }

            WinFormUnsafeNativeMethods.ITypeInfo[] typeInfos = FindTypeInfos(obj, false);

            // oops, looks like this guy doesn't surface any type info
            // this is okay, so we just say it has no props
            if (typeInfos == null || typeInfos.Length == 0) {
                Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "Com2TypeInfoProcessor.GetProperties :: Didn't get typeinfo");
                return null;
            }


            int defaultProp = -1;
            int temp = -1;
            ArrayList propList = new ArrayList();
            Guid[] typeGuids = new Guid[typeInfos.Length];

            for (int i = 0; i < typeInfos.Length; i++) {
               WinFormUnsafeNativeMethods.ITypeInfo ti = typeInfos[i];

               if (ti == null) {
                   continue;
               }

               int[] versions = new int[2];
               Guid typeGuid = GetGuidForTypeInfo(ti, null, versions);
               PropertyDescriptor[] props = null;
               bool dontProcess = typeGuid != Guid.Empty && processedLibraries != null && processedLibraries.Contains(typeGuid);

               if (dontProcess) {
                    CachedProperties cp = (CachedProperties)processedLibraries[typeGuid];
                    
                    if (versions[0] == cp.MajorVersion && versions[1] == cp.MinorVersion) {
                        props = cp.Properties;
                        if (i == 0 && cp.DefaultIndex != -1) {
                            defaultProp = cp.DefaultIndex;
                        }
                    }
                    else {
                        dontProcess = false;
                    }
               }
               
               if (!dontProcess) {
                   props = InternalGetProperties(obj, ti, WinFormNativeMethods.MEMBERID_NIL, ref temp);
    
                   // only save the default property from the first type Info
                   if (i == 0 && temp != -1) {
                      defaultProp = temp;
                   }

                   if (processedLibraries == null) {
                        processedLibraries = new Hashtable();
                   }
             
                   if (typeGuid != Guid.Empty) {
                        processedLibraries[typeGuid] = new CachedProperties(props, i == 0 ? defaultProp : -1, versions[0], versions[1]);
                   }
               }

               if (props != null){
                   propList.AddRange(props);
               }
            }
            
            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "Com2TypeInfoProcessor.GetProperties : returning " + propList.Count.ToString(CultureInfo.InvariantCulture) + " properties");

            // done!
            Com2PropertyDescriptor[] temp2 = new Com2PropertyDescriptor[propList.Count];
            propList.CopyTo(temp2, 0);

            return new Com2Properties(obj, temp2, defaultProp);
        }

        private static Guid GetGuidForTypeInfo(WinFormUnsafeNativeMethods.ITypeInfo typeInfo, StructCache structCache, int[] versions) {
            IntPtr pTypeAttr = IntPtr.Zero;
            int hr = typeInfo.GetTypeAttr(ref pTypeAttr);
            if (!WinFormNativeMethods.Succeeded(hr)) {
                throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetTypeAttrFailed, hr), hr);
            }

            Guid g = Guid.Empty;
            WinFormNativeMethods.tagTYPEATTR typeAttr = null;
            try {
                

                if (structCache == null) {
                    typeAttr = new WinFormNativeMethods.tagTYPEATTR();
                }
                else {
                    typeAttr = (WinFormNativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEATTR));
                }
                WinFormUnsafeNativeMethods.PtrToStructure(pTypeAttr, typeAttr);
                g = typeAttr.guid;
                if (versions != null) {
                    versions[0] = typeAttr.wMajorVerNum;
                    versions[1] = typeAttr.wMinorVerNum;
                }
            }
            finally {
                typeInfo.ReleaseTypeAttr(pTypeAttr);
                if (structCache != null && typeAttr != null) {
                    structCache.ReleaseStruct(typeAttr);
                }
            }

            return g;
        }


        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.GetValueTypeFromTypeDesc"]/*' />
        /// <devdoc>
        /// Resolves a value type for a property from a TYPEDESC.  Value types can be
        /// user defined, which and may be aliased into other type infos.  This function
        /// will recusively walk the ITypeInfos to resolve the type to a clr Type.
        /// </devdoc>
        private static Type GetValueTypeFromTypeDesc(WinFormNativeMethods.tagTYPEDESC typeDesc, WinFormUnsafeNativeMethods.ITypeInfo typeInfo, Object[] typeData, StructCache structCache) {
            IntPtr hreftype;
            int hr = 0;

            switch ((WinFormNativeMethods.tagVT)typeDesc.vt) {
            default:
                return VTToType((WinFormNativeMethods.tagVT)typeDesc.vt);

            case WinFormNativeMethods.tagVT.VT_UNKNOWN:
            case WinFormNativeMethods.tagVT.VT_DISPATCH:
                // get the guid
                typeData[0] = GetGuidForTypeInfo(typeInfo, structCache, null);
                
                // return the type
                return VTToType((WinFormNativeMethods.tagVT)typeDesc.vt);

            case WinFormNativeMethods.tagVT.VT_USERDEFINED:
                // we'll need to recurse into a user defined reference typeinfo
                Debug.Assert(typeDesc.unionMember != IntPtr.Zero, "typeDesc doesn't contain an hreftype!");
                hreftype = typeDesc.unionMember;
                break;

            case WinFormNativeMethods.tagVT.VT_PTR:
                // we'll need to recurse into a user defined reference typeinfo
                Debug.Assert(typeDesc.unionMember != IntPtr.Zero, "typeDesc doesn't contain an refTypeDesc!");
                WinFormNativeMethods.tagTYPEDESC refTypeDesc = (WinFormNativeMethods.tagTYPEDESC)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEDESC));
                
                try {

                    try {
                        //(tagTYPEDESC)Marshal.PtrToStructure(typeDesc.unionMember, typeof(tagTYPEDESC));
                        WinFormUnsafeNativeMethods.PtrToStructure(typeDesc.unionMember, refTypeDesc);
                    }
                    catch {
                        // above is failing, why?
                        refTypeDesc = new WinFormNativeMethods.tagTYPEDESC();
                        refTypeDesc.unionMember = (IntPtr)Marshal.ReadInt32(typeDesc.unionMember);
                        refTypeDesc.vt = Marshal.ReadInt16(typeDesc.unionMember, 4);
                    }
    
                    if (refTypeDesc.vt == (int)WinFormNativeMethods.tagVT.VT_VARIANT) {
                        return VTToType((WinFormNativeMethods.tagVT)refTypeDesc.vt);
                    }
                    hreftype = refTypeDesc.unionMember;
                }
                finally {
                    structCache.ReleaseStruct(refTypeDesc);
                }
                break;
            }

            // get the reference type info
            WinFormUnsafeNativeMethods.ITypeInfo refTypeInfo = null;

            hr = typeInfo.GetRefTypeInfo(hreftype, ref refTypeInfo);
            if (!WinFormNativeMethods.Succeeded(hr)) {
                throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetRefTypeInfoFailed, hr), hr);
            }

            try {
                // here is where we look at the next level type info.
                // if we get an enum, process it, otherwise we will recurse
                // or get a dispatch.
                //
                if (refTypeInfo != null) {
                    IntPtr pRefTypeAttr = IntPtr.Zero;
                    hr = refTypeInfo.GetTypeAttr(ref pRefTypeAttr);

                    if (!WinFormNativeMethods.Succeeded(hr)) {
                        
                        throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetTypeAttrFailed, hr), hr);
                    }

                    WinFormNativeMethods.tagTYPEATTR refTypeAttr = (WinFormNativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEATTR));//(tagTYPEATTR)Marshal.PtrToStructure(pRefTypeAttr, typeof(tagTYPEATTR));
                    WinFormUnsafeNativeMethods.PtrToStructure(pRefTypeAttr, refTypeAttr);
                    try {
                        Guid g = refTypeAttr.guid;

                        // save the guid if we've got one here
                        if (!Guid.Empty.Equals(g)){
                            typeData[0] = g;
                        }

                        switch ((WinFormNativeMethods.tagTYPEKIND)refTypeAttr.typekind) {

                            case WinFormNativeMethods.tagTYPEKIND.TKIND_ENUM:
                                return ProcessTypeInfoEnum(refTypeInfo, structCache);
                                //return VTToType(tagVT.VT_I4);
                            case WinFormNativeMethods.tagTYPEKIND.TKIND_ALIAS:
                                // recurse here
                                return GetValueTypeFromTypeDesc(refTypeAttr.Get_tdescAlias(), refTypeInfo, typeData, structCache);
                            case WinFormNativeMethods.tagTYPEKIND.TKIND_DISPATCH:
                                return VTToType(WinFormNativeMethods.tagVT.VT_DISPATCH);
                                                        case WinFormNativeMethods.tagTYPEKIND.TKIND_INTERFACE:
                                                        case WinFormNativeMethods.tagTYPEKIND.TKIND_COCLASS:
                                return VTToType(WinFormNativeMethods.tagVT.VT_UNKNOWN);
                            default:
                                return null;
                        }
                    }
                    finally {
                        refTypeInfo.ReleaseTypeAttr(pRefTypeAttr);
                        structCache.ReleaseStruct(refTypeAttr);
                    }
                }
            }
            finally {
                refTypeInfo = null;
            }
            return null;
        }

        private static PropertyDescriptor[] InternalGetProperties(Object obj, WinFormUnsafeNativeMethods.ITypeInfo typeInfo, int dispidToGet, ref int defaultIndex) {
        
            if (typeInfo == null) {
                return null;
            }
            
            Hashtable propInfos = new Hashtable();
            
            int nameDispID = GetNameDispId((WinFormUnsafeNativeMethods.IDispatch)obj);
            bool addAboutBox = false;
            
            StructCache structCache = new StructCache();            
            
            // properties can live as functions with get_ and put_ or
            // as variables, so we do two steps here.
            try {
                // DO FUNCDESC things
                ProcessFunctions(typeInfo, propInfos, dispidToGet, nameDispID, ref addAboutBox, structCache);
            }
            catch (ExternalException ex) {
                Debug.Fail("ProcessFunctions failed with hr=" + ex.ErrorCode.ToString(CultureInfo.InvariantCulture) + ", message=" + ex.ToString());
            }

            try {
                // DO VARDESC things.
                ProcessVariables(typeInfo, propInfos, dispidToGet, nameDispID, structCache);
            }
            catch (ExternalException ex) {
                Debug.Fail("ProcessVariables failed with hr=" + ex.ErrorCode.ToString(CultureInfo.InvariantCulture) + ", message=" + ex.ToString());
            }

            typeInfo = null;


            // now we take the propertyInfo structures we built up
            // and use them to create the actual descriptors.
            int cProps = propInfos.Count;
            
            if (addAboutBox) {
               cProps++;
            }
            
            PropertyDescriptor[] props = new PropertyDescriptor[cProps];
            int defaultProp = -1;
            
            int hr = WinFormNativeMethods.S_OK;
            Object[] pvar = new Object[1];
            ComNativeDescriptor cnd = ComNativeDescriptor.Instance;

            // for each item in uur list, create the descriptor an check
            // if it's the default one.
            foreach (PropInfo pi in propInfos.Values){
                if (!pi.NonBrowsable) {
                    // finally, for each property, make sure we can get the value
                    // if we can't then we should mark it non-browsable

                    try {
                        hr = cnd.GetPropertyValue(obj, pi.DispId, pvar);
                    }
                    catch (ExternalException ex) {
                        hr = ex.ErrorCode;
                        Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "IDispatch::Invoke(PROPGET, " +  pi.Name + ") threw an exception :" + ex.ToString());
                    }
                    if (!WinFormNativeMethods.Succeeded(hr)) {
                        Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, String.Format(CultureInfo.CurrentCulture, "Adding Browsable(false) to property '" + pi.Name + "' because Invoke(dispid=0x{0:X} ,DISPATCH_PROPERTYGET) returned hr=0x{1:X}.  Properties that do not return S_OK are hidden by default.", pi.DispId, hr));
                        pi.Attributes.Add(new BrowsableAttribute(false));
                        pi.NonBrowsable = true;
                    }
                }
                else {
                    hr = WinFormNativeMethods.S_OK;
                }

                Attribute[] temp = new Attribute[pi.Attributes.Count];
                pi.Attributes.CopyTo(temp, 0);
                //Debug.Assert(pi.nonbrowsable || pi.valueType != null, "Browsable property '" + pi.name + "' has a null type");
                props[pi.Index] = new Com2PropertyDescriptor(pi.DispId, pi.Name, temp, pi.ReadOnly != PropInfo.ReadOnlyFalse, pi.ValueType, pi.TypeData, !WinFormNativeMethods.Succeeded(hr));
                if (pi.IsDefault) {
                    defaultProp = pi.Index;
                }
            }
            
            if (addAboutBox) {
               props[props.Length-1] = new Com2AboutBoxPropertyDescriptor();
            }
            return props;
        }


        private static PropInfo ProcessDataCore(WinFormUnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispid, int nameDispID, WinFormNativeMethods.tagTYPEDESC typeDesc, int flags, StructCache structCache) {
            string          pPropName = null;
            string          pPropDesc = null;


            // get the name and the helpstring
            int hr = typeInfo.GetDocumentation(dispid, ref pPropName, ref pPropDesc, null, null);

            ComNativeDescriptor cnd = ComNativeDescriptor.Instance;


            if (!WinFormNativeMethods.Succeeded(hr)) {
                throw new COMException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetDocumentationFailed, dispid, hr, cnd.GetClassName(typeInfo)), hr);
            }

            if (pPropName == null){
               Debug.Fail(String.Format(CultureInfo.CurrentCulture, "ITypeInfo::GetDocumentation didn't return a name for DISPID 0x{0:X} but returned SUCEEDED(hr),  Component=" + cnd.GetClassName(typeInfo), dispid));
               return null;
            }

            // now we can create our struct... make sure we don't already have one
            PropInfo pi = (PropInfo)propInfoList[pPropName];

            if (pi == null) {
                pi = new PropInfo();
                pi.Index = propInfoList.Count;
                propInfoList[pPropName] = pi;
                pi.Name = pPropName;
                pi.DispId = dispid;
                pi.Attributes.Add(new DispIdAttribute(pi.DispId));
            }

            if (pPropDesc != null) {
                pi.Attributes.Add(new DescriptionAttribute(pPropDesc));
            }

            // figure out the value type
            if (pi.ValueType == null) {
                Object[] pTypeData = new Object[1];
                try {
                    pi.ValueType = GetValueTypeFromTypeDesc(typeDesc, typeInfo, pTypeData, structCache);
                }
                catch (Exception ex) {
                    Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "Hiding property " + pi.Name + " because value Type could not be resolved: " + ex.ToString());
                }

                // if we can't resolve the type, mark the property as nonbrowsable
                // from the browser
                //
                if (pi.ValueType == null) {
                    pi.NonBrowsable = true;
                }

                if (pi.NonBrowsable) {
                    flags |= (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FNONBROWSABLE;
                }

                if (pTypeData[0] != null) {
                    pi.TypeData = pTypeData[0];
                }
            }

            // check the flags
            if ((flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FREADONLY) != 0) {
                pi.ReadOnly = PropInfo.ReadOnlyTrue;
            }

            if ((flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FHIDDEN) != 0 ||
                (flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FNONBROWSABLE) != 0 ||
                pi.Name[0] == '_' ||
                dispid == WinFormNativeMethods.ActiveX.DISPID_HWND) {
                pi.Attributes.Add(new BrowsableAttribute(false));
                pi.NonBrowsable = true;
            }

            if ((flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FUIDEFAULT) != 0) {
                pi.IsDefault = true;
            }

            if ((flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FBINDABLE) != 0 &&
                (flags & (int)WinFormNativeMethods.tagVARFLAGS.VARFLAG_FDISPLAYBIND) != 0) {
                pi.Attributes.Add(new BindableAttribute(true));
            }

            // lastly, if it's DISPID_Name, add the ParenthesizeNameAttribute
            if (dispid == nameDispID){
                pi.Attributes.Add(new ParenthesizePropertyNameAttribute(true));
                
                // don't allow merges on the name
                pi.Attributes.Add(new MergablePropertyAttribute(false));
            }

            return pi;
        }

        private static void ProcessFunctions(WinFormUnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, ref bool addAboutBox, StructCache structCache) {
            IntPtr pTypeAttr = IntPtr.Zero;
            int hr = typeInfo.GetTypeAttr(ref pTypeAttr);

            if (!WinFormNativeMethods.Succeeded(hr) || pTypeAttr == IntPtr.Zero) {
                throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetTypeAttrFailed, hr), hr);
            }

            WinFormNativeMethods.tagTYPEATTR         typeAttr = (WinFormNativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEATTR));//(tagTYPEATTR)Marshal.PtrToStructure(pTypeAttr, typeof(tagTYPEATTR));
            WinFormUnsafeNativeMethods.PtrToStructure(pTypeAttr, typeAttr);
            if (typeAttr == null) {
                return;
            }
            WinFormNativeMethods.tagFUNCDESC         funcDesc = null;
            WinFormNativeMethods.tagELEMDESC         ed = null;
            try {
                
                funcDesc = (WinFormNativeMethods.tagFUNCDESC)structCache.GetStruct(typeof(WinFormNativeMethods.tagFUNCDESC));
                ed = (WinFormNativeMethods.tagELEMDESC)structCache.GetStruct(typeof(WinFormNativeMethods.tagELEMDESC));
                
                bool              isPropGet;
                PropInfo          pi;

                for (int i = 0; i < typeAttr.cFuncs; i++) {
                    IntPtr pFuncDesc = IntPtr.Zero;
                    hr = typeInfo.GetFuncDesc(i, ref pFuncDesc);

                    if (!WinFormNativeMethods.Succeeded(hr) || pFuncDesc == IntPtr.Zero) {
                        Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, String.Format(CultureInfo.CurrentCulture, "ProcessTypeInfoEnum: ignoring function item 0x{0:X} because ITypeInfo::GetFuncDesc returned hr=0x{1:X} or NULL", i, hr));
                        continue;
                    }

                    //funcDesc = (tagFUNCDESC)Marshal.PtrToStructure(pFuncDesc, typeof(tagFUNCDESC));
                    WinFormUnsafeNativeMethods.PtrToStructure(pFuncDesc, funcDesc);
                    try {
                        if (funcDesc.invkind == (int)WinFormNativeMethods.tagINVOKEKIND.INVOKE_FUNC ||
                            (dispidToGet != WinFormNativeMethods.MEMBERID_NIL && funcDesc.memid != dispidToGet)) {
                            
                            if (funcDesc.memid == WinFormNativeMethods.ActiveX.DISPID_ABOUTBOX) {
                               addAboutBox = true;
                            }
                            continue;
                        }

                        WinFormNativeMethods.tagTYPEDESC typeDesc;

                        // is this a get or a put?
                        isPropGet = (funcDesc.invkind == (int)WinFormNativeMethods.tagINVOKEKIND.INVOKE_PROPERTYGET);

                        if (isPropGet) {

                            if (funcDesc.cParams != 0) {
                                
                                continue;
                            }

                            typeDesc = funcDesc.elemdescFunc.tdesc;
                        }
                        else {
                            Debug.Assert(funcDesc.lprgelemdescParam != IntPtr.Zero, "ELEMDESC param is null!");
                            if (funcDesc.lprgelemdescParam == IntPtr.Zero || funcDesc.cParams != 1) {
                                
                                continue;
                            }
                            Marshal.PtrToStructure(funcDesc.lprgelemdescParam, ed);
                            typeDesc = ed.tdesc;
                        }
                        pi = ProcessDataCore(typeInfo, propInfoList, funcDesc.memid, nameDispID, typeDesc, funcDesc.wFuncFlags, structCache);

                        // if we got a setmethod, it's not readonly
                        if (pi != null && !isPropGet) {
                            pi.ReadOnly = PropInfo.ReadOnlyFalse;
                        }
                    }
                    finally {
                        typeInfo.ReleaseFuncDesc(pFuncDesc);
                    }
                }
            }
            finally {
                if (funcDesc != null) {
                    structCache.ReleaseStruct(funcDesc);
                }
                if (ed != null) {
                    structCache.ReleaseStruct(ed);
                }
                typeInfo.ReleaseTypeAttr(pTypeAttr);
                structCache.ReleaseStruct(typeAttr);
            }
        }

        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.ProcessTypeInfoEnum"]/*' />
        /// <devdoc>
        /// This converts a type info that describes a IDL defined enum
        /// into one we can use
        /// </devdoc>
        private static Type ProcessTypeInfoEnum(WinFormUnsafeNativeMethods.ITypeInfo enumTypeInfo, StructCache structCache) {

            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum entered");

            if (enumTypeInfo == null) {
                Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum got a NULL enumTypeInfo");
                return null;
            }

            try {
                IntPtr pTypeAttr = IntPtr.Zero;
                int hr = enumTypeInfo.GetTypeAttr(ref pTypeAttr);

                if (!WinFormNativeMethods.Succeeded(hr) || pTypeAttr == IntPtr.Zero) {
                        throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetTypeAttrFailed, hr), hr);
                }

                WinFormNativeMethods.tagTYPEATTR typeAttr = (WinFormNativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEATTR));//(tagTYPEATTR)Marshal.PtrToStructure(pTypeAttr, typeof(tagTYPEATTR));
                WinFormUnsafeNativeMethods.PtrToStructure(pTypeAttr, typeAttr);

                if (pTypeAttr == IntPtr.Zero) {
                    Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: failed to get a typeAttr");
                    return null;
                }

                try {

                    int nItems = typeAttr.cVars;

                    Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: processing " + nItems.ToString(CultureInfo.InvariantCulture) + " variables");

                    ArrayList strs = new ArrayList();
                    ArrayList vars = new ArrayList();

                    WinFormNativeMethods.tagVARDESC varDesc = (WinFormNativeMethods.tagVARDESC)structCache.GetStruct(typeof(WinFormNativeMethods.tagVARDESC));
                    Object varValue = null;
                    string enumName = null;
                    string name = null;
                    string helpstr = null;

                    enumTypeInfo.GetDocumentation(WinFormNativeMethods.MEMBERID_NIL, ref enumName, ref helpstr, null, null);
                    
                                                            // For each item in the enum type info,
                    // we just need it's name and value, and helpstring if it's there.
                    //
                    for (int i = 0; i < nItems; i++) {
                        IntPtr pVarDesc = IntPtr.Zero;
                        hr = enumTypeInfo.GetVarDesc(i, ref pVarDesc);

                        if (!WinFormNativeMethods.Succeeded(hr) || pVarDesc == IntPtr.Zero) {
                            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, String.Format(CultureInfo.CurrentCulture, "ProcessTypeInfoEnum: ignoring item 0x{0:X} because ITypeInfo::GetVarDesc returned hr=0x{1:X} or NULL", hr));
                            continue;
                        }

                        try {
                            //varDesc = (tagVARDESC)Marshal.PtrToStructure(pVarDesc, typeof(tagVARDESC));
                            WinFormUnsafeNativeMethods.PtrToStructure(pVarDesc, varDesc);

                            if (varDesc == null ||
                                varDesc.varkind != (int)WinFormNativeMethods.tagVARKIND.VAR_CONST ||
                                varDesc.unionMember == IntPtr.Zero) {
                                continue;
                            }

                            name = helpstr = null;
                            varValue = null;

                            // get the name and the helpstring

                            hr = enumTypeInfo.GetDocumentation(varDesc.memid,  ref name,  ref helpstr, null, null);


                            if (!WinFormNativeMethods.Succeeded(hr)) {
                                Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, String.Format(CultureInfo.CurrentCulture, "ProcessTypeInfoEnum: ignoring item 0x{0:X} because ITypeInfo::GetDocumentation returned hr=0x{1:X} or NULL", hr));
                                continue;
                            }

                            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum got name=" + (name == null ? "(null)" : name) + ", helpstring=" + (helpstr == null ? "(null)" : helpstr));

                            // get the value
                            try {
                                //varValue = (VARIANT)Marshal.PtrToStructure(varDesc.unionMember, typeof(VARIANT));
                                varValue = Marshal.GetObjectForNativeVariant(varDesc.unionMember);
                            }
                            catch (Exception ex) {
                                Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: PtrtoStructFailed " + ex.GetType().Name + "," + ex.Message);
                            }

                            /*if (varValue == null) {
                                Debug.Fail("Couldn't get VARIANT from VARIANTDESC");
                                continue;
                            }*/

                            //variant v = varValue.ToVariant();
                            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: adding variable value=" + Convert.ToString(varValue, CultureInfo.InvariantCulture));
                            vars.Add(varValue);

                            // if we have a helpstring, use it, otherwise use name
                            string nameString;
                            if (helpstr != null) {
                                nameString = helpstr;
                            }
                            else {
                                Debug.Assert(name != null, "No name for VARDESC member, but GetDocumentation returned S_OK!");
                                nameString = name;
                            }
                            Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: adding name value=" + nameString);
                            strs.Add(nameString);
                        }
                        finally {
                            if (pVarDesc != IntPtr.Zero) {
                                enumTypeInfo.ReleaseVarDesc(pVarDesc);
                            }
                        }
                    }
                    structCache.ReleaseStruct(varDesc);
                    Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, "ProcessTypeInfoEnum: returning enum with " + strs.Count.ToString(CultureInfo.InvariantCulture) + " items");

                    // just build our enumerator
                    if (strs.Count > 0) {
                        
                        // get the IUnknown value of the ITypeInfo
                        IntPtr pTypeInfoUnk = Marshal.GetIUnknownForObject(enumTypeInfo);
                        
                        try {
                           enumName = pTypeInfoUnk.ToString() + "_" + enumName;
                           
                           if (builtEnums == null) {
                              builtEnums = new Hashtable();
                           }
                           else if (builtEnums.ContainsKey(enumName)) {
                              return (Type)builtEnums[enumName];
                           }

                           Type enumType = typeof(int);

                           if (vars.Count > 0 && vars[0] != null) {
                               enumType = vars[0].GetType();
                           }
                           
                           EnumBuilder enumBuilder = ModuleBuilder.DefineEnum(enumName, TypeAttributes.Public, enumType);
                           for (int i = 0; i < strs.Count; i++) {
                              enumBuilder.DefineLiteral((string)strs[i], vars[i]);
                           }
                           Type t = enumBuilder.CreateType();
                           builtEnums[enumName] = t;
                           return t;
                        }
                        finally {
                           if (pTypeInfoUnk != IntPtr.Zero) {
                              Marshal.Release(pTypeInfoUnk);
                           }
                        }
                    }

                }
                finally {
                    enumTypeInfo.ReleaseTypeAttr(pTypeAttr);
                    structCache.ReleaseStruct(typeAttr);
                }
            }
            catch {
            }
            return null;
        }


        private static void ProcessVariables(WinFormUnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, StructCache structCache) {
            IntPtr pTypeAttr = IntPtr.Zero;
            int hr = typeInfo.GetTypeAttr(ref pTypeAttr);

            if (!WinFormNativeMethods.Succeeded(hr) || pTypeAttr == IntPtr.Zero) {
                throw new ExternalException(DCSR.GetString(DCSR.TYPEINFOPROCESSORGetTypeAttrFailed, hr), hr);
            }

            WinFormNativeMethods.tagTYPEATTR typeAttr = (WinFormNativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(WinFormNativeMethods.tagTYPEATTR));//(tagTYPEATTR)Marshal.PtrToStructure(pTypeAttr, typeof(tagTYPEATTR));
            WinFormUnsafeNativeMethods.PtrToStructure(pTypeAttr, typeAttr);

            try {
                if (typeAttr == null) {
                    return;
                }
                WinFormNativeMethods.tagVARDESC        varDesc = (WinFormNativeMethods.tagVARDESC)structCache.GetStruct(typeof(WinFormNativeMethods.tagVARDESC));

                for (int i = 0; i < typeAttr.cVars; i++) {
                    IntPtr pVarDesc = IntPtr.Zero;

                    hr = typeInfo.GetVarDesc(i, ref pVarDesc);
                    if (!WinFormNativeMethods.Succeeded(hr) || pVarDesc == IntPtr.Zero) {
                        Debug.WriteLineIf(DbgTypeInfoProcessorSwitch.TraceVerbose, String.Format(CultureInfo.CurrentCulture, "ProcessTypeInfoEnum: ignoring variable item 0x{0:X} because ITypeInfo::GetFuncDesc returned hr=0x{1:X} or NULL", hr));
                        continue;
                    }

                    //varDesc = (tagVARDESC)Marshal.PtrToStructure(pVarDesc, typeof(tagVARDESC));
                    WinFormUnsafeNativeMethods.PtrToStructure(pVarDesc, varDesc);

                    try {

                        if (varDesc.varkind == (int)WinFormNativeMethods.tagVARKIND.VAR_CONST ||
                            (dispidToGet != WinFormNativeMethods.MEMBERID_NIL && varDesc.memid != dispidToGet)) {
                            continue;
                        }


                        PropInfo pi = ProcessDataCore(typeInfo, propInfoList, varDesc.memid, nameDispID, varDesc.elemdescVar.tdesc, varDesc.wVarFlags, structCache);
                        if (pi.ReadOnly != PropInfo.ReadOnlyTrue) {
                            pi.ReadOnly = PropInfo.ReadOnlyFalse;
                        }
                    }
                    finally {
                        if (pVarDesc != IntPtr.Zero) {
                            typeInfo.ReleaseVarDesc(pVarDesc);
                        }
                    }
                }
                structCache.ReleaseStruct(varDesc);
            }
            finally {
                typeInfo.ReleaseTypeAttr(pTypeAttr);
                structCache.ReleaseStruct(typeAttr);
            }
        }

        private static Type VTToType(WinFormNativeMethods.tagVT vt) {
            switch (vt) {
            case WinFormNativeMethods.tagVT.VT_EMPTY:
            case WinFormNativeMethods.tagVT.VT_NULL:
                return null;
            case WinFormNativeMethods.tagVT.VT_I1:
                return typeof(SByte);
            case WinFormNativeMethods.tagVT.VT_UI1:
                return typeof(Byte);

            case WinFormNativeMethods.tagVT.VT_I2:
                return typeof(Int16);
            case WinFormNativeMethods.tagVT.VT_UI2:
                return typeof(UInt16);
                

            case WinFormNativeMethods.tagVT.VT_I4:
            case WinFormNativeMethods.tagVT.VT_INT:
                return typeof(Int32);
            
            case WinFormNativeMethods.tagVT.VT_UI4:
            case WinFormNativeMethods.tagVT.VT_UINT:
                return typeof(UInt32);
            
            case WinFormNativeMethods.tagVT.VT_I8:
                return typeof(Int64);
            case WinFormNativeMethods.tagVT.VT_UI8:
                return typeof(UInt64);

            case WinFormNativeMethods.tagVT.VT_R4:
                return typeof(float);

            case WinFormNativeMethods.tagVT.VT_R8:
                return typeof(double);

            case WinFormNativeMethods.tagVT.VT_CY:
                return typeof(Decimal);
            case WinFormNativeMethods.tagVT.VT_DATE:
                return typeof(DateTime);
            case WinFormNativeMethods.tagVT.VT_BSTR:
            case WinFormNativeMethods.tagVT.VT_LPSTR:
            case WinFormNativeMethods.tagVT.VT_LPWSTR:
                return typeof(string);

            case WinFormNativeMethods.tagVT.VT_DISPATCH:
                return typeof(WinFormUnsafeNativeMethods.IDispatch);
            case WinFormNativeMethods.tagVT.VT_UNKNOWN:
                return typeof(Object);

            case WinFormNativeMethods.tagVT.VT_ERROR:
            case WinFormNativeMethods.tagVT.VT_HRESULT:
                return typeof(int);

            case WinFormNativeMethods.tagVT.VT_BOOL:
                return typeof(bool);

            case WinFormNativeMethods.tagVT.VT_VARIANT:
                return typeof(Com2Variant);
            case WinFormNativeMethods.tagVT.VT_CLSID:
                return typeof(Guid);

            case WinFormNativeMethods.tagVT.VT_FILETIME:
                return typeof(WinFormNativeMethods.FILETIME);

            case WinFormNativeMethods.tagVT.VT_USERDEFINED:
                throw new ArgumentException(DCSR.GetString(DCSR.COM2UnhandledVT, "VT_USERDEFINED"));

                /*case VT_ENUM:
                    if (enumNames != null || null != pPropertyInfo.GetEnum()) {
                        return typeof(IEnum);
                    }
                    goto default;*/
            case WinFormNativeMethods.tagVT.VT_VOID:
            case WinFormNativeMethods.tagVT.VT_PTR:
            case WinFormNativeMethods.tagVT.VT_SAFEARRAY:
            case WinFormNativeMethods.tagVT.VT_CARRAY:

            case WinFormNativeMethods.tagVT.VT_RECORD:
            case WinFormNativeMethods.tagVT.VT_BLOB:
            case WinFormNativeMethods.tagVT.VT_STREAM:
            case WinFormNativeMethods.tagVT.VT_STORAGE:
            case WinFormNativeMethods.tagVT.VT_STREAMED_OBJECT:
            case WinFormNativeMethods.tagVT.VT_STORED_OBJECT:
            case WinFormNativeMethods.tagVT.VT_BLOB_OBJECT:
            case WinFormNativeMethods.tagVT.VT_CF:
            case WinFormNativeMethods.tagVT.VT_BSTR_BLOB:
            case WinFormNativeMethods.tagVT.VT_VECTOR:
            case WinFormNativeMethods.tagVT.VT_ARRAY:
            case WinFormNativeMethods.tagVT.VT_BYREF:
            case WinFormNativeMethods.tagVT.VT_RESERVED:
            default:
                throw new ArgumentException(DCSR.GetString(DCSR.COM2UnhandledVT, ((int)vt).ToString(CultureInfo.InvariantCulture)));
            }
        }

        internal class CachedProperties {

            private PropertyDescriptor[] props;

            public readonly int MajorVersion;
            public readonly int MinorVersion;
            private int defaultIndex;

            internal CachedProperties(PropertyDescriptor[] props, int defIndex, int majVersion, int minVersion) {
                this.props = ClonePropertyDescriptors(props);
                this.MajorVersion = majVersion;
                this.MinorVersion = minVersion;
                this.defaultIndex = defIndex;
            }

            public PropertyDescriptor[] Properties {
                get {
                    return ClonePropertyDescriptors(props);
                }
            }

            public int DefaultIndex {
                get {
                    return defaultIndex;
                }
            }

            private PropertyDescriptor[] ClonePropertyDescriptors(PropertyDescriptor[] props) {
                PropertyDescriptor[] retProps = new PropertyDescriptor[props.Length];
                for (int i = 0; i < props.Length; i++) {
                    if (props[i] is ICloneable) {
                        retProps[i] = (PropertyDescriptor)((ICloneable)props[i]).Clone();;
                    }
                    else {
                        retProps[i] = props[i];
                    }
                }
                return retProps;
            }
        }
        
        /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2TypeInfoProcessor.StructCache"]/*' />
        /// <devdoc>
        /// This class manages a cache of structures that we can use
        /// for passing into native so we don't have to create them every time.
        /// for many objects, these can be used thousands of times.
        /// </devdoc>
        public class StructCache {
           
           private Hashtable queuedTypes = new Hashtable();
           
#if DEBUG
           private Hashtable releaseCheck = new Hashtable();

           ~StructCache() {
                IEnumerator enumRelease = releaseCheck.Keys.GetEnumerator();
                
                while (enumRelease.MoveNext()) {
                    Type t = (Type)enumRelease.Current;
                    if ((int)releaseCheck[t] != 0) {
                        Debug.Assert(false, "Failed to release struct of type " + t.Name);
                    }
                }      
           }
           
#endif
           
           private Queue GetQueue(Type t, bool create) {
               Object queue = queuedTypes[t];
               
               if (queue == null && create){
                  queue = new Queue();
                  queuedTypes[t] = queue;
                  #if DEBUG
                    releaseCheck[t] = 0;
                  #endif
               }
               
               return (Queue)queue;
           }
           
           public Object GetStruct(Type t) {
               Queue queue = GetQueue(t, true);
               
               Object str = null;
               
               if (queue.Count == 0) {
                  str = Activator.CreateInstance(t);
               }
               else {
                  str = queue.Dequeue();
               }
               
               #if DEBUG
                    int count = (int)releaseCheck[t];
                    releaseCheck[t] = ++count;
               #endif
               
               return str;
           }
           
           public void ReleaseStruct(Object str) {
               Type t = str.GetType();
               Queue queue = GetQueue(t, false);
               
               if (queue != null) {
                  queue.Enqueue(str);
                  
                  #if DEBUG
                    int count = (int)releaseCheck[t];
                    releaseCheck[t] = --count;
                  #endif
               } 
           }
            
        }

        private class PropInfo {

            public const int            ReadOnlyUnknown = 0;
            public const int            ReadOnlyTrue =  1;
            public const int            ReadOnlyFalse = 2;

            string               name = null;
            int                  dispid = -1;
            Type                 valueType = null;
            readonly ArrayList   attributes = new ArrayList();
            int                  readOnly = ReadOnlyUnknown;
            bool                 isDefault;
            Object               typeData;
            bool                 nonbrowsable = false;
            int                  index;

            public string Name {
                get { return name; }
                set { name = value; }
            }
            public int DispId {
                get { return dispid; }
                set { dispid = value; }
            }
            public Type ValueType {
                get { return valueType; }
                set { valueType = value; }
            }
            public ArrayList Attributes {
                get { return attributes; }
            }
            public int ReadOnly {
                get { return readOnly; }
                set { readOnly = value; }
            }
            public bool IsDefault {
                get { return isDefault; }
                set { isDefault = value; }
            }
            public object TypeData {
                get { return typeData; }
                set { typeData = value; }
            }
            public bool NonBrowsable {
                get { return nonbrowsable; }
                set { nonbrowsable = value; }
            }
            public int Index{
                get {return index;}
                set {index = value;}
            }


            public override int GetHashCode() {
                if (name != null) {
                    return name.GetHashCode();
                }
                return base.GetHashCode();
            }
        }
    }
    
    
    // just so we can recognize a variant properly...
    /// <include file='doc\COM2TypeInfoProcessor.uex' path='docs/doc[@for="Com2Variant"]/*' />
    //[System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, Name="FullTrust")]
    //[System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Name="FullTrust")]
    public class Com2Variant {
    }
}
