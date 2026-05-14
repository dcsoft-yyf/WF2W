//------------------------------------------------------------------------------
// <copyright file="ToolStripItemCollection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Diagnostics;
    using System.Windows.Forms.Layout;
    using System.Drawing;
    using System.Security;
    using System.Security.Permissions;

    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]
    public class ToolStripItemCollection : ArrangedElementCollection, IList {
        
        private ToolStrip owner;
        private bool itemsCollection;
        private bool isReadOnly = false;
        /// A caching mechanism for key accessor
        /// We use an index here rather than control so that we don't have lifetime
        /// issues by holding on to extra references.
        /// Note this is not Thread Safe - but [....] has to be run in a STA anyways.
        private int lastAccessedIndex = -1;


        internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection) : this(owner, itemsCollection, /*isReadOnly=*/false){
        }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection, bool isReadOnly) {
            this.owner = owner;
            this.itemsCollection = itemsCollection;
            this.isReadOnly = isReadOnly;
        }

        /// <include file='doc\ToolStripItemCollection.uex' path='docs/doc[@for="ToolStripItemCollection.ToolStripItemCollection"]/*' />
        /// <devdoc>
        /// <para>[To be supplied.]</para>
        /// </devdoc>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value) {
            if (owner == null) {
                throw new ArgumentNullException("owner");
            }
           
            this.owner = owner;
            AddRange(value);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public new virtual ToolStripItem this[int index] {
            [System.Runtime.TargetedPatchingOptOutAttribute("Performance critical to inline across NGen image boundaries")]
            get {
                return (ToolStripItem)(InnerList[index]);
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual ToolStripItem this[string key] {
            get {
                // We do not support null and empty string as valid keys.
                if ((key == null) || (key.Length == 0)){
                    return null;
                }

                // Search for the key in our collection
                int index = IndexOfKey(key);
                if (IsValidIndex(index)) {
                    return (ToolStripItem)InnerList[index];
                }
                else {
                    return null;
                }

            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStripItem Add(string text) {
            return Add(text,null,null);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStripItem Add(Image image) {
            return Add(null,image,null);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStripItem Add(string text, Image image) {
            return Add(text,image,null);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStripItem Add(string text, Image image, EventHandler onClick) {
            ToolStripItem item = owner.CreateDefaultItem(text,image,onClick);
            Add(item);
            return item;
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int Add(ToolStripItem value) {
            CheckCanAddOrInsertItem(value);
            
            SetOwner(value);
            int retVal =  InnerList.Add(value);   
            if (itemsCollection &&  owner != null) {
                owner.OnItemAdded(new ToolStripItemEventArgs(value));                
            }           
            return retVal;
            
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddRange(ToolStripItem[] toolStripItems) {        
            if (toolStripItems == null) {
                throw new ArgumentNullException("toolStripItems");
            }
            if (IsReadOnly) {
                throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }

            // ToolStripDropDown will look for PropertyNames.Items to determine if it needs
            // to resize itself.
            using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items)) {
                for (int i = 0; i < toolStripItems.Length; i++) {
                    this.Add(toolStripItems[i]);
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void AddRange(ToolStripItemCollection toolStripItems) {
            if (toolStripItems == null) {
                throw new ArgumentNullException("toolStripItems");
            }
            if (IsReadOnly) {
                throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }

            // ToolStripDropDown will look for PropertyNames.Items to determine if it needs
            // to resize itself.
            using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items)) {
                int currentCount = toolStripItems.Count;
                for (int i = 0; i < currentCount; i++) {
                    this.Add(toolStripItems[i]);
                }
            }
          
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Contains(ToolStripItem value) {
            return InnerList.Contains(value);
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void Clear() {
            if (IsReadOnly) {
               throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }
            if (Count == 0) {
                return;
            }
            ToolStripOverflow overflow = null;

            if (owner != null && !owner.IsDisposingItems) {
                owner.SuspendLayout();
                overflow = owner.GetOverflow();
                if (overflow != null) {
                    overflow.SuspendLayout();
                }
            }
            try {
                while (Count != 0) {
                    RemoveAt(Count - 1);
                }
            }
            finally {
                if (overflow != null) {
                    overflow.ResumeLayout(false);
                }
                if (owner != null && !owner.IsDisposingItems) {
                    owner.ResumeLayout();
                }
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual bool ContainsKey(string key) {
            return IsValidIndex(IndexOfKey(key)); 
        }

        private void CheckCanAddOrInsertItem(ToolStripItem value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            if (IsReadOnly) {
                throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }
            
           
            ToolStripDropDown dropDown = owner as ToolStripDropDown;
            if (dropDown != null) {
                // VSWhidbey 436973: if we're on a dropdown, we can only add non-control host items
                // as we dont want anything on a dropdown to get keyboard messages in the Internet.

                if (dropDown.OwnerItem == value) {
                   throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCircularReference)); 
                }
                
                // VSWhidbey 496526: ScrollButton is the only allowed control host as it correctly eats key messages.
                if (value is ToolStripControlHost && !(value is System.Windows.Forms.ToolStripScrollButton)) {
                    if (dropDown.IsRestrictedWindow) {
                        //IntSecurity.AllWindows.Demand();
                    }
                }
            }
           
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStripItem[] Find(string key, bool searchAllChildren) {            
            if ((key == null) || (key.Length == 0)) {
              throw new System.ArgumentNullException("key", DCSR.GetString(DCSR.FindKeyMayNotBeEmptyOrNull));
            }

            ArrayList foundItems =  FindInternal(key, searchAllChildren, this, new ArrayList());

            // Make this a stongly typed collection.
            ToolStripItem[] stronglyTypedFoundItems = new ToolStripItem[foundItems.Count]; 
            foundItems.CopyTo(stronglyTypedFoundItems, 0);

            return stronglyTypedFoundItems;
        }
      
        /// <devdoc>
        ///     <para>Searches for Items by their Name property, builds up an array list
        ///           of all the items that match. 
        ///     </para>
        /// </devdoc>
        /// <internalonly/> 
        private ArrayList FindInternal(string key, bool searchAllChildren, ToolStripItemCollection itemsToLookIn, ArrayList foundItems)
        {
            if ((itemsToLookIn == null) || (foundItems == null)) {
                return null;  // 
            }

            try
            {
                for (int i = 0; i < itemsToLookIn.Count; i++)
                {
                    if (itemsToLookIn[i] == null)
                    {
                        continue;
                    }

                    if (WindowsFormsUtils.SafeCompareStrings(itemsToLookIn[i].Name, key, /* ignoreCase = */ true))
                    {
                        foundItems.Add(itemsToLookIn[i]);
                    }
                }


                // Optional recurive search for controls in child collections.

                if (searchAllChildren)
                {
                    for (int j = 0; j < itemsToLookIn.Count; j++)
                    {
                        ToolStripDropDownItem item = itemsToLookIn[j] as ToolStripDropDownItem;
                        if (item == null)
                        {
                            continue;
                        }
                        if (item.HasDropDownItems)
                        {
                            // if it has a valid child collecion, append those results to our collection
                            foundItems = FindInternal(key, searchAllChildren, item.DropDownItems, foundItems);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // VSWHIDBEY 80122 make sure we deal with non-critical failures gracefully.
                if (ClientUtils.IsCriticalException(e))
                {
                    throw;
                }
            }
      
            return foundItems;
        }


        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public override bool IsReadOnly { get { return this.isReadOnly; }}

        void IList.Clear() { Clear(); }
        bool IList.IsFixedSize { get { return InnerList.IsFixedSize; }}
        bool IList.Contains(object value) { return InnerList.Contains(value); }
        void IList.RemoveAt(int index) { RemoveAt(index); }
        void IList.Remove(object value) { Remove(value as ToolStripItem); }
        int IList.Add(object value) { return Add(value as ToolStripItem); }
        int IList.IndexOf(object value) { return IndexOf(value as ToolStripItem); }
        void IList.Insert(int index, object value) { Insert(index, value as ToolStripItem);  }

        /// <include file='doc\ToolStripItemCollection.uex' path='docs/doc[@for="ToolStripItemCollection.IList.this"]/*' />
        /// <internalonly/>
        object IList.this[int index] {
            get { return InnerList[index]; }            
            set { throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripCollectionMustInsertAndRemove)); /* InnerList[index] = value; */ }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Insert(int index, ToolStripItem value) {
            CheckCanAddOrInsertItem(value);
            SetOwner(value);
            InnerList.Insert(index, value);
            if (itemsCollection && owner != null) {
                if (owner.IsHandleCreated){
                    LayoutTransaction.DoLayout(owner, value, PropertyNames.Parent);
                }
                else {
                    // next time we fetch the preferred size, recalc it.
                    CommonProperties.xClearPreferredSizeCache(owner);
                }
                owner.OnItemAdded(new ToolStripItemEventArgs(value));                  
            }           

        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int IndexOf(ToolStripItem value) {
            return InnerList.IndexOf(value);
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual int IndexOfKey(String key) {
            // Step 0 - Arg validation
            if ((key == null) || (key.Length == 0)){
                return -1; // we dont support empty or null keys.
            }

            // step 1 - check the last cached item
            if (IsValidIndex(lastAccessedIndex)) {
                if (WindowsFormsUtils.SafeCompareStrings(this[lastAccessedIndex].Name, key, /* ignoreCase = */ true)) {
                    return lastAccessedIndex;
                }
            }

            // step 2 - search for the item
            for (int i = 0; i < this.Count; i ++) {
                if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, /* ignoreCase = */ true)) {
                    lastAccessedIndex = i;
                    return i;
                }
            }

            // step 3 - we didn't find it.  Invalidate the last accessed index and return -1.
            lastAccessedIndex = -1;
            return -1;
        }

        /// <devdoc>
        ///     <para>Determines if the index is valid for the collection.</para>
        /// </devdoc>
        /// <internalonly/> 
        private bool IsValidIndex(int index) {
            return ((index >= 0) && (index < this.Count));
        }

        /// <devdoc>
        ///  Do proper cleanup of ownership, etc.
        /// </devdoc>
        private void OnAfterRemove(ToolStripItem item) {
            if (itemsCollection) {
                ToolStrip parent = null; 
                if (item != null) {
                    parent = item.ParentInternal;
                    item.SetOwner(null);
                }

                if (owner != null) {
                    if (!owner.IsDisposingItems) {
                        ToolStripItemEventArgs e = new ToolStripItemEventArgs(item);
                        owner.OnItemRemoved(e);

                        // VSWhidbey 505129
                        // dont fire the ItemRemoved event for Overflow
                        // it would fire constantly.... instead clear any state if the item
                        // is really being removed from the master collection.
                        if (parent != null && parent != owner) {
                            parent.OnItemVisibleChanged(e, /*performLayout*/false);
                        }
                    }
                }
                
            }
        }
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void Remove(ToolStripItem value) {
            if (IsReadOnly) {
               throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }
            InnerList.Remove(value);
            OnAfterRemove(value);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void RemoveAt(int index) {
            if (IsReadOnly) {
               throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }
            ToolStripItem item = null;
            if (index < Count && index >= 0)  {
                item = (ToolStripItem)(InnerList[index]);
            }
            InnerList.RemoveAt(index);
            OnAfterRemove(item);
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public virtual void RemoveByKey(string key) {
            if (IsReadOnly) {
               throw new NotSupportedException(DCSR.GetString(DCSR.ToolStripItemCollectionIsReadOnly));
            }
            int index = IndexOfKey(key);
            if (IsValidIndex(index)) {
                RemoveAt(index); 
            }
        }

        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public void CopyTo(ToolStripItem[] array, int index) {
            InnerList.CopyTo(array, index);
        }
            
        // 
        internal void MoveItem(ToolStripItem value) {
            if (value.ParentInternal != null) {
                int indexOfItem = value.ParentInternal.Items.IndexOf(value);
                if (indexOfItem >= 0) {
                    value.ParentInternal.Items.RemoveAt(indexOfItem);
                }
            }
            Add(value);

        }

        internal void MoveItem(int index, ToolStripItem value) {
           
            // if moving to the end - call add instead.
            if (index == this.Count) {
                MoveItem(value);
                return;
            }
			
            if (value.ParentInternal != null) {
                int indexOfItem = value.ParentInternal.Items.IndexOf(value);
				
                if (indexOfItem >= 0) {
                    value.ParentInternal.Items.RemoveAt(indexOfItem);
				
                    if ((value.ParentInternal == owner) && (index > indexOfItem)) {
                        index--;
                    }
                }
            }
            Insert(index,value);

        }

        private void SetOwner(ToolStripItem item) {

            if (itemsCollection) {
              
                if (item != null) {
                    if (item.Owner != null) {
                        item.Owner.Items.Remove(item);
                    }

                    item.SetOwner(owner);
                    if (item.Renderer != null) {
                        item.Renderer.InitializeItem(item);
                    }
                }
            }
        }
        
    }
    

}

