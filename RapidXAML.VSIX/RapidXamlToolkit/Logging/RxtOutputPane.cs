﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace RapidXamlToolkit.Logging
{
    public class RxtOutputPane : IOutputPane
    {
        private static Guid rxtPaneGuid = new Guid("32C5FA5D-E91C-4113-8B22-3396D748D429");
        private static string rxtPaneTitle = "Rapid XAML Toolkit";

        private static RxtOutputPane instance;

        private readonly IVsOutputWindowPane rxtPane;

        private RxtOutputPane()
        {
            if (ServiceProvider.GlobalProvider.GetService(typeof(SVsOutputWindow)) is IVsOutputWindow outWindow)
            {
                outWindow.GetPane(ref rxtPaneGuid, out this.rxtPane);

                if (this.rxtPane == null)
                {
                    outWindow.CreatePane(ref rxtPaneGuid, rxtPaneTitle, 1, 0);
                    outWindow.GetPane(ref rxtPaneGuid, out this.rxtPane);
                }
            }
        }

        public static RxtOutputPane Instance => instance ?? (instance = new RxtOutputPane());

        public void Write(string message)
        {
            this.rxtPane.OutputString(message);
        }

        public void Activate()
        {
            this.rxtPane.Activate();
        }
    }
}
