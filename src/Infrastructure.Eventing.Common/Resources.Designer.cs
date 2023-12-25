﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Eventing.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Eventing.Common.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to The consumer &apos;{0}&apos; did not handle the event &apos;{1}&apos; with event type &apos;{2}&apos;. Aborting notifications.
        /// </summary>
        internal static string EventNotificationNotifier_ConsumerError {
            get {
                return ResourceManager.GetString("EventNotificationNotifier_ConsumerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The producer &apos;{0}&apos; did not handle the event &apos;{1}&apos; with event type &apos;{2}&apos;. Aborting notifications.
        /// </summary>
        internal static string EventNotificationNotifier_ProducerError {
            get {
                return ResourceManager.GetString("EventNotificationNotifier_ProducerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The event stream {0} is at checkpoint &apos;{1}&apos;, but new events are at version {2}. Perhaps some event history is missing?.
        /// </summary>
        internal static string ReadModelProjector_CheckpointError {
            get {
                return ResourceManager.GetString("ReadModelProjector_CheckpointError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The projection &apos;{0}&apos; did not handle the event &apos;{1}&apos; with event type &apos;{2}&apos;. Aborting projections.
        /// </summary>
        internal static string ReadModelProjector_ProjectionError {
            get {
                return ResourceManager.GetString("ReadModelProjector_ProjectionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No projection is configured for entity type &apos;{0}&apos;. Aborting.
        /// </summary>
        internal static string ReadModelProjector_ProjectionNotConfigured {
            get {
                return ResourceManager.GetString("ReadModelProjector_ProjectionNotConfigured", resourceCulture);
            }
        }
    }
}