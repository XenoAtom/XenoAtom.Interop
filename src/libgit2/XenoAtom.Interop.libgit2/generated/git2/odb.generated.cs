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
        /// Flags controlling the behavior of ODB lookup operations
        /// </summary>
        [Flags]
        public enum git_odb_lookup_flags_t : uint
        {
            /// <summary>
            /// Don't call `git_odb_refresh` if the lookup fails. Useful when doing
            /// a batch of lookup operations for objects that may legitimately not
            /// exist. When using this flag, you may wish to manually call
            /// `git_odb_refresh` before processing a batch of objects.
            /// </summary>
            GIT_ODB_LOOKUP_NO_REFRESH = unchecked((uint)1),
        }
        
        /// <summary>
        /// Don't call `git_odb_refresh` if the lookup fails. Useful when doing
        /// a batch of lookup operations for objects that may legitimately not
        /// exist. When using this flag, you may wish to manually call
        /// `git_odb_refresh` before processing a batch of objects.
        /// </summary>
        public const libgit2.git_odb_lookup_flags_t GIT_ODB_LOOKUP_NO_REFRESH = git_odb_lookup_flags_t.GIT_ODB_LOOKUP_NO_REFRESH;
        
        /// <summary>
        /// Options for configuring a loose object backend.
        /// </summary>
        public partial struct git_odb_options
        {
            /// <summary>
            /// version for the struct
            /// </summary>
            public uint version;
            
            /// <summary>
            /// Type of object IDs to use for this object database, or
            /// 0 for default (currently SHA1).
            /// </summary>
            public libgit2.git_oid_t oid_type;
        }
        
        /// <summary>
        /// The information about object IDs to query in `git_odb_expand_ids`,
        /// which will be populated upon return.
        /// </summary>
        public partial struct git_odb_expand_id
        {
            /// <summary>
            /// The object ID to expand
            /// </summary>
            public libgit2.git_oid id;
            
            /// <summary>
            /// The length of the object ID (in nibbles, or packets of 4 bits; the
            /// number of hex characters)
            /// </summary>
            public ushort length;
            
            /// <summary>
            /// The (optional) type of the object to search for; leave as `0` or set
            /// to `GIT_OBJECT_ANY` to query for any object matching the ID.
            /// </summary>
            public libgit2.git_object_t type;
        }
        
        /// <summary>
        /// Function type for callbacks from git_odb_foreach.
        /// </summary>
        public readonly partial struct git_odb_foreach_cb : IEquatable<libgit2.git_odb_foreach_cb>
        {
            public git_odb_foreach_cb(delegate*unmanaged[Cdecl]<libgit2.git_oid*, void*, int> value) => this.Value = value;
            
            public delegate*unmanaged[Cdecl]<libgit2.git_oid*, void*, int> Value { get; }
            
            public override bool Equals(object obj) => obj is git_odb_foreach_cb other && Equals(other);
            
            public bool Equals(git_odb_foreach_cb other) => Value == other.Value;
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator delegate*unmanaged[Cdecl]<libgit2.git_oid*, void*, int> (libgit2.git_odb_foreach_cb from) => from.Value;
            
            public static implicit operator libgit2.git_odb_foreach_cb (delegate*unmanaged[Cdecl]<libgit2.git_oid*, void*, int> from) => new libgit2.git_odb_foreach_cb(from);
            
            public static bool operator ==(git_odb_foreach_cb left, git_odb_foreach_cb right) => left.Equals(right);
            
            public static bool operator !=(git_odb_foreach_cb left, git_odb_foreach_cb right) => !left.Equals(right);
        }
        
        public const uint GIT_ODB_OPTIONS_VERSION = 1;
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_new")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_new(out libgit2.git_odb @out);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_open")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_open(out libgit2.git_odb @out, byte* objects_dir);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_open")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_open(out libgit2.git_odb @out, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> objects_dir);
        
        /// <summary>
        /// Add an on-disk alternate to an existing Object DB.
        /// </summary>
        /// <param name="odb">database to add the backend to</param>
        /// <param name="path">path to the objects folder for the alternate</param>
        /// <returns>0 on success, error code otherwise</returns>
        /// <remarks>
        /// Note that the added path must point to an `objects`, not
        /// to a full repository, to use it as an alternate store.Alternate backends are always checked for objects *after*
        /// all the main backends have been exhausted.Writing is disabled on alternate backends.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_add_disk_alternate")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_add_disk_alternate(libgit2.git_odb odb, byte* path);
        
        /// <summary>
        /// Add an on-disk alternate to an existing Object DB.
        /// </summary>
        /// <param name="odb">database to add the backend to</param>
        /// <param name="path">path to the objects folder for the alternate</param>
        /// <returns>0 on success, error code otherwise</returns>
        /// <remarks>
        /// Note that the added path must point to an `objects`, not
        /// to a full repository, to use it as an alternate store.Alternate backends are always checked for objects *after*
        /// all the main backends have been exhausted.Writing is disabled on alternate backends.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_add_disk_alternate")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_add_disk_alternate(libgit2.git_odb odb, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> path);
        
        /// <summary>
        /// Close an open object database.
        /// </summary>
        /// <param name="db">database pointer to close. If NULL no action is taken.</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_odb_free(libgit2.git_odb db);
        
        /// <summary>
        /// Read an object from the database.
        /// </summary>
        /// <param name="out">pointer where to store the read object</param>
        /// <param name="db">database to search for the object in.</param>
        /// <param name="id">identity of the object to read.</param>
        /// <returns>@return 0 if the object was read, GIT_ENOTFOUND if the object is
        /// not in the database.</returns>
        /// <remarks>
        /// This method queries all available ODB backends
        /// trying to read the given OID.The returned object is reference counted and
        /// internally cached, so it should be closed
        /// by the user once it's no longer in use.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_read")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_read(out libgit2.git_odb_object @out, libgit2.git_odb db, in libgit2.git_oid id);
        
        /// <summary>
        /// Read an object from the database, given a prefix
        /// of its identifier.
        /// </summary>
        /// <param name="out">pointer where to store the read object</param>
        /// <param name="db">database to search for the object in.</param>
        /// <param name="short_id">a prefix of the id of the object to read.</param>
        /// <param name="len">the length of the prefix</param>
        /// <returns>@return 0 if the object was read, GIT_ENOTFOUND if the object is not in the
        /// database. GIT_EAMBIGUOUS if the prefix is ambiguous
        /// (several objects match the prefix)</returns>
        /// <remarks>
        /// This method queries all available ODB backends
        /// trying to match the 'len' first hexadecimal
        /// characters of the 'short_id'.
        /// The remaining (GIT_OID_SHA1_HEXSIZE-len)*4 bits of
        /// 'short_id' must be 0s.
        /// 'len' must be at least GIT_OID_MINPREFIXLEN,
        /// and the prefix must be long enough to identify
        /// a unique object in all the backends; the
        /// method will fail otherwise.The returned object is reference counted and
        /// internally cached, so it should be closed
        /// by the user once it's no longer in use.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_read_prefix")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_read_prefix(out libgit2.git_odb_object @out, libgit2.git_odb db, in libgit2.git_oid short_id, nuint len);
        
        /// <summary>
        /// Read the header of an object from the database, without
        /// reading its full contents.
        /// </summary>
        /// <param name="len_out">pointer where to store the length</param>
        /// <param name="type_out">pointer where to store the type</param>
        /// <param name="db">database to search for the object in.</param>
        /// <param name="id">identity of the object to read.</param>
        /// <returns>@return 0 if the object was read, GIT_ENOTFOUND if the object is not
        /// in the database.</returns>
        /// <remarks>
        /// The header includes the length and the type of an object.Note that most backends do not support reading only the header
        /// of an object, so the whole object will be read and then the
        /// header will be returned.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_read_header")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_read_header(out nuint len_out, out libgit2.git_object_t type_out, libgit2.git_odb db, in libgit2.git_oid id);
        
        /// <summary>
        /// Determine if the given object can be found in the object database.
        /// </summary>
        /// <param name="db">database to be searched for the given object.</param>
        /// <param name="id">the object to search for.</param>
        /// <returns>1 if the object was found, 0 otherwise</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_exists")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_exists(libgit2.git_odb db, in libgit2.git_oid id);
        
        /// <summary>
        /// Determine if the given object can be found in the object database, with
        /// extended options.
        /// </summary>
        /// <param name="db">database to be searched for the given object.</param>
        /// <param name="id">the object to search for.</param>
        /// <param name="flags">flags affecting the lookup (see `git_odb_lookup_flags_t`)</param>
        /// <returns>1 if the object was found, 0 otherwise</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_exists_ext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_exists_ext(libgit2.git_odb db, in libgit2.git_oid id, libgit2.git_odb_lookup_flags_t flags);
        
        /// <summary>
        /// Determine if an object can be found in the object database by an
        /// abbreviated object ID.
        /// </summary>
        /// <param name="out">The full OID of the found object if just one is found.</param>
        /// <param name="db">The database to be searched for the given object.</param>
        /// <param name="short_id">A prefix of the id of the object to read.</param>
        /// <param name="len">The length of the prefix.</param>
        /// <returns>@return 0 if found, GIT_ENOTFOUND if not found, GIT_EAMBIGUOUS if multiple
        /// matches were found, other value 
        /// &lt;
        /// 0 if there was a read error.</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_exists_prefix")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_exists_prefix(out libgit2.git_oid @out, libgit2.git_odb db, in libgit2.git_oid short_id, nuint len);
        
        /// <summary>
        /// Determine if one or more objects can be found in the object database
        /// by their abbreviated object ID and type.
        /// </summary>
        /// <param name="db">The database to be searched for the given objects.</param>
        /// <param name="ids">An array of short object IDs to search for</param>
        /// <param name="count">The length of the `ids` array</param>
        /// <returns>0 on success or an error code on failure</returns>
        /// <remarks>
        /// The given array will be updated in place: for each abbreviated ID that is
        /// unique in the database, and of the given type (if specified),
        /// the full object ID, object ID length (`GIT_OID_SHA1_HEXSIZE`) and type will be
        /// written back to the array. For IDs that are not found (or are ambiguous),
        /// the array entry will be zeroed.Note that since this function operates on multiple objects, the
        /// underlying database will not be asked to be reloaded if an object is
        /// not found (which is unlike other object database operations.)
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_expand_ids")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_expand_ids(libgit2.git_odb db, libgit2.git_odb_expand_id* ids, nuint count);
        
        /// <summary>
        /// Refresh the object database to load newly added files.
        /// </summary>
        /// <param name="db">database to refresh</param>
        /// <returns>0 on success, error code otherwise</returns>
        /// <remarks>
        /// If the object databases have changed on disk while the library
        /// is running, this function will force a reload of the underlying
        /// indexes.Use this function when you're confident that an external
        /// application has tampered with the ODB.NOTE that it is not necessary to call this function at all. The
        /// library will automatically attempt to refresh the ODB
        /// when a lookup fails, to see if the looked up object exists
        /// on disk but hasn't been loaded yet.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_refresh")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_refresh(libgit2.git_odb db);
        
        /// <summary>
        /// List all objects available in the database
        /// </summary>
        /// <param name="db">database to use</param>
        /// <param name="cb">the callback to call for each object</param>
        /// <param name="payload">data to pass to the callback</param>
        /// <returns>0 on success, non-zero callback return value, or error code</returns>
        /// <remarks>
        /// The callback will be called for each object available in the
        /// database. Note that the objects are likely to be returned in the index
        /// order, which would make accessing the objects in that order inefficient.
        /// Return a non-zero value from the callback to stop looping.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_foreach")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_foreach(libgit2.git_odb db, libgit2.git_odb_foreach_cb cb, void* payload);
        
        /// <summary>
        /// Write an object directly into the ODB
        /// </summary>
        /// <param name="out">pointer to store the OID result of the write</param>
        /// <param name="odb">object database where to store the object</param>
        /// <param name="data">buffer with the data to store</param>
        /// <param name="len">size of the buffer</param>
        /// <param name="type">type of the data to store</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// This method writes a full object straight into the ODB.
        /// For most cases, it is preferred to write objects through a write
        /// stream, which is both faster and less memory intensive, specially
        /// for big objects.This method is provided for compatibility with custom backends
        /// which are not able to support streaming writes
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_write")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_write(out libgit2.git_oid @out, libgit2.git_odb odb, void* data, nuint len, libgit2.git_object_t type);
        
        /// <summary>
        /// Open a stream to write an object into the ODB
        /// </summary>
        /// <param name="out">pointer where to store the stream</param>
        /// <param name="db">object database where the stream will write</param>
        /// <param name="size">final size of the object that will be written</param>
        /// <param name="type">type of the object that will be written</param>
        /// <returns>0 if the stream was created; error code otherwise</returns>
        /// <remarks>
        /// The type and final length of the object must be specified
        /// when opening the stream.The returned stream will be of type `GIT_STREAM_WRONLY`, and it
        /// won't be effective until `git_odb_stream_finalize_write` is called
        /// and returns without an errorThe stream must always be freed when done with `git_odb_stream_free` or
        /// will leak memory.
        /// </remarks>
        /// <seealso cref="git_odb_stream"/>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_open_wstream")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_open_wstream(out libgit2.git_odb_stream* @out, libgit2.git_odb db, libgit2.git_object_size_t size, libgit2.git_object_t type);
        
        /// <summary>
        /// Write to an odb stream
        /// </summary>
        /// <param name="stream">the stream</param>
        /// <param name="buffer">the data to write</param>
        /// <param name="len">the buffer's length</param>
        /// <returns>0 if the write succeeded, error code otherwise</returns>
        /// <remarks>
        /// This method will fail if the total number of received bytes exceeds the
        /// size declared with `git_odb_open_wstream()`
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_stream_write")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_stream_write(ref libgit2.git_odb_stream stream, byte* buffer, nuint len);
        
        /// <summary>
        /// Finish writing to an odb stream
        /// </summary>
        /// <param name="out">pointer to store the resulting object's id</param>
        /// <param name="stream">the stream</param>
        /// <returns>0 on success, an error code otherwise</returns>
        /// <remarks>
        /// The object will take its final name and will be available to the
        /// odb.This method will fail if the total number of received bytes
        /// differs from the size declared with `git_odb_open_wstream()`
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_stream_finalize_write")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_stream_finalize_write(out libgit2.git_oid @out, libgit2.git_odb_stream* stream);
        
        /// <summary>
        /// Read from an odb stream
        /// </summary>
        /// <param name="stream">the stream</param>
        /// <param name="buffer">a user-allocated buffer to store the data in.</param>
        /// <param name="len">the buffer's length</param>
        /// <returns>0 if the read succeeded, error code otherwise</returns>
        /// <remarks>
        /// Most backends don't implement streaming reads
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_stream_read")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_stream_read(libgit2.git_odb_stream* stream, byte* buffer, nuint len);
        
        /// <summary>
        /// Free an odb stream
        /// </summary>
        /// <param name="stream">the stream to free</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_stream_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_odb_stream_free(libgit2.git_odb_stream* stream);
        
        /// <summary>
        /// Open a stream to read an object from the ODB
        /// </summary>
        /// <param name="out">pointer where to store the stream</param>
        /// <param name="len">pointer where to store the length of the object</param>
        /// <param name="type">pointer where to store the type of the object</param>
        /// <param name="db">object database where the stream will read from</param>
        /// <param name="oid">oid of the object the stream will read from</param>
        /// <returns>0 if the stream was created, error code otherwise</returns>
        /// <remarks>
        /// Note that most backends do *not* support streaming reads
        /// because they store their objects as compressed/delta'ed blobs.It's recommended to use `git_odb_read` instead, which is
        /// assured to work on all backends.The returned stream will be of type `GIT_STREAM_RDONLY` and
        /// will have the following methods:- stream-&gt;read: read `n` bytes from the stream
        /// - stream-&gt;free: free the streamThe stream must always be free'd or will leak memory.
        /// </remarks>
        /// <seealso cref="git_odb_stream"/>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_open_rstream")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_open_rstream(out libgit2.git_odb_stream* @out, out nuint len, out libgit2.git_object_t type, libgit2.git_odb db, in libgit2.git_oid oid);
        
        /// <summary>
        /// Open a stream for writing a pack file to the ODB.
        /// </summary>
        /// <param name="out">pointer to the writepack functions</param>
        /// <param name="db">object database where the stream will read from</param>
        /// <param name="progress_cb">function to call with progress information.
        /// Be aware that this is called inline with network and indexing operations,
        /// so performance may be affected.</param>
        /// <param name="progress_payload">payload for the progress callback</param>
        /// <returns>0 or an error code.</returns>
        /// <remarks>
        /// If the ODB layer understands pack files, then the given
        /// packfile will likely be streamed directly to disk (and a
        /// corresponding index created).  If the ODB layer does not
        /// understand pack files, the objects will be stored in whatever
        /// format the ODB layer uses.
        /// </remarks>
        /// <seealso cref="git_odb_writepack"/>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_write_pack")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_write_pack(out libgit2.git_odb_writepack* @out, libgit2.git_odb db, libgit2.git_indexer_progress_cb progress_cb, void* progress_payload);
        
        /// <summary>
        /// Write a `multi-pack-index` file from all the `.pack` files in the ODB.
        /// </summary>
        /// <param name="db">object database where the `multi-pack-index` file will be written.</param>
        /// <returns>0 or an error code.</returns>
        /// <remarks>
        /// If the ODB layer understands pack files, then this will create a file called
        /// `multi-pack-index` next to the `.pack` and `.idx` files, which will contain
        /// an index of all objects stored in `.pack` files. This will allow for
        /// O(log n) lookup for n objects (regardless of how many packfiles there
        /// exist).
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_write_multi_pack_index")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_write_multi_pack_index(libgit2.git_odb db);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_hash")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_hash(out libgit2.git_oid @out, void* data, nuint len, libgit2.git_object_t type);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_hashfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_hashfile(out libgit2.git_oid @out, byte* path, libgit2.git_object_t type);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_hashfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_hashfile(out libgit2.git_oid @out, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> path, libgit2.git_object_t type);
        
        /// <summary>
        /// Create a copy of an odb_object
        /// </summary>
        /// <param name="dest">pointer where to store the copy</param>
        /// <param name="source">object to copy</param>
        /// <returns>0 or an error code</returns>
        /// <remarks>
        /// The returned copy must be manually freed with `git_odb_object_free`.
        /// Note that because of an implementation detail, the returned copy will be
        /// the same pointer as `source`: the object is internally refcounted, so the
        /// copy still needs to be freed twice.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_dup")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_object_dup(out libgit2.git_odb_object dest, libgit2.git_odb_object source);
        
        /// <summary>
        /// Close an ODB object
        /// </summary>
        /// <param name="object">object to close</param>
        /// <remarks>
        /// This method must always be called once a `git_odb_object` is no
        /// longer needed, otherwise memory will leak.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_odb_object_free(libgit2.git_odb_object @object);
        
        /// <summary>
        /// Return the OID of an ODB object
        /// </summary>
        /// <param name="object">the object</param>
        /// <returns>a pointer to the OID</returns>
        /// <remarks>
        /// This is the OID from which the object was read from
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_id")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_oid* git_odb_object_id(libgit2.git_odb_object @object);
        
        /// <summary>
        /// Return the data of an ODB object
        /// </summary>
        /// <param name="object">the object</param>
        /// <returns>a pointer to the data</returns>
        /// <remarks>
        /// This is the uncompressed, raw data as read from the ODB,
        /// without the leading header.This pointer is owned by the object and shall not be free'd.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_data")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void* git_odb_object_data(libgit2.git_odb_object @object);
        
        /// <summary>
        /// Return the size of an ODB object
        /// </summary>
        /// <param name="object">the object</param>
        /// <returns>the size</returns>
        /// <remarks>
        /// This is the real size of the `data` buffer, not the
        /// actual size of the object.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_size")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint git_odb_object_size(libgit2.git_odb_object @object);
        
        /// <summary>
        /// Return the type of an ODB object
        /// </summary>
        /// <param name="object">the object</param>
        /// <returns>the type</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_object_type")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_object_t git_odb_object_type(libgit2.git_odb_object @object);
        
        /// <summary>
        /// Add a custom backend to an existing Object DB
        /// </summary>
        /// <param name="odb">database to add the backend to</param>
        /// <param name="backend">pointer to a git_odb_backend instance</param>
        /// <param name="priority">Value for ordering the backends queue</param>
        /// <returns>0 on success, error code otherwise</returns>
        /// <remarks>
        /// The backends are checked in relative ordering, based on the
        /// value of the `priority` parameter.Read 
        /// &lt;sys
        /// /odb_backend.h&gt; for more information.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_add_backend")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_add_backend(libgit2.git_odb odb, libgit2.git_odb_backend backend, int priority);
        
        /// <summary>
        /// Add a custom backend to an existing Object DB; this
        /// backend will work as an alternate.
        /// </summary>
        /// <param name="odb">database to add the backend to</param>
        /// <param name="backend">pointer to a git_odb_backend instance</param>
        /// <param name="priority">Value for ordering the backends queue</param>
        /// <returns>0 on success, error code otherwise</returns>
        /// <remarks>
        /// Alternate backends are always checked for objects *after*
        /// all the main backends have been exhausted.The backends are checked in relative ordering, based on the
        /// value of the `priority` parameter.Writing is disabled on alternate backends.Read 
        /// &lt;sys
        /// /odb_backend.h&gt; for more information.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_add_alternate")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_add_alternate(libgit2.git_odb odb, libgit2.git_odb_backend backend, int priority);
        
        /// <summary>
        /// Get the number of ODB backend objects
        /// </summary>
        /// <param name="odb">object database</param>
        /// <returns>number of backends in the ODB</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_num_backends")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint git_odb_num_backends(libgit2.git_odb odb);
        
        /// <summary>
        /// Lookup an ODB backend object by index
        /// </summary>
        /// <param name="out">output pointer to ODB backend at pos</param>
        /// <param name="odb">object database</param>
        /// <param name="pos">index into object database backend list</param>
        /// <returns>@return 0 on success, GIT_ENOTFOUND if pos is invalid, other errors 
        /// &lt;
        /// 0</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_get_backend")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_get_backend(out libgit2.git_odb_backend @out, libgit2.git_odb odb, nuint pos);
        
        /// <summary>
        /// Set the git commit-graph for the ODB.
        /// </summary>
        /// <param name="odb">object database</param>
        /// <param name="cgraph">the git commit-graph</param>
        /// <returns>0 on success; error code otherwise</returns>
        /// <remarks>
        /// After a successful call, the ownership of the cgraph parameter will be
        /// transferred to libgit2, and the caller should not free it.The commit-graph can also be unset by explicitly passing NULL as the cgraph
        /// parameter.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_set_commit_graph")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_odb_set_commit_graph(libgit2.git_odb odb, libgit2.git_commit_graph cgraph);
    }
}
