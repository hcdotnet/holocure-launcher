using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using HoloCureLauncher.Helpers;

#region External definitions

// These are definitions not part of the original code.

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
#pragma warning disable CA1822
#pragma warning disable CS0169
#pragma warning disable CS1998

#endregion

namespace HoloCureLauncher;

/// <summary>
/// 
/// </summary>
public class MainWindow : Window, IComponentConnector
{
    /// <summary>
    /// 
    /// </summary>
    private enum LauncherStatus
    {
        /// <summary>
        /// 
        /// </summary>
        GameReady,

        /// <summary>
        /// 
        /// </summary>
        Installing,

        /// <summary>
        /// 
        /// </summary>
        Updating,

        /// <summary>
        /// 
        /// </summary>
        Failed
    }

    /// <summary>
    /// 
    /// </summary>
    private Downloader Downloader;

    /// <summary>
    /// 
    /// </summary>
    private LauncherStatus Status;

    /// <summary>
    /// 
    /// </summary>
    private static string GameDirectoryPath;

    /// <summary>
    /// 
    /// </summary>
    private static string GameExecutablePath;

    /// <summary>
    /// 
    /// </summary>
    private static string GameExeName; // = ConfigurationManager.AppSettings["GameExeName"];

    /// <summary>
    /// 
    /// </summary>
    internal Button PlayButton;

    /// <summary>
    /// 
    /// </summary>
    internal Button DownloadButton;

    /// <summary>
    /// 
    /// </summary>
    private bool _contentLoaded;

    /// <summary>
    /// 
    /// </summary>
    private void ChangeDownloadButtonState() {
        /* Code omitted. */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlayButtonCallback(object sender, RoutedEventArgs e) {
        /* Code omitted. */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void InstallButtonCallback(object sender, RoutedEventArgs e) {
        /* Code omitted. */
    }

    /// <summary>
    /// 
    /// </summary>
    public MainWindow() {
        /* Code omitted. */
    }

    #region Compiler-generated

    /// <summary>
    /// 
    /// </summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "0.6.0.6")]
    public void InitializeComponent() {
        /* Code omitted. */
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="target"></param>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "0.6.0.6")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target) {
        /* Code omitted. */
    }

    #endregion
}
