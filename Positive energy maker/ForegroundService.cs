using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Positive_energy_maker
{
    [Service]
    class ForegroundService : Service
    {
        private const int NOTIFICATION_ID = 1000;

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            #region Channel

            string channelId = "com.sasame.example";
            string channelName = "exampleForegroundService";

            NotificationChannel channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default);

            #endregion

            #region Notificaton

            // when user click the notification
            Intent newIntent = new Intent(this, typeof(MainActivity));
            Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(newIntent);

            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channelId);
            Notification notification = builder
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("my notification")
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetContentText("click here to open main page")
                .Build();

            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(channel);

            StartForeground(NOTIFICATION_ID, notification);

            #endregion

            return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}