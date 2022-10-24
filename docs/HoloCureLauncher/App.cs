using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace HoloCureLauncher;

#region External definitions

// These are definitions not part of the original code.

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
#pragma warning disable CA1822
#pragma warning disable CS0169
#pragma warning disable CS1998

#endregion

/// <inheritdoc cref="Application"/>
/// <summary>
///     Compiler-generated implementation of <see cref="Application"/> used to set the values of properties within the base <see cref="Application"/> class. <br />
///     Provides an <see cref="InitializeComponent"/> method to achieve the aforementioned property initialization as well as a static program entrypoint.
/// </summary>
public class App : Application
{
    #region Compiler-generated

    /// <summary>
    ///     Initializes this <see cref="Application"/> component. <br />
    ///     Sets <see cref="Application.StartupUri"/> as <see cref="UriKind.Relative"/> with a value of <c>"MainWindow.xaml"</c>.
    /// </summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.6.0")]
    public void InitializeComponent() {
        /* Code omitted. */
    }

    /// <summary>
    ///     The main program entrypoint. <br />
    ///     Instantiates a new instance of <see cref="App"/>, runs <see cref="App.InitializeComponent()"/> and them <see cref="App.Run()"/>.
    /// </summary>
    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.6.0")]
    public static void Main() {
        /* Code omitted. */
    }

    #endregion
}
