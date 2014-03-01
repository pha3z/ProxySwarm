﻿using PropertyChanged;
using ProxySwarm.Domain;
using ProxySwarm.WpfApp.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProxySwarm.WpfApp.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : BaseViewModel
    {
        private bool isPlaying;

        private void PlayPauseHandler()
        {
            //if (this.isPlaying)
            //    this.swarmCoordinator.Pause();
            //else
            //    this.swarmCoordinator.Start();

            this.isPlaying = !this.isPlaying;
        }

        private void FilesPickedHandler(string[] fileNames)
        {
            //foreach (var file in fileNames)
            //    this.proxyFileSource.ReadProxiesFromFileAsync(file, CancellationToken.None);
        }

        private async Task UpdateUIAsync()
        {
            while (true)
            {
                await Task.WhenAll(this.uiInvoker.YieldBackgroundPriority(), Task.Delay(50));
                //await this.counterBinds.ReceiveAsync();
                //this.counterBinds.UpdateAndFlushIfReceived();
            }
        }

        public MainViewModel(IUIInvoker uiInvoker)
            : base(uiInvoker)
        {
            this.PlayPauseCommand = new DelegateCommand(this.PlayPauseHandler);
            this.FilesPickedCommand = new DelegateCommand<string[]>(this.FilesPickedHandler);

            this.uiInvoker.InvokeOnUIThreadAsync(async () => await this.UpdateUIAsync());
        }

        public ICommand PlayPauseCommand { get; private set; }
        public ICommand FilesPickedCommand { get; private set; }

        public int SuccessCount { get; private set; }
        public int FailCount { get; private set; }
        public int ConnectionCount { get; private set; }
        public int ProxyCount { get; private set; }
    }
}