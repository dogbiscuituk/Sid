﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToyGraf.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hue, Saturation")]
        public string KnownColorSortOrder {
            get {
                return ((string)(this["KnownColorSortOrder"]));
            }
            set {
                this["KnownColorSortOrder"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ToyGraf Files (*.tgf)|*.tgf|ToyGraf Templates (*.tgt)|*.tgt|All Files (*.*)|*.*")]
        public string GraphFilter {
            get {
                return ((string)(this["GraphFilter"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("800, 480")]
        public global::System.Drawing.Size GraphForm_MinimumSize {
            get {
                return ((global::System.Drawing.Size)(this["GraphForm_MinimumSize"]));
            }
            set {
                this["GraphForm_MinimumSize"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Images (*.bmp;*.gif;*.jpeg;*.jpg;*.png)|*.bmp;*.gif;*.jpeg;*.jpg;*.png|All files " +
            "(*.*)|*.*")]
        public string ImageFilter {
            get {
                return ((string)(this["ImageFilter"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Options_OpenInNewWindow {
            get {
                return ((bool)(this["Options_OpenInNewWindow"]));
            }
            set {
                this["Options_OpenInNewWindow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Options_GroupUndo {
            get {
                return ((bool)(this["Options_GroupUndo"]));
            }
            set {
                this["Options_GroupUndo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string FilesFolderPath {
            get {
                return ((string)(this["FilesFolderPath"]));
            }
            set {
                this["FilesFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TemplatesFolderPath {
            get {
                return ((string)(this["TemplatesFolderPath"]));
            }
            set {
                this["TemplatesFolderPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseMaxima {
            get {
                return ((bool)(this["UseMaxima"]));
            }
            set {
                this["UseMaxima"] = value;
            }
        }
    }
}
