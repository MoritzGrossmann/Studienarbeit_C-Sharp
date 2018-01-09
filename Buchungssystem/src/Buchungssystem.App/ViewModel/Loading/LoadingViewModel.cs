using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.Loading
{
    internal class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            
        }

        public LoadingViewModel(IPersistBookingSystemData persistence, EventHandler<ICollection<Room>> onRoomsLoaded)
        {
            var awaiter = GetRoomsAsync(persistence).GetAwaiter();
            while (!awaiter.IsCompleted)
            {
                
            }
            onRoomsLoaded?.Invoke(this, awaiter.GetResult());
        }

        private async Task<ICollection<Room>> GetRoomsAsync(IPersistBookingSystemData persistence)
        {
            return await Task.Run(() => persistence.Rooms().ToList());
        }
    }
}
