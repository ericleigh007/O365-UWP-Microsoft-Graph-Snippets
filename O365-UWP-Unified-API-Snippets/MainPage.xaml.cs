﻿// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace O365_UWP_Unified_API_Snippets
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<StoryDefinition> StoryCollection { get; private set; }
        public MainPage()
        {
            this.InitializeComponent();
            CreateStoryList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Developer code - if you haven't registered the app yet, we warn you. 
            if (!App.Current.Resources.ContainsKey("ida:ClientID"))
            {
                Debug.WriteLine("Oops - App not registered with Office 365. To run this sample, you must register it with Office 365. See Readme for more info.");

            }
        }
        private void CreateStoryList()
        {
            StoryCollection = new List<StoryDefinition>();

            // These stories require your app to have permission to access your organization's directory. 
            // Comment them if you're not going to run the app with that permission level.

            // API level
            StoryCollection.Add(new StoryDefinition() { GroupName = "API", Title = "Get MetaData", RunStoryAsync = APIStories.TryGetMetadataAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "API", Title = "Get BetaData", RunStoryAsync = APIStories.TryGetBetadataAsync });

            // User stories

            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Me", RunStoryAsync = UserStories.TryGetMeAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Read Users", RunStoryAsync = UserStories.TryGetUsersAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Create User", RunStoryAsync = UserStories.TryCreateUserAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Drive", RunStoryAsync = UserStories.TryGetCurrentUserDriveAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Events", RunStoryAsync = UserStories.TryGetEventsAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Create Event", RunStoryAsync = UserStories.TryCreateEventAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Update Event", RunStoryAsync = UserStories.TryUpdateEventAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Delete Event", RunStoryAsync = UserStories.TryDeleteEventAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Messages", RunStoryAsync = UserStories.TryGetMessages });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Send Message", RunStoryAsync = UserStories.TrySendMailAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Manager", RunStoryAsync = UserStories.TryGetCurrentUserManagerAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Directs", RunStoryAsync = UserStories.TryGetDirectReportsAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get Photo", RunStoryAsync = UserStories.TryGetCurrentUserPhotoAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get User Groups", RunStoryAsync = UserStories.TryGetCurrentUserGroupsAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Get User Files", RunStoryAsync = UserStories.TryGetCurrentUserFilesAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Create Text File", RunStoryAsync = UserStories.TryCreateFileAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Download File", RunStoryAsync = UserStories.TryDownloadFileAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Update File", RunStoryAsync = UserStories.TryUpdateFileAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Rename File", RunStoryAsync = UserStories.TryRenameFileAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Delete File", RunStoryAsync = UserStories.TryDeleteFileAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Create Folder", RunStoryAsync = UserStories.TryCreateFolderAsync });

            StoryCollection.Add(new StoryDefinition() { GroupName = "Users", Title = "Test User Events", RunStoryAsync = UserStories.TryGetTestUsersEventsAsync });

            // Group stories

            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Get All Groups", RunStoryAsync = GroupStories.TryGetGroupsAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Get a Group", RunStoryAsync = GroupStories.TryGetGroupAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Get Members", RunStoryAsync = GroupStories.TryGetGroupMembersAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Get Owners", RunStoryAsync = GroupStories.TryGetGroupOwnersAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Create Group", RunStoryAsync = GroupStories.TryCreateGroupAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Update Group", RunStoryAsync = GroupStories.TryUpdateGroupAsync });
            StoryCollection.Add(new StoryDefinition() { GroupName = "Groups", Title = "Delete Group", RunStoryAsync = GroupStories.TryDeleteGroupAsync });


            // Organization stories

            StoryCollection.Add(new StoryDefinition() { GroupName = "Organization", Title = "Get Org Drives", RunStoryAsync = OrganizationStories.TryGetDrivesAsync });


            var result = from story in StoryCollection group story by story.GroupName into api orderby api.Key select api;
            StoriesByApi.Source = result;
        }


        private async void RunSelectedStories_Click(object sender, RoutedEventArgs e)
        {

            await runSelectedAsync();
        }

        private async Task runSelectedAsync()
        {
            ResetStories();
            Stopwatch sw = new Stopwatch();

            foreach (var story in StoryGrid.SelectedItems)
            {
                StoryDefinition currentStory = story as StoryDefinition;
                currentStory.IsRunning = true;
                sw.Restart();
                bool result = false;
                try
                {
                    result = await currentStory.RunStoryAsync();
                    Debug.WriteLine(String.Format("{0}.{1} {2}", currentStory.GroupName, currentStory.Title, (result) ? "passed" : "failed"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("{0}.{1} failed. Exception: {2}", currentStory.GroupName, currentStory.Title, ex.Message);
                    result = false;

                }
                currentStory.Result = result;
                sw.Stop();
                currentStory.DurationMS = sw.ElapsedMilliseconds;
                currentStory.IsRunning = false;


            }

            // To shut down this app when the Stories complete, uncomment the following line. 
            // Application.Current.Exit();
        }

        private void ResetStories()
        {
            foreach (var story in StoryCollection)
            {
                story.Result = null;
                story.DurationMS = null;
            }
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            StoryGrid.SelectedItems.Clear();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationHelper.SignOut();
            StoryGrid.SelectedItems.Clear();
        }

        private async void CopyRedirectUriToClipboard_Click(object sender, RoutedEventArgs e)
        {
            var redirectURI = AuthenticationHelper.GetAppRedirectURI();

            var dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dataPackage.SetText(redirectURI);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            Windows.ApplicationModel.DataTransfer.Clipboard.Flush();

            var dialog = new Windows.UI.Popups.MessageDialog(redirectURI, "App Redirect URI copied to clipboard");
            await dialog.ShowAsync();

        }
    }
}

//********************************************************* 
// 
//O365-UWP-Microsoft-Graph-Snippets, https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Snippets
//
//Copyright (c) Microsoft Corporation
//All rights reserved. 
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:

// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
//********************************************************* 