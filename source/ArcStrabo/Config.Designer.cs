//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArcStrabo {
    using ESRI.ArcGIS.Framework;
    using ESRI.ArcGIS.ArcMapUI;
    using System;
    using System.Collections.Generic;
    using ESRI.ArcGIS.Desktop.AddIns;
    
    
    /// <summary>
    /// A class for looking up declarative information in the associated configuration xml file (.esriaddinx).
    /// </summary>
    internal static class ThisAddIn {
        
        internal static string Name {
            get {
                return "ArcStrabo2";
            }
        }
        
        internal static string AddInID {
            get {
                return "{452AA858-1606-4E99-9833-1D38F46096C8}";
            }
        }
        
        internal static string Company {
            get {
                return "University of Southern California";
            }
        }
        
        internal static string Version {
            get {
                return "1.0";
            }
        }
        
        internal static string Description {
            get {
                return "ArcStrabo2";
            }
        }
        
        internal static string Author {
            get {
                return "simakmo";
            }
        }
        
        internal static string Date {
            get {
                return "5/11/2014";
            }
        }
        
        internal static ESRI.ArcGIS.esriSystem.UID ToUID(this System.String id) {
            ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = id;
            return uid;
        }
        
        /// <summary>
        /// A class for looking up Add-in id strings declared in the associated configuration xml file (.esriaddinx).
        /// </summary>
        internal class IDs {
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ButtonShowStraboHome', the id declared for Add-in Button class 'ButtonShowStraboHome'
            /// </summary>
            internal static string ButtonShowStraboHome {
                get {
                    return "USC_ArcStrabo_ButtonShowStraboHome";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ButtonShowTessdataPrefix', the id declared for Add-in Button class 'ButtonShowTessdataPrefix'
            /// </summary>
            internal static string ButtonShowTessdataPrefix {
                get {
                    return "USC_ArcStrabo_ButtonShowTessdataPrefix";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ComboBoxLayerSelector', the id declared for Add-in ComboBox class 'ComboBoxLayerSelector'
            /// </summary>
            internal static string ComboBoxLayerSelector {
                get {
                    return "USC_ArcStrabo_ComboBoxLayerSelector";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ComboBoxLanguageSelector', the id declared for Add-in ComboBox class 'ComboBoxLanguageSelector'
            /// </summary>
            internal static string ComboBoxLanguageSelector {
                get {
                    return "USC_ArcStrabo_ComboBoxLanguageSelector";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ButtonSymbolRecognition', the id declared for Add-in Button class 'ButtonSymbolRecognition'
            /// </summary>
            internal static string ButtonSymbolRecognition {
                get {
                    return "USC_ArcStrabo_ButtonSymbolRecognition";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ButtonTextExtraction', the id declared for Add-in Button class 'ButtonTextExtraction'
            /// </summary>
            internal static string ButtonTextExtraction {
                get {
                    return "USC_ArcStrabo_ButtonTextExtraction";
                }
            }
            
            /// <summary>
            /// Returns 'USC_ArcStrabo_ButtonColorSegmentation', the id declared for Add-in Button class 'ButtonColorSegmentation'
            /// </summary>
            internal static string ButtonColorSegmentation {
                get {
                    return "USC_ArcStrabo_ButtonColorSegmentation";
                }
            }
            
            /// <summary>
            /// Returns 'ArcStrabo2', the id declared for Add-in Extension class 'ArcStrabo2Extension'
            /// </summary>
            internal static string ArcStrabo2Extension {
                get {
                    return "ArcStrabo2";
                }
            }
        }
    }
    
internal static class ArcMap
{
  private static IApplication s_app = null;
  private static IDocumentEvents_Event s_docEvent;

  public static IApplication Application
  {
    get
    {
      if (s_app == null)
        s_app = Internal.AddInStartupObject.GetHook<IMxApplication>() as IApplication;

      return s_app;
    }
  }

  public static IMxDocument Document
  {
    get
    {
      if (Application != null)
        return Application.Document as IMxDocument;

      return null;
    }
  }
  public static IMxApplication ThisApplication
  {
    get { return Application as IMxApplication; }
  }
  public static IDockableWindowManager DockableWindowManager
  {
    get { return Application as IDockableWindowManager; }
  }
  public static IDocumentEvents_Event Events
  {
    get
    {
      s_docEvent = Document as IDocumentEvents_Event;
      return s_docEvent;
    }
  }
}

namespace Internal
{
  [StartupObjectAttribute()]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public sealed partial class AddInStartupObject : AddInEntryPoint
  {
    private static AddInStartupObject _sAddInHostManager;
    private List<object> m_addinHooks = null;

    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    public AddInStartupObject()
    {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool Initialize(object hook)
    {
      bool createSingleton = _sAddInHostManager == null;
      if (createSingleton)
      {
        _sAddInHostManager = this;
        m_addinHooks = new List<object>();
        m_addinHooks.Add(hook);
      }
      else if (!_sAddInHostManager.m_addinHooks.Contains(hook))
        _sAddInHostManager.m_addinHooks.Add(hook);

      return createSingleton;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void Shutdown()
    {
      _sAddInHostManager = null;
      m_addinHooks = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static T GetHook<T>() where T : class
    {
      if (_sAddInHostManager != null)
      {
        foreach (object o in _sAddInHostManager.m_addinHooks)
        {
          if (o is T)
            return o as T;
        }
      }

      return null;
    }

    // Expose this instance of Add-in class externally
    public static AddInStartupObject GetThis()
    {
      return _sAddInHostManager;
    }
  }
}
}
