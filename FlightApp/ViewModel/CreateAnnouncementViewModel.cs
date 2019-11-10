using FlightApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlightApp.ViewModel
{
    public class CreateAnnouncementViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public async void PostAnnouncement()
        {
            try
            {
                if (!await AnnouncementRepository.PostAnnouncement(Title, Content))
                {
                    MessageDialog messageDialog = new MessageDialog("Invalid announcement. \nPlease try again.");
                    messageDialog.Commands.Add(new UICommand("Close"));
                    messageDialog.CancelCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
            }
            catch (Exception e)
            {
                MessageDialog messageDialog = new MessageDialog($"Couldn't establish a connection to the database. \n{e.Message}");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            }
        }
    }
}
