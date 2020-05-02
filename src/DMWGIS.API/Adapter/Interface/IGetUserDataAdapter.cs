using DMWGIS.API.Models;
using System.Collections.Generic;

namespace DMWGIS.API.Adapter.Interface
{
    public interface IGetUserDataAdapter
    {
        List<UserNotification> GetUserNotifications(string userid);
        void AddAlert(UserNotification notification);
        List<City> GetCities();
        List<Type> GetTypes();
    }
}
