﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitClientVS.Contracts.Interfaces;
using GitClientVS.Contracts.Interfaces.Services;
using GitClientVS.Contracts.Interfaces.Views;
using GitClientVS.VisualStudio.UI.Window;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace GitClientVS.VisualStudio.UI.Services
{
    [Export(typeof(ICommandsService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CommandsService : ICommandsService
    {
        private Package _package;

        public void Initialize(object package)
        {
            _package = (Package)package;
        }


        public void ShowDiffWindow(object parameter, int? id)
        {
            ShowWindow<DiffWindow>(parameter, id);
        }

        private void ShowWindow<TWindow>(object parameter = null, int? id = null)
        {
            if (_package == null)
                throw new Exception("Package wasn't initialized");

            var windowNumber = id ?? 0;

            ToolWindowPane window = _package.FindToolWindow(typeof(TWindow), windowNumber, true);

            if (window?.Frame == null)
                throw new NotSupportedException("Cannot create window");

            var view = window.Content as IView;
            var vm = view?.DataContext as IInitializable;
            vm?.InitializeCommand.Execute(parameter);

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}