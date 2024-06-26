// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System.Collections.Generic;

namespace XenoAtom.Interop.CodeGen.sqlite;

internal partial class SqliteGenerator
{
    /// <summary>
    /// Function list extracted from https://www.sqlite.org/c3ref/funclist.html
    /// </summary>
    private static readonly Dictionary<string, string> MapFunctionToUrlPart = new()
    {
        { "sqlite3_aggregate_context", "/c3ref/aggregate_context.html" },
        { "sqlite3_auto_extension", "/c3ref/auto_extension.html" },
        { "sqlite3_autovacuum_pages", "/c3ref/autovacuum_pages.html" },
        { "sqlite3_backup_finish", "/c3ref/backup_finish.html#sqlite3backupfinish" },
        { "sqlite3_backup_init", "/c3ref/backup_finish.html#sqlite3backupinit" },
        { "sqlite3_backup_pagecount", "/c3ref/backup_finish.html#sqlite3backuppagecount" },
        { "sqlite3_backup_remaining", "/c3ref/backup_finish.html#sqlite3backupremaining" },
        { "sqlite3_backup_step", "/c3ref/backup_finish.html#sqlite3backupstep" },
        { "sqlite3_bind_blob", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_blob64", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_double", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_int", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_int64", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_null", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_parameter_count", "/c3ref/bind_parameter_count.html" },
        { "sqlite3_bind_parameter_index", "/c3ref/bind_parameter_index.html" },
        { "sqlite3_bind_parameter_name", "/c3ref/bind_parameter_name.html" },
        { "sqlite3_bind_pointer", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_text", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_text16", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_text64", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_value", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_zeroblob", "/c3ref/bind_blob.html" },
        { "sqlite3_bind_zeroblob64", "/c3ref/bind_blob.html" },
        { "sqlite3_blob_bytes", "/c3ref/blob_bytes.html" },
        { "sqlite3_blob_close", "/c3ref/blob_close.html" },
        { "sqlite3_blob_open", "/c3ref/blob_open.html" },
        { "sqlite3_blob_read", "/c3ref/blob_read.html" },
        { "sqlite3_blob_reopen", "/c3ref/blob_reopen.html" },
        { "sqlite3_blob_write", "/c3ref/blob_write.html" },
        { "sqlite3_busy_handler", "/c3ref/busy_handler.html" },
        { "sqlite3_busy_timeout", "/c3ref/busy_timeout.html" },
        { "sqlite3_cancel_auto_extension", "/c3ref/cancel_auto_extension.html" },
        { "sqlite3_changes", "/c3ref/changes.html" },
        { "sqlite3_changes64", "/c3ref/changes.html" },
        { "sqlite3_clear_bindings", "/c3ref/clear_bindings.html" },
        { "sqlite3_close_v2", "/c3ref/close.html" },
        { "sqlite3_close", "/c3ref/close.html" },
        { "sqlite3_collation_needed", "/c3ref/collation_needed.html" },
        { "sqlite3_collation_needed16", "/c3ref/collation_needed.html" },
        { "sqlite3_column_blob", "/c3ref/column_blob.html" },
        { "sqlite3_column_bytes", "/c3ref/column_blob.html" },
        { "sqlite3_column_bytes16", "/c3ref/column_blob.html" },
        { "sqlite3_column_count", "/c3ref/column_count.html" },
        { "sqlite3_column_database_name", "/c3ref/column_database_name.html" },
        { "sqlite3_column_database_name16", "/c3ref/column_database_name.html" },
        { "sqlite3_column_decltype", "/c3ref/column_decltype.html" },
        { "sqlite3_column_decltype16", "/c3ref/column_decltype.html" },
        { "sqlite3_column_double", "/c3ref/column_blob.html" },
        { "sqlite3_column_int", "/c3ref/column_blob.html" },
        { "sqlite3_column_int64", "/c3ref/column_blob.html" },
        { "sqlite3_column_name", "/c3ref/column_name.html" },
        { "sqlite3_column_name16", "/c3ref/column_name.html" },
        { "sqlite3_column_origin_name", "/c3ref/column_database_name.html" },
        { "sqlite3_column_origin_name16", "/c3ref/column_database_name.html" },
        { "sqlite3_column_table_name", "/c3ref/column_database_name.html" },
        { "sqlite3_column_table_name16", "/c3ref/column_database_name.html" },
        { "sqlite3_column_text", "/c3ref/column_blob.html" },
        { "sqlite3_column_text16", "/c3ref/column_blob.html" },
        { "sqlite3_column_type", "/c3ref/column_blob.html" },
        { "sqlite3_column_value", "/c3ref/column_blob.html" },
        { "sqlite3_commit_hook", "/c3ref/commit_hook.html" },
        { "sqlite3_compileoption_get", "/c3ref/compileoption_get.html" },
        { "sqlite3_compileoption_used", "/c3ref/compileoption_get.html" },
        { "sqlite3_complete", "/c3ref/complete.html" },
        { "sqlite3_complete16", "/c3ref/complete.html" },
        { "sqlite3_config", "/c3ref/config.html" },
        { "sqlite3_context_db_handle", "/c3ref/context_db_handle.html" },
        { "sqlite3_create_collation_v2", "/c3ref/create_collation.html" },
        { "sqlite3_create_collation", "/c3ref/create_collation.html" },
        { "sqlite3_create_collation16", "/c3ref/create_collation.html" },
        { "sqlite3_create_filename", "/c3ref/create_filename.html" },
        { "sqlite3_create_function_v2", "/c3ref/create_function.html" },
        { "sqlite3_create_function", "/c3ref/create_function.html" },
        { "sqlite3_create_function16", "/c3ref/create_function.html" },
        { "sqlite3_create_module_v2", "/c3ref/create_module.html" },
        { "sqlite3_create_module", "/c3ref/create_module.html" },
        { "sqlite3_create_window_function", "/c3ref/create_function.html" },
        { "sqlite3_data_count", "/c3ref/data_count.html" },
        { "sqlite3_database_file_object", "/c3ref/database_file_object.html" },
        { "sqlite3_db_cacheflush", "/c3ref/db_cacheflush.html" },
        { "sqlite3_db_config", "/c3ref/db_config.html" },
        { "sqlite3_db_filename", "/c3ref/db_filename.html" },
        { "sqlite3_db_handle", "/c3ref/db_handle.html" },
        { "sqlite3_db_mutex", "/c3ref/db_mutex.html" },
        { "sqlite3_db_name", "/c3ref/db_name.html" },
        { "sqlite3_db_readonly", "/c3ref/db_readonly.html" },
        { "sqlite3_db_release_memory", "/c3ref/db_release_memory.html" },
        { "sqlite3_db_status", "/c3ref/db_status.html" },
        { "sqlite3_declare_vtab", "/c3ref/declare_vtab.html" },
        { "sqlite3_deserialize", "/c3ref/deserialize.html" },
        { "sqlite3_drop_modules", "/c3ref/drop_modules.html" },
        { "sqlite3_enable_load_extension", "/c3ref/enable_load_extension.html" },
        { "sqlite3_enable_shared_cache", "/c3ref/enable_shared_cache.html" },
        { "sqlite3_errcode", "/c3ref/errcode.html" },
        { "sqlite3_errmsg", "/c3ref/errcode.html" },
        { "sqlite3_errmsg16", "/c3ref/errcode.html" },
        { "sqlite3_error_offset", "/c3ref/errcode.html" },
        { "sqlite3_errstr", "/c3ref/errcode.html" },
        { "sqlite3_exec", "/c3ref/exec.html" },
        { "sqlite3_expanded_sql", "/c3ref/expanded_sql.html" },
        { "sqlite3_extended_errcode", "/c3ref/errcode.html" },
        { "sqlite3_extended_result_codes", "/c3ref/extended_result_codes.html" },
        { "sqlite3_file_control", "/c3ref/file_control.html" },
        { "sqlite3_filename_database", "/c3ref/filename_database.html" },
        { "sqlite3_filename_journal", "/c3ref/filename_database.html" },
        { "sqlite3_filename_wal", "/c3ref/filename_database.html" },
        { "sqlite3_finalize", "/c3ref/finalize.html" },
        { "sqlite3_free_filename", "/c3ref/create_filename.html" },
        { "sqlite3_free_table", "/c3ref/free_table.html" },
        { "sqlite3_free", "/c3ref/free.html" },
        { "sqlite3_get_autocommit", "/c3ref/get_autocommit.html" },
        { "sqlite3_get_auxdata", "/c3ref/get_auxdata.html" },
        { "sqlite3_get_clientdata", "/c3ref/get_clientdata.html" },
        { "sqlite3_get_table", "/c3ref/free_table.html" },
        { "sqlite3_hard_heap_limit64", "/c3ref/hard_heap_limit64.html" },
        { "sqlite3_initialize", "/c3ref/initialize.html" },
        { "sqlite3_interrupt", "/c3ref/interrupt.html" },
        { "sqlite3_is_interrupted", "/c3ref/interrupt.html" },
        { "sqlite3_keyword_check", "/c3ref/keyword_check.html" },
        { "sqlite3_keyword_count", "/c3ref/keyword_check.html" },
        { "sqlite3_keyword_name", "/c3ref/keyword_check.html" },
        { "sqlite3_last_insert_rowid", "/c3ref/last_insert_rowid.html" },
        { "sqlite3_libversion_number", "/c3ref/libversion.html" },
        { "sqlite3_libversion", "/c3ref/libversion.html" },
        { "sqlite3_limit", "/c3ref/limit.html" },
        { "sqlite3_load_extension", "/c3ref/load_extension.html" },
        { "sqlite3_log", "/c3ref/log.html" },
        { "sqlite3_malloc", "/c3ref/free.html" },
        { "sqlite3_malloc64", "/c3ref/free.html" },
        { "sqlite3_memory_highwater", "/c3ref/memory_highwater.html" },
        { "sqlite3_memory_used", "/c3ref/memory_highwater.html" },
        { "sqlite3_mprintf", "/c3ref/mprintf.html" },
        { "sqlite3_msize", "/c3ref/free.html" },
        { "sqlite3_mutex_alloc", "/c3ref/mutex_alloc.html" },
        { "sqlite3_mutex_enter", "/c3ref/mutex_alloc.html" },
        { "sqlite3_mutex_free", "/c3ref/mutex_alloc.html" },
        { "sqlite3_mutex_held", "/c3ref/mutex_held.html" },
        { "sqlite3_mutex_leave", "/c3ref/mutex_alloc.html" },
        { "sqlite3_mutex_notheld", "/c3ref/mutex_held.html" },
        { "sqlite3_mutex_try", "/c3ref/mutex_alloc.html" },
        { "sqlite3_next_stmt", "/c3ref/next_stmt.html" },
        { "sqlite3_normalized_sql", "/c3ref/expanded_sql.html" },
        { "sqlite3_open_v2", "/c3ref/open.html" },
        { "sqlite3_open", "/c3ref/open.html" },
        { "sqlite3_open16", "/c3ref/open.html" },
        { "sqlite3_os_end", "/c3ref/initialize.html" },
        { "sqlite3_os_init", "/c3ref/initialize.html" },
        { "sqlite3_overload_function", "/c3ref/overload_function.html" },
        { "sqlite3_prepare_v2", "/c3ref/prepare.html" },
        { "sqlite3_prepare_v3", "/c3ref/prepare.html" },
        { "sqlite3_prepare", "/c3ref/prepare.html" },
        { "sqlite3_prepare16_v2", "/c3ref/prepare.html" },
        { "sqlite3_prepare16_v3", "/c3ref/prepare.html" },
        { "sqlite3_prepare16", "/c3ref/prepare.html" },
        { "sqlite3_preupdate_blobwrite", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_preupdate_count", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_preupdate_depth", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_preupdate_hook", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_preupdate_new", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_preupdate_old", "/c3ref/preupdate_blobwrite.html" },
        { "sqlite3_profile", "/c3ref/profile.html" },
        { "sqlite3_progress_handler", "/c3ref/progress_handler.html" },
        { "sqlite3_randomness", "/c3ref/randomness.html" },
        { "sqlite3_realloc", "/c3ref/free.html" },
        { "sqlite3_realloc64", "/c3ref/free.html" },
        { "sqlite3_release_memory", "/c3ref/release_memory.html" },
        { "sqlite3_reset_auto_extension", "/c3ref/reset_auto_extension.html" },
        { "sqlite3_reset", "/c3ref/reset.html" },
        { "sqlite3_result_blob", "/c3ref/result_blob.html" },
        { "sqlite3_result_blob64", "/c3ref/result_blob.html" },
        { "sqlite3_result_double", "/c3ref/result_blob.html" },
        { "sqlite3_result_error_code", "/c3ref/result_blob.html" },
        { "sqlite3_result_error_nomem", "/c3ref/result_blob.html" },
        { "sqlite3_result_error_toobig", "/c3ref/result_blob.html" },
        { "sqlite3_result_error", "/c3ref/result_blob.html" },
        { "sqlite3_result_error16", "/c3ref/result_blob.html" },
        { "sqlite3_result_int", "/c3ref/result_blob.html" },
        { "sqlite3_result_int64", "/c3ref/result_blob.html" },
        { "sqlite3_result_null", "/c3ref/result_blob.html" },
        { "sqlite3_result_pointer", "/c3ref/result_blob.html" },
        { "sqlite3_result_subtype", "/c3ref/result_subtype.html" },
        { "sqlite3_result_text", "/c3ref/result_blob.html" },
        { "sqlite3_result_text16", "/c3ref/result_blob.html" },
        { "sqlite3_result_text16be", "/c3ref/result_blob.html" },
        { "sqlite3_result_text16le", "/c3ref/result_blob.html" },
        { "sqlite3_result_text64", "/c3ref/result_blob.html" },
        { "sqlite3_result_value", "/c3ref/result_blob.html" },
        { "sqlite3_result_zeroblob", "/c3ref/result_blob.html" },
        { "sqlite3_result_zeroblob64", "/c3ref/result_blob.html" },
        { "sqlite3_rollback_hook", "/c3ref/commit_hook.html" },
        { "sqlite3_serialize", "/c3ref/serialize.html" },
        { "sqlite3_set_authorizer", "/c3ref/set_authorizer.html" },
        { "sqlite3_set_auxdata", "/c3ref/get_auxdata.html" },
        { "sqlite3_set_clientdata", "/c3ref/get_clientdata.html" },
        { "sqlite3_set_last_insert_rowid", "/c3ref/set_last_insert_rowid.html" },
        { "sqlite3_shutdown", "/c3ref/initialize.html" },
        { "sqlite3_sleep", "/c3ref/sleep.html" },
        { "sqlite3_snapshot_cmp", "/c3ref/snapshot_cmp.html" },
        { "sqlite3_snapshot_free", "/c3ref/snapshot_free.html" },
        { "sqlite3_snapshot_get", "/c3ref/snapshot_get.html" },
        { "sqlite3_snapshot_open", "/c3ref/snapshot_open.html" },
        { "sqlite3_snapshot_recover", "/c3ref/snapshot_recover.html" },
        { "sqlite3_snprintf", "/c3ref/mprintf.html" },
        { "sqlite3_soft_heap_limit64", "/c3ref/hard_heap_limit64.html" },
        { "sqlite3_sourceid", "/c3ref/libversion.html" },
        { "sqlite3_sql", "/c3ref/expanded_sql.html" },
        { "sqlite3_status", "/c3ref/status.html" },
        { "sqlite3_status64", "/c3ref/status.html" },
        { "sqlite3_step", "/c3ref/step.html" },
        { "sqlite3_stmt_busy", "/c3ref/stmt_busy.html" },
        { "sqlite3_stmt_explain", "/c3ref/stmt_explain.html" },
        { "sqlite3_stmt_isexplain", "/c3ref/stmt_isexplain.html" },
        { "sqlite3_stmt_readonly", "/c3ref/stmt_readonly.html" },
        { "sqlite3_stmt_scanstatus_reset", "/c3ref/stmt_scanstatus_reset.html" },
        { "sqlite3_stmt_scanstatus_v2", "/c3ref/stmt_scanstatus.html" },
        { "sqlite3_stmt_scanstatus", "/c3ref/stmt_scanstatus.html" },
        { "sqlite3_stmt_status", "/c3ref/stmt_status.html" },
        { "sqlite3_str_append", "/c3ref/str_append.html" },
        { "sqlite3_str_appendall", "/c3ref/str_append.html" },
        { "sqlite3_str_appendchar", "/c3ref/str_append.html" },
        { "sqlite3_str_appendf", "/c3ref/str_append.html" },
        { "sqlite3_str_errcode", "/c3ref/str_errcode.html" },
        { "sqlite3_str_finish", "/c3ref/str_finish.html" },
        { "sqlite3_str_length", "/c3ref/str_errcode.html" },
        { "sqlite3_str_new", "/c3ref/str_new.html" },
        { "sqlite3_str_reset", "/c3ref/str_append.html" },
        { "sqlite3_str_value", "/c3ref/str_errcode.html" },
        { "sqlite3_str_vappendf", "/c3ref/str_append.html" },
        { "sqlite3_strglob", "/c3ref/strglob.html" },
        { "sqlite3_stricmp", "/c3ref/stricmp.html" },
        { "sqlite3_strlike", "/c3ref/strlike.html" },
        { "sqlite3_strnicmp", "/c3ref/stricmp.html" },
        { "sqlite3_system_errno", "/c3ref/system_errno.html" },
        { "sqlite3_table_column_metadata", "/c3ref/table_column_metadata.html" },
        { "sqlite3_test_control", "/c3ref/test_control.html" },
        { "sqlite3_threadsafe", "/c3ref/threadsafe.html" },
        { "sqlite3_total_changes", "/c3ref/total_changes.html" },
        { "sqlite3_total_changes64", "/c3ref/total_changes.html" },
        { "sqlite3_trace_v2", "/c3ref/trace_v2.html" },
        { "sqlite3_trace", "/c3ref/profile.html" },
        { "sqlite3_txn_state", "/c3ref/txn_state.html" },
        { "sqlite3_unlock_notify", "/c3ref/unlock_notify.html" },
        { "sqlite3_update_hook", "/c3ref/update_hook.html" },
        { "sqlite3_uri_boolean", "/c3ref/uri_boolean.html" },
        { "sqlite3_uri_int64", "/c3ref/uri_boolean.html" },
        { "sqlite3_uri_key", "/c3ref/uri_boolean.html" },
        { "sqlite3_uri_parameter", "/c3ref/uri_boolean.html" },
        { "sqlite3_user_data", "/c3ref/user_data.html" },
        { "sqlite3_value_blob", "/c3ref/value_blob.html" },
        { "sqlite3_value_bytes", "/c3ref/value_blob.html" },
        { "sqlite3_value_bytes16", "/c3ref/value_blob.html" },
        { "sqlite3_value_double", "/c3ref/value_blob.html" },
        { "sqlite3_value_dup", "/c3ref/value_dup.html" },
        { "sqlite3_value_encoding", "/c3ref/value_encoding.html" },
        { "sqlite3_value_free", "/c3ref/value_dup.html" },
        { "sqlite3_value_frombind", "/c3ref/value_blob.html" },
        { "sqlite3_value_int", "/c3ref/value_blob.html" },
        { "sqlite3_value_int64", "/c3ref/value_blob.html" },
        { "sqlite3_value_nochange", "/c3ref/value_blob.html" },
        { "sqlite3_value_numeric_type", "/c3ref/value_blob.html" },
        { "sqlite3_value_pointer", "/c3ref/value_blob.html" },
        { "sqlite3_value_subtype", "/c3ref/value_subtype.html" },
        { "sqlite3_value_text", "/c3ref/value_blob.html" },
        { "sqlite3_value_text16", "/c3ref/value_blob.html" },
        { "sqlite3_value_text16be", "/c3ref/value_blob.html" },
        { "sqlite3_value_text16le", "/c3ref/value_blob.html" },
        { "sqlite3_value_type", "/c3ref/value_blob.html" },
        { "sqlite3_version", "/c3ref/libversion.html" },
        { "sqlite3_vfs_find", "/c3ref/vfs_find.html" },
        { "sqlite3_vfs_register", "/c3ref/vfs_find.html" },
        { "sqlite3_vfs_unregister", "/c3ref/vfs_find.html" },
        { "sqlite3_vmprintf", "/c3ref/mprintf.html" },
        { "sqlite3_vsnprintf", "/c3ref/mprintf.html" },
        { "sqlite3_vtab_collation", "/c3ref/vtab_collation.html" },
        { "sqlite3_vtab_config", "/c3ref/vtab_config.html" },
        { "sqlite3_vtab_distinct", "/c3ref/vtab_distinct.html" },
        { "sqlite3_vtab_in_first", "/c3ref/vtab_in_first.html" },
        { "sqlite3_vtab_in_next", "/c3ref/vtab_in_first.html" },
        { "sqlite3_vtab_in", "/c3ref/vtab_in.html" },
        { "sqlite3_vtab_nochange", "/c3ref/vtab_nochange.html" },
        { "sqlite3_vtab_on_conflict", "/c3ref/vtab_on_conflict.html" },
        { "sqlite3_vtab_rhs_value", "/c3ref/vtab_rhs_value.html" },
        { "sqlite3_wal_autocheckpoint", "/c3ref/wal_autocheckpoint.html" },
        { "sqlite3_wal_checkpoint_v2", "/c3ref/wal_checkpoint_v2.html" },
        { "sqlite3_wal_checkpoint", "/c3ref/wal_checkpoint.html" },
        { "sqlite3_wal_hook", "/c3ref/wal_hook.html" },
        { "sqlite3_win32_set_directory", "/c3ref/win32_set_directory.html" },
        { "sqlite3_win32_set_directory16", "/c3ref/win32_set_directory.html" },
        { "sqlite3_win32_set_directory8", "/c3ref/win32_set_directory.html" },
    };
}