using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        Settings GetSettings();
        void Save(Settings settings);
    }
}
