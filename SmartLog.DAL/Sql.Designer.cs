﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartLog.DAL {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartLog.DAL.Sql", typeof(Sql).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to delete from [custom_attributes].
        /// </summary>
        internal static string DeleteCustomAttributes {
            get {
                return ResourceManager.GetString("DeleteCustomAttributes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to delete from [log_data].
        /// </summary>
        internal static string DeleteLogData {
            get {
                return ResourceManager.GetString("DeleteLogData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to delete from [logs].
        /// </summary>
        internal static string DeleteLogs {
            get {
                return ResourceManager.GetString("DeleteLogs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to insert into [custom_attributes] ([logs_id], [name], [value])
        ///values(@pLogsId, @pName, @pValue).
        /// </summary>
        internal static string InsertCustomAttributes {
            get {
                return ResourceManager.GetString("InsertCustomAttributes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to insert into [log_data] ([logs_id], [data_key], [data_value])
        ///values(@pLogsId, @pDataKey, @pDataValue).
        /// </summary>
        internal static string InsertLogData {
            get {
                return ResourceManager.GetString("InsertLogData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to insert into [logs] ([create_date], [log_guid], [message], [method_name], [parent], [type])
        ///values(@pCreateDate, @pLogGuid, @pMessage, @pMethodName, @pParent, @pType).
        /// </summary>
        internal static string InsertLogs {
            get {
                return ResourceManager.GetString("InsertLogs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select [custom_attributes_id], [logs_id], [name], [value] from [custom_attributes] where[logs_id] in ({0}).
        /// </summary>
        internal static string SelectCustomAttributes {
            get {
                return ResourceManager.GetString("SelectCustomAttributes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select [log_data_id], [logs_id], [data_key], [data_value] from [log_data] where[logs_id] in ({0}).
        /// </summary>
        internal static string SelectLogData {
            get {
                return ResourceManager.GetString("SelectLogData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select [logs_id], [log_guid], [parent], [create_date], [type], [method_name], [message] from [logs]
        ///where [create_date] &gt;= @pInitial and [create_date] &lt;= @pFinal
        ///order by [create_date].
        /// </summary>
        internal static string SelectLogs {
            get {
                return ResourceManager.GetString("SelectLogs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select [log_types_id], [name] from [log_types]
        ///  order by [log_types_id].
        /// </summary>
        internal static string SelectLogTypes {
            get {
                return ResourceManager.GetString("SelectLogTypes", resourceCulture);
            }
        }
    }
}
