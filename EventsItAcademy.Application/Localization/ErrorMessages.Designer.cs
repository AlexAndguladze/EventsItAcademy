﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventsItAcademy.Application.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EventsItAcademy.Application.Localization.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Could not delete item.
        /// </summary>
        internal static string DeleteFailed {
            get {
                return ResourceManager.GetString("DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is invalid.
        /// </summary>
        internal static string InvalidEmail {
            get {
                return ResourceManager.GetString("InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is invalid.
        /// </summary>
        internal static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item not found.
        /// </summary>
        internal static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password Does not match.
        /// </summary>
        internal static string PasswordDontMatch {
            get {
                return ResourceManager.GetString("PasswordDontMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registration model is null.
        /// </summary>
        internal static string RegistrationModelIsNull {
            get {
                return ResourceManager.GetString("RegistrationModelIsNull", resourceCulture);
            }
        }
    }
}