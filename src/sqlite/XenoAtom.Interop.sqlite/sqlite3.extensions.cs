// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the sqlite library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class sqlite
{
    /// <summary>
    /// * CAPI3REF: One-Step Query Execution Interface
    /// * METHOD: sqlite3
    /// *
    /// * The sqlite3_exec() interface is a convenience wrapper around
    /// * [sqlite3_prepare_v2()], [sqlite3_step()], and [sqlite3_finalize()],
    /// * that allows an application to run multiple statements of SQL
    /// * without having to use a lot of C code.
    /// *
    /// * ^The sqlite3_exec() interface runs zero or more UTF-8 encoded,
    /// * semicolon-separate SQL statements passed into its 2nd argument,
    /// * in the context of the [database connection] passed in as its 1st
    /// * argument.  ^If the callback function of the 3rd argument to
    /// * sqlite3_exec() is not NULL, then it is invoked for each result row
    /// * coming out of the evaluated SQL statements.  ^The 4th argument to
    /// * sqlite3_exec() is relayed through to the 1st argument of each
    /// * callback invocation.  ^If the callback pointer to sqlite3_exec()
    /// * is NULL, then no callback is ever invoked and result rows are
    /// * ignored.
    /// *
    /// * ^If an error occurs while evaluating the SQL statements passed into
    /// * sqlite3_exec(), then execution of the current statement stops and
    /// * subsequent statements are skipped.  ^If the 5th parameter to sqlite3_exec()
    /// * is not NULL then any error message is written into memory obtained
    /// * from [sqlite3_malloc()] and passed back through the 5th parameter.
    /// * To avoid memory leaks, the application should invoke [sqlite3_free()]
    /// * on error message strings returned through the 5th parameter of
    /// * sqlite3_exec() after the error message string is no longer needed.
    /// * ^If the 5th parameter to sqlite3_exec() is not NULL and no errors
    /// * occur, then sqlite3_exec() sets the pointer in its 5th parameter to
    /// * NULL before returning.
    /// *
    /// * ^If an sqlite3_exec() callback returns non-zero, the sqlite3_exec()
    /// * routine returns SQLITE_ABORT without invoking the callback again and
    /// * without running any subsequent SQL statements.
    /// *
    /// * ^The 2nd argument to the sqlite3_exec() callback function is the
    /// * number of columns in the result.  ^The 3rd argument to the sqlite3_exec()
    /// * callback is an array of pointers to strings obtained as if from
    /// * [sqlite3_column_text()], one for each column.  ^If an element of a
    /// * result row is NULL then the corresponding string pointer for the
    /// * sqlite3_exec() callback is a NULL pointer.  ^The 4th argument to the
    /// * sqlite3_exec() callback is an array of pointers to strings where each
    /// * entry represents the name of corresponding result column as obtained
    /// * from [sqlite3_column_name()].
    /// *
    /// * ^If the 2nd parameter to sqlite3_exec() is a NULL pointer, a pointer
    /// * to an empty string, or a pointer that contains only whitespace and/or
    /// * SQL comments, then no SQL statements are evaluated and the database
    /// * is not changed.
    /// *
    /// * Restrictions:
    /// *
    /// * &lt;ul&gt;* &lt;li&gt;The application must ensure that the 1st parameter to sqlite3_exec()
    /// *      is a valid and open [database connection].
    /// * &lt;li&gt;The application must not close the [database connection] specified by
    /// *      the 1st parameter to sqlite3_exec() while sqlite3_exec() is running.
    /// * &lt;li&gt;The application must not modify the SQL statement text passed into
    /// *      the 2nd parameter of sqlite3_exec() while sqlite3_exec() is running.
    /// * &lt;/ul&gt;
    /// </summary>
    public static int sqlite3_exec(sqlite.sqlite3 arg0, ReadOnlySpan<char> sql, sqlite3_exec_callback callback)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            return sqlite3_exec(arg0, sql, &sqlite3_exec_default_callback, (void*)GCHandle.ToIntPtr(handle), out _);
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// * CAPI3REF: One-Step Query Execution Interface
    /// * METHOD: sqlite3
    /// *
    /// * The sqlite3_exec() interface is a convenience wrapper around
    /// * [sqlite3_prepare_v2()], [sqlite3_step()], and [sqlite3_finalize()],
    /// * that allows an application to run multiple statements of SQL
    /// * without having to use a lot of C code.
    /// *
    /// * ^The sqlite3_exec() interface runs zero or more UTF-8 encoded,
    /// * semicolon-separate SQL statements passed into its 2nd argument,
    /// * in the context of the [database connection] passed in as its 1st
    /// * argument.  ^If the callback function of the 3rd argument to
    /// * sqlite3_exec() is not NULL, then it is invoked for each result row
    /// * coming out of the evaluated SQL statements.  ^The 4th argument to
    /// * sqlite3_exec() is relayed through to the 1st argument of each
    /// * callback invocation.  ^If the callback pointer to sqlite3_exec()
    /// * is NULL, then no callback is ever invoked and result rows are
    /// * ignored.
    /// *
    /// * ^If an error occurs while evaluating the SQL statements passed into
    /// * sqlite3_exec(), then execution of the current statement stops and
    /// * subsequent statements are skipped.  ^If the 5th parameter to sqlite3_exec()
    /// * is not NULL then any error message is written into memory obtained
    /// * from [sqlite3_malloc()] and passed back through the 5th parameter.
    /// * To avoid memory leaks, the application should invoke [sqlite3_free()]
    /// * on error message strings returned through the 5th parameter of
    /// * sqlite3_exec() after the error message string is no longer needed.
    /// * ^If the 5th parameter to sqlite3_exec() is not NULL and no errors
    /// * occur, then sqlite3_exec() sets the pointer in its 5th parameter to
    /// * NULL before returning.
    /// *
    /// * ^If an sqlite3_exec() callback returns non-zero, the sqlite3_exec()
    /// * routine returns SQLITE_ABORT without invoking the callback again and
    /// * without running any subsequent SQL statements.
    /// *
    /// * ^The 2nd argument to the sqlite3_exec() callback function is the
    /// * number of columns in the result.  ^The 3rd argument to the sqlite3_exec()
    /// * callback is an array of pointers to strings obtained as if from
    /// * [sqlite3_column_text()], one for each column.  ^If an element of a
    /// * result row is NULL then the corresponding string pointer for the
    /// * sqlite3_exec() callback is a NULL pointer.  ^The 4th argument to the
    /// * sqlite3_exec() callback is an array of pointers to strings where each
    /// * entry represents the name of corresponding result column as obtained
    /// * from [sqlite3_column_name()].
    /// *
    /// * ^If the 2nd parameter to sqlite3_exec() is a NULL pointer, a pointer
    /// * to an empty string, or a pointer that contains only whitespace and/or
    /// * SQL comments, then no SQL statements are evaluated and the database
    /// * is not changed.
    /// *
    /// * Restrictions:
    /// *
    /// * &lt;ul&gt;* &lt;li&gt;The application must ensure that the 1st parameter to sqlite3_exec()
    /// *      is a valid and open [database connection].
    /// * &lt;li&gt;The application must not close the [database connection] specified by
    /// *      the 1st parameter to sqlite3_exec() while sqlite3_exec() is running.
    /// * &lt;li&gt;The application must not modify the SQL statement text passed into
    /// *      the 2nd parameter of sqlite3_exec() while sqlite3_exec() is running.
    /// * &lt;/ul&gt;
    /// </summary>
    public static int sqlite3_exec(sqlite.sqlite3 arg0, ReadOnlySpan<char> sql, sqlite3_exec_callback callback, out string? errMsg)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            errMsg = null;

            byte* nativeErrMsg;
            var rc = sqlite3_exec(arg0, sql, &sqlite3_exec_default_callback, (void*)GCHandle.ToIntPtr(handle), out nativeErrMsg);

            if (rc != SQLITE_OK && nativeErrMsg != null)
            {
                errMsg = Marshal.PtrToStringUTF8((nint)nativeErrMsg);
                sqlite3_free(nativeErrMsg);
            }

            return rc;
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// Callback used by sqlite3_exec, called for each row returned.
    /// </summary>
    /// <param name="columns">The columns returned for the current row.</param>
    /// <returns>Value different from zero if processing the row failed.</returns>
    public delegate int sqlite3_exec_callback(sqlite3_exec_callback_columns columns);

    /// <summary>
    /// Columns returned for the current row in the sqlite3_exec_callback.
    /// </summary>
    public ref struct sqlite3_exec_callback_columns
    {
        /// <summary>
        /// Creates a new instance of <see cref="sqlite3_exec_callback_columns"/>.
        /// </summary>
        /// <param name="count">The number of columns</param>  
        /// <param name="columnTexts">The pointer to an array of string</param>
        /// <param name="columnNames">The pointer to an array of string</param>
        public sqlite3_exec_callback_columns(int count, byte** columnTexts, byte** columnNames)
        {
            this.Count = count;
            this.ColumnTexts = columnTexts;
            this.ColumnNames = columnNames;
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Count;

        /// <summary>
        /// The pointer to an array of string for column texts.
        /// </summary>
        public byte** ColumnTexts;

        /// <summary>
        /// The pointer to an array of string for column names.
        /// </summary>
        public byte** ColumnNames;

        /// <summary>
        /// Gets the text of the column at the specified index.
        /// </summary>
        /// <param name="index">The index of the column</param>
        /// <returns>The string of the column text</returns>
        /// <exception cref="ArgumentOutOfRangeException">If index is out of range.</exception>
        public string? GetColumnText(int index)
        {
            if ((uint)index >= (uint)Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Marshal.PtrToStringUTF8((nint)ColumnTexts[index]);
        }
        
        /// <summary>
        /// Gets the name of the column at the specified index.
        /// </summary>
        /// <param name="index">The index of the column</param>
        /// <returns>The string of the column name</returns>
        /// <exception cref="ArgumentOutOfRangeException">If index is out of range.</exception>
        public string? GetColumnName(int index)
        {
            if ((uint)index >= (uint)Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return Marshal.PtrToStringUTF8((nint)ColumnNames[index]);
        }
    }

    /// <summary>
    /// Internal callback
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static int sqlite3_exec_default_callback(void* arg0, int arg1, byte** arg2, byte** arg3)
    {
        GCHandle handle = GCHandle.FromIntPtr((IntPtr)arg0);
        var callback = (sqlite3_exec_callback)handle.Target!;
        return callback(new sqlite3_exec_callback_columns(arg1, arg2, arg3));
    }

    /// <summary>
    /// * CAPI3REF: Compiling An SQL Statement
    /// * KEYWORDS: {SQL statement compiler}
    /// * METHOD: sqlite3
    /// * CONSTRUCTOR: sqlite3_stmt
    /// *
    /// * To execute an SQL statement, it must first be compiled into a byte-code
    /// * program using one of these routines.  Or, in other words, these routines
    /// * are constructors for the [prepared statement] object.
    /// *
    /// * The preferred routine to use is [sqlite3_prepare_v2()].  The
    /// * [sqlite3_prepare()] interface is legacy and should be avoided.
    /// * [sqlite3_prepare_v3()] has an extra "prepFlags" option that is used
    /// * for special purposes.
    /// *
    /// * The use of the UTF-8 interfaces is preferred, as SQLite currently
    /// * does all parsing using UTF-8.  The UTF-16 interfaces are provided
    /// * as a convenience.  The UTF-16 interfaces work by converting the
    /// * input text into UTF-8, then invoking the corresponding UTF-8 interface.
    /// *
    /// * The first argument, "db", is a [database connection] obtained from a
    /// * prior successful call to [sqlite3_open()], [sqlite3_open_v2()] or
    /// * [sqlite3_open16()].  The database connection must not have been closed.
    /// *
    /// * The second argument, "zSql", is the statement to be compiled, encoded
    /// * as either UTF-8 or UTF-16.  The sqlite3_prepare(), sqlite3_prepare_v2(),
    /// * and sqlite3_prepare_v3()
    /// * interfaces use UTF-8, and sqlite3_prepare16(), sqlite3_prepare16_v2(),
    /// * and sqlite3_prepare16_v3() use UTF-16.
    /// *
    /// * ^If pzTail is not NULL then *pzTail is made to point to the first byte
    /// * past the end of the first SQL statement in zSql.  These routines only
    /// * compile the first statement in zSql, so *pzTail is left pointing to
    /// * what remains uncompiled.
    /// *
    /// * ^*ppStmt is left pointing to a compiled [prepared statement] that can be
    /// * executed using [sqlite3_step()].  ^If there is an error, *ppStmt is set
    /// * to NULL.  ^If the input text contains no SQL (if the input is an empty
    /// * string or a comment) then *ppStmt is set to NULL.
    /// * The calling procedure is responsible for deleting the compiled
    /// * SQL statement using [sqlite3_finalize()] after it has finished with it.
    /// * ppStmt may not be NULL.
    /// *
    /// * ^On success, the sqlite3_prepare() family of routines return [SQLITE_OK];
    /// * otherwise an [error code] is returned.
    /// *
    /// * The sqlite3_prepare_v2(), sqlite3_prepare_v3(), sqlite3_prepare16_v2(),
    /// * and sqlite3_prepare16_v3() interfaces are recommended for all new programs.
    /// * The older interfaces (sqlite3_prepare() and sqlite3_prepare16())
    /// * are retained for backwards compatibility, but their use is discouraged.
    /// * ^In the "vX" interfaces, the prepared statement
    /// * that is returned (the [sqlite3_stmt] object) contains a copy of the
    /// * original SQL text. This causes the [sqlite3_step()] interface to
    /// * behave differently in three ways:
    /// *
    /// * &lt;ol&gt;* &lt;li&gt;* ^If the database schema changes, instead of returning [SQLITE_SCHEMA] as it
    /// * always used to do, [sqlite3_step()] will automatically recompile the SQL
    /// * statement and try to run it again. As many as [SQLITE_MAX_SCHEMA_RETRY]
    /// * retries will occur before sqlite3_step() gives up and returns an error.
    /// * &lt;/li&gt;*
    /// * &lt;li&gt;* ^When an error occurs, [sqlite3_step()] will return one of the detailed
    /// * [error codes] or [extended error codes].  ^The legacy behavior was that
    /// * [sqlite3_step()] would only return a generic [SQLITE_ERROR] result code
    /// * and the application would have to make a second call to [sqlite3_reset()]
    /// * in order to find the underlying cause of the problem. With the "v2" prepare
    /// * interfaces, the underlying reason for the error is returned immediately.
    /// * &lt;/li&gt;*
    /// * &lt;li&gt;* ^If the specific value bound to a [parameter | host parameter] in the
    /// * WHERE clause might influence the choice of query plan for a statement,
    /// * then the statement will be automatically recompiled, as if there had been
    /// * a schema change, on the first [sqlite3_step()] call following any change
    /// * to the [sqlite3_bind_text | bindings] of that [parameter].
    /// * ^The specific value of a WHERE-clause [parameter] might influence the
    /// * choice of query plan if the parameter is the left-hand side of a [LIKE]
    /// * or [GLOB] operator or if the parameter is compared to an indexed column
    /// * and the [SQLITE_ENABLE_STAT4] compile-time option is enabled.
    /// * &lt;/li&gt;* &lt;/ol&gt;*
    /// * &lt;p&gt;^sqlite3_prepare_v3() differs from sqlite3_prepare_v2() only in having
    /// * the extra prepFlags parameter, which is a bit array consisting of zero or
    /// * more of the [SQLITE_PREPARE_PERSISTENT|SQLITE_PREPARE_*] flags.  ^The
    /// * sqlite3_prepare_v2() interface works exactly the same as
    /// * sqlite3_prepare_v3() with a zero prepFlags parameter.
    /// </summary>
    public static int sqlite3_prepare(sqlite.sqlite3 db, ReadOnlySpan<byte> zSql, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (byte* zSqlPtr = zSql)
        {
            return sqlite3_prepare(db, zSqlPtr, zSql.Length, out ppStmt, out _);
        }
    }

    public static int sqlite3_prepare_v2(sqlite.sqlite3 db, ReadOnlySpan<byte> zSql, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (byte* zSqlPtr = zSql)
        {
            return sqlite3_prepare_v2(db, zSqlPtr, zSql.Length, out ppStmt, out _);
        }
    }

    public static int sqlite3_prepare_v3(sqlite.sqlite3 db, ReadOnlySpan<byte> zSql, uint prepFlags, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (byte* zSqlPtr = zSql)
        {
            return sqlite3_prepare_v3(db, zSqlPtr, zSql.Length, prepFlags, out ppStmt, out _);
        }
    }

    public static int sqlite3_prepare16(sqlite.sqlite3 db, ReadOnlySpan<char> zSql, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (char* zSqlPtr = zSql)
        {
            return sqlite3_prepare16(db, zSqlPtr, zSql.Length * sizeof(char), out ppStmt, out _);
        }
    }

    public static int sqlite3_prepare16_v2(sqlite.sqlite3 db, ReadOnlySpan<char> zSql, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (char* zSqlPtr = zSql)
        {
            return sqlite3_prepare16_v2(db, zSqlPtr, zSql.Length * sizeof(char), out ppStmt, out _);
        }
    }

    public static int sqlite3_prepare16_v3(sqlite.sqlite3 db, ReadOnlySpan<char> zSql, uint prepFlags, out sqlite.sqlite3_stmt ppStmt)
    {
        fixed (char* zSqlPtr = zSql)
        {
            return sqlite3_prepare16_v3(db, zSqlPtr, zSql.Length * sizeof(char), prepFlags, out ppStmt, out _);
        }
    }
}