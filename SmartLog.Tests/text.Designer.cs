﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartLog.Tests {
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
    internal class text {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal text() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmartLog.Tests.text", typeof(text).Assembly);
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
        ///   Looks up a localized string similar to &lt;SmartLogRequest&gt;
        ///    &lt;Logs&gt;
        ///        &lt;Log&gt;
        ///            &lt;UId&gt;9297766D-6317-4589-8B95-C8D468DFB78B&lt;/UId&gt;
        ///            &lt;MethodName&gt;Execute1&lt;/MethodName&gt;
        ///            &lt;Type&gt;Info&lt;/Type&gt;
        ///            &lt;CreateDate&gt;2021-01-01T21:00:00&lt;/CreateDate&gt;
        ///            &lt;Message&gt;Message 1&lt;/Message&gt;
        ///        &lt;/Log&gt;
        ///        &lt;Log&gt;
        ///            &lt;UId&gt;7407C74A-C826-41C0-8491-178D04B02202&lt;/UId&gt;
        ///            &lt;MethodName&gt;Execute2&lt;/MethodName&gt;
        ///            &lt;Type&gt;Warning&lt;/Type&gt;
        ///            &lt;CreateDate&gt;2021-01-01T21:01:00&lt;/CreateDat [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string insert_request {
            get {
                return ResourceManager.GetString("insert_request", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;SmartLogResponse&gt;
        ///	&lt;Code&gt;200&lt;/Code&gt;
        ///&lt;/SmartLogResponse&gt;.
        /// </summary>
        internal static string response_200 {
            get {
                return ResourceManager.GetString("response_200", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;SmartLogResponse&gt;
        ///	&lt;Code&gt;404&lt;/Code&gt;
        ///	&lt;SmartLogError&gt;
        ///		&lt;UId&gt;51AB89D6-09DF-4C28-A0FF-061D0A8A6C45&lt;/UId&gt;
        ///		&lt;Message&gt;Not found.&lt;/Message&gt;
        ///	&lt;/SmartLogError&gt;
        ///&lt;/SmartLogResponse&gt;.
        /// </summary>
        internal static string response_404 {
            get {
                return ResourceManager.GetString("response_404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;SmartLogInfoResponse&gt;
        ///	&lt;Version&gt;1.0.0.0&lt;/Version&gt;
        ///&lt;/SmartLogInfoResponse&gt;.
        /// </summary>
        internal static string response_info {
            get {
                return ResourceManager.GetString("response_info", resourceCulture);
            }
        }
    }
}
