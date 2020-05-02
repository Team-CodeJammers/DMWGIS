using DMWGIS.API.Adapter.Interface;
using DMWGIS.API.Helper.Interface;
using DMWGIS.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using Type = DMWGIS.API.Models.Type;

namespace DMWGIS.API.Adapter
{
    internal class GetUserDataAdapter:IGetUserDataAdapter
    {
        private readonly IMongoClientHelper<UserNotification> _getNotificationDetails;
        private readonly IMongoClientHelper<City> _getCityList;
        private readonly IMongoClientHelper<Type> _getTypes;

        private readonly string _cityMasterCollection;
        private readonly string _notificationCollection;
        private readonly string _typesCollection;

        public GetUserDataAdapter(IMongoClientHelper<UserNotification> getNotificationDetails,
            IMongoClientHelper<Type> getTypes, IMongoClientHelper<City> getCityList,IOptions<MongoDbSettings> settings)
        {
            _getNotificationDetails = getNotificationDetails;
            _getCityList = getCityList;
            _getTypes = getTypes;
            _cityMasterCollection = settings.Value.CityMasterCollection;
            _notificationCollection = settings.Value.AlertNotificationCollection;
            _typesCollection = settings.Value.TypeCollection;
        }

        public List<UserNotification> GetUserNotifications(string userid)
        {
            var filterUserId = Builders<UserNotification>.Filter.Eq( f => f.userid, userid);
            var filterPending = Builders<UserNotification>.Filter.Eq(f => f.status, "pending");
            var filterActive = Builders<UserNotification>.Filter.Eq(f => f.status, "active");
            var orStatus = Builders<UserNotification>.Filter.Or(filterPending,filterActive);
            var filterNotification = Builders<UserNotification>.Filter.And(filterUserId,orStatus);

            return _getNotificationDetails.GetData(filterNotification, _notificationCollection);
        }

        public void AddAlert(UserNotification notification)
        {
            notification.alertid = Guid.NewGuid().ToString();
            notification.createtime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            _getNotificationDetails.InsertOne(notification, _notificationCollection);
        }

        public List<City> GetCities()
        {

            return _getCityList.GetData(FilterDefinition<City>.Empty, _cityMasterCollection);
        }

        public List<Type> GetTypes()
        {
            return _getTypes.GetData(FilterDefinition<Type>.Empty,_typesCollection);
        }
    }
}
