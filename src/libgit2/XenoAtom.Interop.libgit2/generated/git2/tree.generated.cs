//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
namespace XenoAtom.Interop
{
    using System.Runtime.InteropServices;
    
    using System.Runtime.CompilerServices;
    
    public static unsafe partial class libgit2
    {
        /// <summary>
        /// Tree traversal modes
        /// </summary>
        public enum git_treewalk_mode : uint
        {
            /// <summary>
            /// Pre-order
            /// </summary>
            GIT_TREEWALK_PRE = unchecked((uint)0),
            
            /// <summary>
            /// Post-order
            /// </summary>
            GIT_TREEWALK_POST = unchecked((uint)1),
        }
        
        /// <summary>
        /// Pre-order
        /// </summary>
        public const libgit2.git_treewalk_mode GIT_TREEWALK_PRE = git_treewalk_mode.GIT_TREEWALK_PRE;
        
        /// <summary>
        /// Post-order
        /// </summary>
        public const libgit2.git_treewalk_mode GIT_TREEWALK_POST = git_treewalk_mode.GIT_TREEWALK_POST;
        
        /// <summary>
        /// The kind of update to perform
        /// </summary>
        public enum git_tree_update_t : uint
        {
            /// <summary>
            /// Update or insert an entry at the specified path
            /// </summary>
            GIT_TREE_UPDATE_UPSERT = unchecked((uint)0),
            
            /// <summary>
            /// Remove an entry from the specified path
            /// </summary>
            GIT_TREE_UPDATE_REMOVE = unchecked((uint)1),
        }
        
        /// <summary>
        /// Update or insert an entry at the specified path
        /// </summary>
        public const libgit2.git_tree_update_t GIT_TREE_UPDATE_UPSERT = git_tree_update_t.GIT_TREE_UPDATE_UPSERT;
        
        /// <summary>
        /// Remove an entry from the specified path
        /// </summary>
        public const libgit2.git_tree_update_t GIT_TREE_UPDATE_REMOVE = git_tree_update_t.GIT_TREE_UPDATE_REMOVE;
        
        /// <summary>
        /// An action to perform during the update of a tree
        /// </summary>
        public partial struct git_tree_update
        {
            /// <summary>
            /// Update action. If it's an removal, only the path is looked at
            /// </summary>
            public libgit2.git_tree_update_t action;
            
            /// <summary>
            /// The entry's id
            /// </summary>
            public libgit2.git_oid id;
            
            /// <summary>
            /// The filemode/kind of object
            /// </summary>
            public libgit2.git_filemode_t filemode;
            
            /// <summary>
            /// The full path from the root tree
            /// </summary>
            public byte* path;
        }
        
        /// <summary>
        /// Callback for git_treebuilder_filter
        /// </summary>
        /// <remarks>
        /// The return value is treated as a boolean, with zero indicating that the
        /// entry should be left alone and any non-zero value meaning that the
        /// entry should be removed from the treebuilder list (i.e. filtered out).
        /// </remarks>
        public readonly partial struct git_treebuilder_filter_cb : IEquatable<libgit2.git_treebuilder_filter_cb>
        {
            public git_treebuilder_filter_cb(delegate*unmanaged[Cdecl]<libgit2.git_tree_entry, void*, int> value) => this.Value = value;
            
            public delegate*unmanaged[Cdecl]<libgit2.git_tree_entry, void*, int> Value { get; }
            
            public override bool Equals(object obj) => obj is git_treebuilder_filter_cb other && Equals(other);
            
            public bool Equals(git_treebuilder_filter_cb other) => Value == other.Value;
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator delegate*unmanaged[Cdecl]<libgit2.git_tree_entry, void*, int> (libgit2.git_treebuilder_filter_cb from) => from.Value;
            
            public static implicit operator libgit2.git_treebuilder_filter_cb (delegate*unmanaged[Cdecl]<libgit2.git_tree_entry, void*, int> from) => new libgit2.git_treebuilder_filter_cb(from);
            
            public static bool operator ==(git_treebuilder_filter_cb left, git_treebuilder_filter_cb right) => left.Equals(right);
            
            public static bool operator !=(git_treebuilder_filter_cb left, git_treebuilder_filter_cb right) => !left.Equals(right);
        }
        
        /// <summary>
        /// Callback for the tree traversal method
        /// </summary>
        public readonly partial struct git_treewalk_cb : IEquatable<libgit2.git_treewalk_cb>
        {
            public git_treewalk_cb(delegate*unmanaged[Cdecl]<byte*, libgit2.git_tree_entry, void*, int> value) => this.Value = value;
            
            public delegate*unmanaged[Cdecl]<byte*, libgit2.git_tree_entry, void*, int> Value { get; }
            
            public override bool Equals(object obj) => obj is git_treewalk_cb other && Equals(other);
            
            public bool Equals(git_treewalk_cb other) => Value == other.Value;
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator delegate*unmanaged[Cdecl]<byte*, libgit2.git_tree_entry, void*, int> (libgit2.git_treewalk_cb from) => from.Value;
            
            public static implicit operator libgit2.git_treewalk_cb (delegate*unmanaged[Cdecl]<byte*, libgit2.git_tree_entry, void*, int> from) => new libgit2.git_treewalk_cb(from);
            
            public static bool operator ==(git_treewalk_cb left, git_treewalk_cb right) => left.Equals(right);
            
            public static bool operator !=(git_treewalk_cb left, git_treewalk_cb right) => !left.Equals(right);
        }
        
        /// <summary>
        /// Lookup a tree object from the repository.
        /// </summary>
        /// <param name="out">Pointer to the looked up tree</param>
        /// <param name="repo">The repo to use when locating the tree.</param>
        /// <param name="id">Identity of the tree to locate.</param>
        /// <returns>0 or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_lookup")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_lookup(out libgit2.git_tree @out, libgit2.git_repository repo, in libgit2.git_oid id);
        
        /// <summary>
        /// Lookup a tree object from the repository,
        /// given a prefix of its identifier (short id).
        /// </summary>
        /// <param name="out">pointer to the looked up tree</param>
        /// <param name="repo">the repo to use when locating the tree.</param>
        /// <param name="id">identity of the tree to locate.</param>
        /// <param name="len">the length of the short identifier</param>
        /// <returns>0 or an error code</returns>
        /// <seealso cref="git_object_lookup_prefix"/>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_lookup_prefix")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_lookup_prefix(out libgit2.git_tree @out, libgit2.git_repository repo, in libgit2.git_oid id, nuint len);
        
        /// <summary>
        /// Close an open tree
        /// </summary>
        /// <param name="tree">The tree to close</param>
        /// <remarks>
        /// You can no longer use the git_tree pointer after this call.IMPORTANT: You MUST call this method when you stop using a tree to
        /// release memory. Failure to do so will cause a memory leak.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_tree_free(libgit2.git_tree tree);
        
        /// <summary>
        /// Get the id of a tree.
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <returns>object identity for the tree.</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_id")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_oid* git_tree_id(libgit2.git_tree tree);
        
        /// <summary>
        /// Get the repository that contains the tree.
        /// </summary>
        /// <param name="tree">A previously loaded tree.</param>
        /// <returns>Repository that contains this tree.</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_owner")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_repository git_tree_owner(libgit2.git_tree tree);
        
        /// <summary>
        /// Get the number of entries listed in a tree
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <returns>the number of entries in the tree</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entrycount")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint git_tree_entrycount(libgit2.git_tree tree);
        
        /// <summary>
        /// Lookup a tree entry by its filename
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <param name="filename">the filename of the desired entry</param>
        /// <returns>the tree entry; NULL if not found</returns>
        /// <remarks>
        /// This returns a git_tree_entry that is owned by the git_tree.  You don't
        /// have to free it, but you must not use it after the git_tree is released.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_byname")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_tree_entry_byname(libgit2.git_tree tree, byte* filename);
        
        /// <summary>
        /// Lookup a tree entry by its filename
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <param name="filename">the filename of the desired entry</param>
        /// <returns>the tree entry; NULL if not found</returns>
        /// <remarks>
        /// This returns a git_tree_entry that is owned by the git_tree.  You don't
        /// have to free it, but you must not use it after the git_tree is released.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_byname")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_tree_entry_byname(libgit2.git_tree tree, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> filename);
        
        /// <summary>
        /// Lookup a tree entry by its position in the tree
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <param name="idx">the position in the entry list</param>
        /// <returns>the tree entry; NULL if not found</returns>
        /// <remarks>
        /// This returns a git_tree_entry that is owned by the git_tree.  You don't
        /// have to free it, but you must not use it after the git_tree is released.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_byindex")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_tree_entry_byindex(libgit2.git_tree tree, nuint idx);
        
        /// <summary>
        /// Lookup a tree entry by SHA value.
        /// </summary>
        /// <param name="tree">a previously loaded tree.</param>
        /// <param name="id">the sha being looked for</param>
        /// <returns>the tree entry; NULL if not found</returns>
        /// <remarks>
        /// This returns a git_tree_entry that is owned by the git_tree.  You don't
        /// have to free it, but you must not use it after the git_tree is released.Warning: this must examine every entry in the tree, so it is not fast.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_byid")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_tree_entry_byid(libgit2.git_tree tree, in libgit2.git_oid id);
        
        /// <summary>
        /// Retrieve a tree entry contained in a tree or in any of its subtrees,
        /// given its relative path.
        /// </summary>
        /// <param name="out">Pointer where to store the tree entry</param>
        /// <param name="root">Previously loaded tree which is the root of the relative path</param>
        /// <param name="path">Path to the contained entry</param>
        /// <returns>0 on success; GIT_ENOTFOUND if the path does not exist</returns>
        /// <remarks>
        /// Unlike the other lookup functions, the returned tree entry is owned by
        /// the user and must be freed explicitly with `git_tree_entry_free()`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_bypath")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_tree_entry_bypath(out libgit2.git_tree_entry @out, libgit2.git_tree root, byte* path);
        
        /// <summary>
        /// Retrieve a tree entry contained in a tree or in any of its subtrees,
        /// given its relative path.
        /// </summary>
        /// <param name="out">Pointer where to store the tree entry</param>
        /// <param name="root">Previously loaded tree which is the root of the relative path</param>
        /// <param name="path">Path to the contained entry</param>
        /// <returns>0 on success; GIT_ENOTFOUND if the path does not exist</returns>
        /// <remarks>
        /// Unlike the other lookup functions, the returned tree entry is owned by
        /// the user and must be freed explicitly with `git_tree_entry_free()`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_bypath")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_tree_entry_bypath(out libgit2.git_tree_entry @out, libgit2.git_tree root, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> path);
        
        /// <summary>
        /// Duplicate a tree entry
        /// </summary>
        /// <param name="dest">pointer where to store the copy</param>
        /// <param name="source">tree entry to duplicate</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// Create a copy of a tree entry. The returned copy is owned by the user,
        /// and must be freed explicitly with `git_tree_entry_free()`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_dup")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_entry_dup(out libgit2.git_tree_entry dest, libgit2.git_tree_entry source);
        
        /// <summary>
        /// Free a user-owned tree entry
        /// </summary>
        /// <param name="entry">The entry to free</param>
        /// <remarks>
        /// IMPORTANT: This function is only needed for tree entries owned by the
        /// user, such as the ones returned by `git_tree_entry_dup()` or
        /// `git_tree_entry_bypath()`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_tree_entry_free(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the filename of a tree entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>the name of the file</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_name")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* git_tree_entry_name_(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the filename of a tree entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>the name of the file</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_name")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string git_tree_entry_name(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the id of the object pointed by the entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>the oid of the object</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_id")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_oid* git_tree_entry_id(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the type of the object pointed by the entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>the type of the pointed object</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_type")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_object_t git_tree_entry_type(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the UNIX file attributes of a tree entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>filemode as an integer</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_filemode")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_filemode_t git_tree_entry_filemode(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Get the raw UNIX file attributes of a tree entry
        /// </summary>
        /// <param name="entry">a tree entry</param>
        /// <returns>filemode as an integer</returns>
        /// <remarks>
        /// This function does not perform any normalization and is only useful
        /// if you need to be able to recreate the original tree object.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_filemode_raw")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_filemode_t git_tree_entry_filemode_raw(libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Compare two tree entries
        /// </summary>
        /// <param name="e1">first tree entry</param>
        /// <param name="e2">second tree entry</param>
        /// <returns>@return &lt;
        /// 0 if e1 is before e2, 0 if e1 == e2, &gt;0 if e1 is after e2</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_cmp")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_tree_entry_cmp(libgit2.git_tree_entry e1, libgit2.git_tree_entry e2);
        
        /// <summary>
        /// Convert a tree entry to the git_object it points to.
        /// </summary>
        /// <param name="object_out">pointer to the converted object</param>
        /// <param name="repo">repository where to lookup the pointed object</param>
        /// <param name="entry">a tree entry</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// You must call `git_object_free()` on the object when you are done with it.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_entry_to_object")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_entry_to_object(out libgit2.git_object object_out, libgit2.git_repository repo, libgit2.git_tree_entry entry);
        
        /// <summary>
        /// Create a new tree builder.
        /// </summary>
        /// <param name="out">Pointer where to store the tree builder</param>
        /// <param name="repo">Repository in which to store the object</param>
        /// <param name="source">Source tree to initialize the builder (optional)</param>
        /// <returns>0 on success; error code otherwise</returns>
        /// <remarks>
        /// The tree builder can be used to create or modify trees in memory and
        /// write them as tree objects to the database.If the `source` parameter is not NULL, the tree builder will be
        /// initialized with the entries of the given tree.If the `source` parameter is NULL, the tree builder will start with no
        /// entries and will have to be filled manually.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_new")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_new(out libgit2.git_treebuilder @out, libgit2.git_repository repo, libgit2.git_tree source);
        
        /// <summary>
        /// Clear all the entries in the builder
        /// </summary>
        /// <param name="bld">Builder to clear</param>
        /// <returns>0 on success; error code otherwise</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_clear")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_clear(libgit2.git_treebuilder bld);
        
        /// <summary>
        /// Get the number of entries listed in a treebuilder
        /// </summary>
        /// <param name="bld">a previously loaded treebuilder.</param>
        /// <returns>the number of entries in the treebuilder</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_entrycount")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint git_treebuilder_entrycount(libgit2.git_treebuilder bld);
        
        /// <summary>
        /// Free a tree builder
        /// </summary>
        /// <param name="bld">Builder to free</param>
        /// <remarks>
        /// This will clear all the entries and free to builder.
        /// Failing to free the builder after you're done using it
        /// will result in a memory leak
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_treebuilder_free(libgit2.git_treebuilder bld);
        
        /// <summary>
        /// Get an entry from the builder from its filename
        /// </summary>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Name of the entry</param>
        /// <returns>pointer to the entry; NULL if not found</returns>
        /// <remarks>
        /// The returned entry is owned by the builder and should
        /// not be freed manually.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_get")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_treebuilder_get(libgit2.git_treebuilder bld, byte* filename);
        
        /// <summary>
        /// Get an entry from the builder from its filename
        /// </summary>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Name of the entry</param>
        /// <returns>pointer to the entry; NULL if not found</returns>
        /// <remarks>
        /// The returned entry is owned by the builder and should
        /// not be freed manually.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_get")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_tree_entry git_treebuilder_get(libgit2.git_treebuilder bld, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> filename);
        
        /// <summary>
        /// Add or update an entry to the builder
        /// </summary>
        /// <param name="out">Pointer to store the entry (optional)</param>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Filename of the entry</param>
        /// <param name="id">SHA1 oid of the entry</param>
        /// <param name="filemode">Folder attributes of the entry. This parameter must
        /// be valued with one of the following entries: 0040000, 0100644,
        /// 0100755, 0120000 or 0160000.</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// Insert a new entry for `filename` in the builder with the
        /// given attributes.If an entry named `filename` already exists, its attributes
        /// will be updated with the given ones.The optional pointer `out` can be used to retrieve a pointer to the
        /// newly created/updated entry.  Pass NULL if you do not need it. The
        /// pointer may not be valid past the next operation in this
        /// builder. Duplicate the entry if you want to keep it.By default the entry that you are inserting will be checked for
        /// validity; that it exists in the object database and is of the
        /// correct type.  If you do not want this behavior, set the
        /// `GIT_OPT_ENABLE_STRICT_OBJECT_CREATION` library option to false.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_insert")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_insert(out libgit2.git_tree_entry @out, libgit2.git_treebuilder bld, byte* filename, in libgit2.git_oid id, libgit2.git_filemode_t filemode);
        
        /// <summary>
        /// Add or update an entry to the builder
        /// </summary>
        /// <param name="out">Pointer to store the entry (optional)</param>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Filename of the entry</param>
        /// <param name="id">SHA1 oid of the entry</param>
        /// <param name="filemode">Folder attributes of the entry. This parameter must
        /// be valued with one of the following entries: 0040000, 0100644,
        /// 0100755, 0120000 or 0160000.</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// Insert a new entry for `filename` in the builder with the
        /// given attributes.If an entry named `filename` already exists, its attributes
        /// will be updated with the given ones.The optional pointer `out` can be used to retrieve a pointer to the
        /// newly created/updated entry.  Pass NULL if you do not need it. The
        /// pointer may not be valid past the next operation in this
        /// builder. Duplicate the entry if you want to keep it.By default the entry that you are inserting will be checked for
        /// validity; that it exists in the object database and is of the
        /// correct type.  If you do not want this behavior, set the
        /// `GIT_OPT_ENABLE_STRICT_OBJECT_CREATION` library option to false.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_insert")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_insert(out libgit2.git_tree_entry @out, libgit2.git_treebuilder bld, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> filename, in libgit2.git_oid id, libgit2.git_filemode_t filemode);
        
        /// <summary>
        /// Remove an entry from the builder by its filename
        /// </summary>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Filename of the entry to remove</param>
        /// <returns>0 or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_remove")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_remove(libgit2.git_treebuilder bld, byte* filename);
        
        /// <summary>
        /// Remove an entry from the builder by its filename
        /// </summary>
        /// <param name="bld">Tree builder</param>
        /// <param name="filename">Filename of the entry to remove</param>
        /// <returns>0 or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_remove")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_remove(libgit2.git_treebuilder bld, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> filename);
        
        /// <summary>
        /// Selectively remove entries in the tree
        /// </summary>
        /// <param name="bld">Tree builder</param>
        /// <param name="filter">Callback to filter entries</param>
        /// <param name="payload">Extra data to pass to filter callback</param>
        /// <returns>0 on success, non-zero callback return value, or error code</returns>
        /// <remarks>
        /// The `filter` callback will be called for each entry in the tree with a
        /// pointer to the entry and the provided `payload`; if the callback returns
        /// non-zero, the entry will be filtered (removed from the builder).
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_filter")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_filter(libgit2.git_treebuilder bld, libgit2.git_treebuilder_filter_cb filter, void* payload);
        
        /// <summary>
        /// Write the contents of the tree builder as a tree object
        /// </summary>
        /// <param name="id">Pointer to store the OID of the newly written tree</param>
        /// <param name="bld">Tree builder to write</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// The tree builder will be written to the given `repo`, and its
        /// identifying SHA1 hash will be stored in the `id` pointer.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_treebuilder_write")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_treebuilder_write(out libgit2.git_oid id, libgit2.git_treebuilder bld);
        
        /// <summary>
        /// Traverse the entries in a tree and its subtrees in post or pre order.
        /// </summary>
        /// <param name="tree">The tree to walk</param>
        /// <param name="mode">Traversal mode (pre or post-order)</param>
        /// <param name="callback">Function to call on each tree entry</param>
        /// <param name="payload">Opaque pointer to be passed on each callback</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// The entries will be traversed in the specified order, children subtrees
        /// will be automatically loaded as required, and the `callback` will be
        /// called once per entry with the current (relative) root for the entry and
        /// the entry data itself.If the callback returns a positive value, the passed entry will be
        /// skipped on the traversal (in pre mode). A negative value stops the walk.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_walk")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_walk(libgit2.git_tree tree, libgit2.git_treewalk_mode mode, libgit2.git_treewalk_cb callback, void* payload);
        
        /// <summary>
        /// Create an in-memory copy of a tree. The copy must be explicitly
        /// free'd or it will leak.
        /// </summary>
        /// <param name="out">Pointer to store the copy of the tree</param>
        /// <param name="source">Original tree to copy</param>
        /// <returns>0</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_dup")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_tree_dup(out libgit2.git_tree @out, libgit2.git_tree source);
        
        /// <summary>
        /// Create a tree based on another one with the specified modifications
        /// </summary>
        /// <param name="out">id of the new tree</param>
        /// <param name="repo">the repository in which to create the tree, must be the
        /// same as for `baseline`</param>
        /// <param name="baseline">the tree to base these changes on</param>
        /// <param name="nupdates">the number of elements in the update list</param>
        /// <param name="updates">the list of updates to perform</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// Given the `baseline` perform the changes described in the list of
        /// `updates` and create a new tree.This function is optimized for common file/directory addition, removal and
        /// replacement in trees. It is much more efficient than reading the tree into a
        /// `git_index` and modifying that, but in exchange it is not as flexible.Deleting and adding the same entry is undefined behaviour, changing
        /// a tree to a blob or viceversa is not supported.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_tree_create_updated")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_tree_create_updated(out libgit2.git_oid @out, libgit2.git_repository repo, libgit2.git_tree baseline, nuint nupdates, libgit2.git_tree_update* updates);
    }
}
